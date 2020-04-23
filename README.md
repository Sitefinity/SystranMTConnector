Progress.Sitefinity.Translations.SystranMachineTranslationConnector
===========================================

>**Latest supported version**: Sitefinity CMS 12.1.7100.0

### Overview

When working with the Sitefinity CMS *Translation* module, you can benefit from a number of translation connectors that you use out-of-the-box with minimum setup. To serve your requirements, you can also implement your own translation connector with custom logic. 

This tutorial provides you with a sample that you use to implement a custom translation connector to work with the *SYSTRAN Pure Neural Server* service. You first create and setup the connector and then use the *Systran Translation .Net Client Library* to implement the overall translation process.   

### Prerequisites
- You must have a Sitefinity CMS license.
- Your Sitefinity CMS website must be in multilingual mode.  
 You have added atleast one additinal language to the website. Otherwise, you will not see the translations screen in the administrations tab of your application.

Currently, **SYSTRAN Pure Neural Server** machine translation service supports two-letter ISO language codes like 'en' 'fr' 'de' 'es' 'nl' etc., you can use neutral culture like 'en', or specify a mapping between the specific culture and neutral culture in the translations advanced settings screen: <i>Administration >> Settings >> Advanced >> Culture mappings </i> text box.

### Instalation

Add the *Systran Machine Translation* sample project to your solution. To do this:

1. Open your Sitefinity CMS solution in Visual Studio.
2. Add `Progress.Sitefinity.Translations.SystranMachineTranslation` project to the same solution.
3. In _SystranMachineTranslation_, add a reference to the `SystranClientTranslationApiLib` assembly.  
 You can download it from Systran Natural Language Processing .NET Client Library from https://github.com/SYSTRAN/translation-api-csharp-client.
4. Ensure `Telerik.Sitefinity.Translations` nuget package is installed in `SystranMachineTranslation`.
5. In _SitefinityWebApp_, add a reference to the *SystranMachineTranslation* project.
6. Build your solution.

## Configure the connector

To configure the *SystranMachineTranslationConnector* connector, perform the following:

1. In Sitefinity CMS, navigate to _Administration >> Settings >> Advanced >> Translations >> Connectors_.
2. Expand the _Parameters_ section of the _SystranMachineTranslation_ connector
3. Enter and save the following _Keys_: 
   **NOTE:** The following parameters must be obtained from Systran.io https://platform.systran.net/user/admin#/apiKeys .
 * <strong>apiKey</strong> </br>In <i>Value</i>, enter the api key
 * <strong>apiUrl</strong> </br>The Systran API Url. If not set, the default value **https://api-platform.systran.net** will be used
3. To enable the connector, for <strong>SystranMachineTranslation</strong> in the <i>Enabled</i> field, enter <strong>true</strong>.
4. Save your changes.

Note: SYSTRAN Pure Neural Server is not supporting culture-specific languages.
