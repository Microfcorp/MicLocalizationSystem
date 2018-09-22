# MicLocalizationSystem
Language management system developed by Microf for its projects

Description of library commands:
```C#
using MicLocalizationSystem;
Language lg = LocalizationSettings.GetLangSystem(); //To get the current system language
Language lg = LocalizationSettings.GetLangName(name, Environment.CurrentDirectory + "\\Localization\\");//Get a localized file name

LangParam lng = LocalizationSettings.LoadLangParamFromFile(lg, Environment.CurrentDirectory + "\\Localization\\"); //Download the localization files for the desired language and from the required folder.
Console.WriteLine(lng.GetLangText("Load_Video")); //Get a localized string with the name

Language[] lng = LocalizationSettings.GetFromFolder(Environment.CurrentDirectory + "\\Localization\\");//Get an array of all localization files
foreach (var item in lng)
{
     Console.WriteLine(item.GetName()); //To obtain the name of the localized file     
}
```
Online Translate:
```C#
using MicLocalizationSystem.Translate;
Console.WriteLine(OnlineLocalization.GetOnline(Lang.(Language), string Text)); //Get a localized string with Internet connection
```

Automatic genaration localizations lines:
```C#
using MicLocalizationSystem.Form;
using System.Windows.Forms;
Console.WriteLine(FormLocalizations.WriteConsoleLNG(Control.ControlCollection Controls));
Console.WriteLine(FormLocalizations.WriteConsoleCSharp(Control.ControlCollection Controls));
```

Including ConsoleGameEngine and 2 functions:
```GAM
LoadLangParamFromFile //and 2 parameters: NameLangFile and Path
PrintLocalized //and 1 parameters: Name
```
