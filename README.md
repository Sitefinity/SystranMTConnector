# Progress.Sitefinity.Translations.SystranMachineTranslationConnector

### This repository is not automatically upgraded to latest Sitefintiy version. The repository is monitored for pull requests and fixes. The latest official version of Sitefinity that supports this sample is v11.0 Beta. Be aware that using a higher version could cause unexpected behavior. If you successfully upgrade the example to a greater version, please share your work with the community by submitting your changes via pull request.

When working with the Sitefinity CMS *Translation* module, you can benefit from a number of translation connectors that you use out-of-the-box with minimum setup. You can, however, implement your own translation connector with custom logic to serve your requirements. 

This tutorial provides you with a sample that you use to implement a custom translation connector to work with the **Systran.io** service. You first create and setup the connector and then use the *Systran Translation .Net Client Library*  to implement the overall translation process.   
## Requirements
Sitefinity CMS license

## Prerequisites

Your Sitefinity CMS web site must be in multilingual mode meaning that you have added atleast one additinal language to the current website you are browsing. Otherwise you will not see the translations screen in the administrations tab of your application.

You should either use country specific languages like 'en-US' and not just 'en' or specify a mapping between the country invariant and country specific language in the translations advanced settings screen: <i>Administration >> Settings >> Advanced >> Culture mappings </i> text box.

Add the *Systran Machine Translation* sample project to your solution. To do this:

1. In Visual Studio, open your Sitefinity CMS web application solution.
2. In the **SitefinityWebApp**, add a reference to the <strong>Telerik.Sitefinity.Translations.SystranMachineTranslationConnector</strong> assembly.
3. In the **SitefinityWebApp**, add a reference to the <strong>SystranClientTranslationApiLib<strong> assembly. Download from Systran Natural Language Processing .Net Client Library from https://github.com/SYSTRAN/nlp-api-csharp-client.


## Create and configure the connector

To configure the *SystranMachineTranslationConnector* connector in Sitefinity CMS:

1. Navigate to <i>Administration >> Settings >> Advanced >> Translations >> Connectors >> Create new</i>.
2. In <i>Connector name</i>, enter <strong>SystranMachineTranslation</strong>.
3. In <i>Connector title</i>, enter <strong>Systran Machine Translation</strong>.
4. In <i>Connector type</i>, enter <strong>Telerik.Sitefinity.Translations.SystranMachineTranslationConnector</strong>.
5. To enable the connector, in the <i>Enabled</i> field, enter <strong>true</strong>.
6. Save your changes.
7.In your web app folder add api key file obtained fro Systran.io [TBD] add link

## Systran Natural Language Processing .Net Client Library
https://github.com/SYSTRAN/nlp-api-csharp-client 

