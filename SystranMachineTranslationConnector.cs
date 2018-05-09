using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using Systran;
using Systran.TranslationClientLib.Api;
using Systran.TranslationClientLib.Client;
using Telerik.Sitefinity.Translations;

namespace Progress.Sitefinity.Translations
{
    public class SystranMachineTranslationConnector : MachineTranslationConnector
    {
        #region Initialization
        protected override void InitializeConnector(NameValueCollection config)
        {
            if (!File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/apiKey.txt")))
                throw new Exception("To properly run the tests, please add an apiKey.txt file containing your api key in the SystranClientTranslationApiLibTests folder or edit the test file with your key");

            this.client = new ApiClient("https://api-platform.systran.net");
            Configuration.apiClient = client;
            Dictionary<String, String> keys = new Dictionary<String, String>();
            string key;
            using (StreamReader streamReader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/apiKey.txt"), Encoding.UTF8))
            {
                key = streamReader.ReadToEnd();
            }

            keys.Add("key", key);
            Configuration.apiKey = keys;
            if (keys.Count == 0)
                throw new Exception("No api key found, please check your apiKey.txt file");

            this.translationApi = new TranslationApi(Configuration.apiClient);

        }
        #endregion

        protected override List<string> Translate(List<string> input, ITranslationOptions translationOptions)
        {
            var output = new List<string>();
            foreach (var item in input)
            {
                var response = this.translationApi.TranslationTranslateGet(new List<string>() { item }, translationOptions.SourceLanguage, translationOptions.TargetLanguage, null, null, true, false, null, null, false, null, null, false, null, null);
                output.Add(response.Outputs.First().Output);
            }

            return output;
        }

        private ApiClient client;
        private TranslationApi translationApi;
    }
}