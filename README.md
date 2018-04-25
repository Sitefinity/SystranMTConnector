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
2. Add Telerik.Sitefinity.Translations.SystranMachineTranslation project to the same solution
3. In **SystranMachineTranslation** add a reference to the **SystranClientTranslationApiLib** assembly. Download from Systran Natural Language Processing .Net Client Library from https://github.com/SYSTRAN/nlp-api-csharp-client.
4. Ensure Telerik.Sitefinity.Translations nuget package is installed in **SystranMachineTranslation**.
5. In **SitefinityWebApp**, add a reference to the **SystranMachineTranslation** project.
6. In **SitefinityWebApp** add apiKey.txt file containing key obtained from Systran.io https://platform.systran.net/user/admin#/apiKeys .
7. Compile your solution
8. Add the following binding redirect to the web.config of **SitefinityWebApp**
<dependentAssembly>
        <assemblyIdentity name="Newtonsoft.Json" publicKeyToken="30ad4fe6b2a6aeed" culture="neutral" />
       <bindingRedirect oldVersion="0.0.0.0-11.0.0.0" newVersion="10.0.0.0" />
</dependentAssembly>

## Create and configure the connector

To configure the *SystranMachineTranslationConnector* connector in Sitefinity CMS:

1. Navigate to <i>Administration >> Settings >> Advanced >> Translations >> Connectors >> Create new</i>.
2. In <i>Connector name</i>, enter <strong>SystranMachineTranslation</strong>.
3. In <i>Connector title</i>, enter <strong>Systran Machine Translation</strong>.
4. In <i>Connector type</i>, enter <strong>Telerik.Sitefinity.Translations.SystranMachineTranslationConnector</strong>.
5. To enable the connector, in the <i>Enabled</i> field, enter <strong>true</strong>.
6. Save your changes.
7. For MVC widgets follow the documentation https://docs.sitefinity.com/administration-translate-mvc-widgets 
8. For optimal translation time apply the following settings:
 Advanced Settings > Translations > Scheduling > Sync settings > Auto sync cron specification: */0,1 * * * *, Timeout: 1


## Systran Natural Language Processing .Net Client Library
https://github.com/SYSTRAN/nlp-api-csharp-client 

