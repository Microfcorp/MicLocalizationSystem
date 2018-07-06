# MicLocalizationSystem
Language management system developed by Microf for its projects

Description of library commands:
```C#
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
