using System;
using Telerik.Sitefinity.Translations;
using Telerik.Sitefinity.Translations.Events;
using Telerik.Sitefinity.Translations.Model.Contracts;
using Telerik.Sitefinity.Translations.Xliff.Model;
using Systran;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using Systran.TranslationClientLib.Api;
using Systran.TranslationClientLib.Client;

using System.Text;
using Systran.TranslationClientLib.Model;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Progress.Sitefinity.Translations
{
    public class SystranMachineTranslationConnector : TranslationConnectorBase
    {
        public NameValueCollection Config
        {
            get;
            private set;
        }

        internal static string Translate(string toTranslate, string targetLanguage)
        {
            return string.Format("{0}-{1}", toTranslate, targetLanguage);
        }

        #region Initialization
        protected override void InitializeConnector(NameValueCollection config)
        {
            this.Config = config;

            ClassInit();
        }
        #endregion

        #region Send methods
        protected override bool ProcessSendTranslationEvent(ISendTranslationTaskEvent evnt, ITranslationJobContext context, out string translationId)
        {
            translationId = string.Empty;
            var projectId = evnt.ProjectId;


            //get original xliff files
            var xliffFile = evnt.GetXliffFile();

            // translate xliff file
            this.TranslateXliff(xliffFile, evnt.ActualSourceLanguage, evnt.TargetLanguage);

            // add task for review of the translated xliff in the queue
            var reviewTransEvnt = new ReviewTranslationTaskEvent(xliffFile)
            {
                ProjectId = projectId
            };

            this.waitingMessages.Enqueue(
                new Tuple<DateTime, EventMessage>(
                    DateTime.UtcNow.AddSeconds(1),
                    reviewTransEvnt));

            return true;
        }

        protected void TranslateXliff(XliffFile xliffFile, string sourceLanguage, string targetLanguage)
        {
            foreach (var unit in xliffFile.Body)
            {
                //copy source xliff structure to target xliff structure
                unit.Target = new XliffTranslationUnitElement();
                this.CopyTo(unit.Source, unit.Target);

                //get source html chunks for translation
                List<string> markers = new List<string>();
                foreach (var marker in unit.Source.HtmlTextChunks.Markers)
                {
                    markers.Add(marker.Text);
                }

                //translate all chunks
                var translations = Translate(markers, sourceLanguage, targetLanguage).ToArray();

                // get target markers for html chunks
                var targetMarkers = unit.Target.HtmlTextChunks.Markers;

                // copy translated chunks to target xliff structure
                for (int i = 0; i < translations.Length;  i++)
                {
                    targetMarkers[i].Text = translations[i].Output;                   
                }
            }
        }

        private void CopyTo(XliffTranslationUnitElement source, XliffTranslationUnitElement target)
        {
            var propertyDescriptor = TypeDescriptor.GetProperties(typeof(XliffTranslationUnitElement));

            foreach (PropertyDescriptor property in propertyDescriptor)
            {
                property.SetValue(target, property.GetValue(source));
            }
        }

        internal static List<TranslationOutput> Translate(List<string> listToTranslate, string sourceLanguage, string targetLanguage)
        {
            var response = translationApi.TranslationTranslateGet(listToTranslate, sourceLanguage, targetLanguage, null, null, true, false, null, null, false, null, null, false, null, null);
            return response.Outputs;
        }

        protected override bool ProcessStartProjectEvent(IStartProjectTaskEvent evnt, ITranslationJobContext context, out string projectId)
        {
            projectId = null;
            return true;
        }

        protected override bool ProcessCompleteProjectEvent(ICompleteProjectTaskEvent evnt)
        {
            return true;
        }
        #endregion

        #region Sync methods

        protected override bool AcknowledgeMessage(object rawMessage, ITranslationSyncContext context)
        {

            return true;
        }

        protected override IEnumerable<object> GetRawMessages(ITranslationSyncContext context)
        {
            // Move messages which have waited enough to this.MessageQueue
            while (this.waitingMessages.Count > 0)
            {
                var head = this.waitingMessages.Peek();
                if (head.Item1 > DateTime.UtcNow)
                    break;
                this.MessageQueue.Enqueue(head.Item2);
                this.waitingMessages.Dequeue();
            }

            return new DummyMessageQueue(this);
        }

        protected override IEnumerable<SyncEventMessage> ExtractSyncEventMessages(object rawMessage, ITranslationSyncContext context)
        {
            var message = rawMessage as SyncEventMessage;
            return new SyncEventMessage[] { message };
        }

        protected override void OnEndSyncTranslationJob(ITranslationSyncContext context)
        {
        }

        #endregion

        internal Queue<EventMessage> MessageQueue
        {
            get { return this.messageQueue; }
        }

        private Queue<EventMessage> messageQueue = new Queue<EventMessage>();

        // We want to simulate a little delay between sending for translation and receiving the translated items,
        // so let's store waiting message here for a while.
        private readonly Queue<Tuple<DateTime, EventMessage>> waitingMessages = new Queue<Tuple<DateTime, EventMessage>>();

        internal class DummyMessageQueue : EventMessageQueue<EventMessage>
        {
            public DummyMessageQueue(SystranMachineTranslationConnector connector)
            {
                this.connector = connector;
            }

            public override EventMessage GetNextMessage()
            {
                var queue = this.connector.MessageQueue;

                if (queue.Count == 0)
                    return null;

                return queue.Dequeue();
            }

            private readonly SystranMachineTranslationConnector connector;
        }

        private static ApiClient client;
        private static TranslationApi translationApi;

        public static void ClassInit()
        {
            if (!File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/apiKey.txt")))
                throw new Exception("To properly run the tests, please add an apiKey.txt file containing your api key in the SystranClientTranslationApiLibTests folder or edit the test file with your key");
            client = new ApiClient("https://api-platform.systran.net");
            Configuration.apiClient = client;
            Dictionary<String, String> keys = new Dictionary<String, String>();
            string key;
            using (StreamReader streamReader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/apiKey.txt"), Encoding.UTF8))
            {
                key = streamReader.ReadToEnd();
            }
            keys.Add("key", key); Configuration.apiKey = keys; Configuration.apiKey = keys;
            Configuration.apiKey = keys;
            if (keys.Count == 0)
                throw new Exception("No api key found, please check your apiKey.txt file");
            translationApi = new TranslationApi(Configuration.apiClient);
        }
    }
}
