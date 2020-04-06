# Progress.Sitefinity.Translations.SystranMachineTranslationConnector

### Latest supported version: Sitefinity CMS 12.1.7100.0

## Overview

When working with the Sitefinity CMS *Translation* module, you can benefit from a number of translation connectors that you use out-of-the-box with minimum setup. You can, however, implement your translation connector with custom logic to serve your requirements. 

This tutorial provides you with a sample that you use to implement a custom translation connector to work with the **SYSTRAN Pure Neural Server** service. You first create and set up the connector and then use the *Systran Translation .Net Client Library*  to implement the overall translation process.

## Requirements
Sitefinity CMS license

## Prerequisites

Your Sitefinity CMS web site must be in multilingual mode, meaning that you have added at least one additional language to the current website you are browsing. Otherwise, you will not see the translations screen in the _Administration_ tab of your application.

Currently, **SYSTRAN Pure Neural Server** machine translation service supports two-letter ISO language codes like 'en' 'fr' 'de' 'es' 'nl' etc., you can use neutral culture like 'en', or specify a mapping between the specific culture and neutral culture in the translations advanced settings screen: _Administration » Settings » Advanced » Culture mappings_ text box.

Add the *Systran Machine Translation* sample project to your solution. To do this:

1. In Visual Studio, open your Sitefinity CMS web application solution..
2. Add _Progress.Sitefinity.Translations.SystranMachineTranslation_ project to the same solution.
3. Download the Systran Natural Language Processing .Net Client Library from https://github.com/SYSTRAN/translation-api-csharp-client, open it in Visual Studio, and build it.
4. In **SystranMachineTranslation** add a reference to the built **SystranClientTranslationApiLib** assembly from the previous step.
5. Ensure Telerik.Sitefinity.Translations NuGet package is installed in **SystranMachineTranslation**.
6. In **SitefinityWebApp**, add a reference to the **SystranMachineTranslation** project.
7. Compile your solution.

## Configure the connector

To configure the *SystranMachineTranslationConnector* connector in Sitefinity CMS:

1. Navigate to [Systran.io](https://platform.systran.net/user/admin#/apiKeys) and obtain the following parameters:
 * __API key__ </br>The API key
 * __API Url__ </br>The Systran API Url. This is optional, and if you do not set it, the default value **https://api-platform.systran.net** will be used
2. Navigate to _Administration » Settings » Advanced » Translations » Connectors_.
3. Expand the _Parameters_ section of the For **SystranMachineTranslation** connector, enter and save the following _Keys_:</br>
 * __apiKey__ </br>In _Value_, enter the api key
 * __apiUrl__ </br>The Systran API Url. If not set, the default value **https://api-platform.systran.net** will be used
3. To enable the connector, for **SystranMachineTranslation** in the _Enabled_ field, enter **true**.
4. Save your changes.

**Note**: SYSTRAN Pure Neural Server does not support culture-specific languages.
