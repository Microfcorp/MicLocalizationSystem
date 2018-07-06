using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;

namespace MicLocalizationSystem
{
    public class LocalizationSettings
    {
        public static Language GetLangSystem()
        {
            Language tmp = new Language();
            CultureInfo culture = CultureInfo.CurrentCulture;            
            tmp.langid = culture.Name;
            return tmp;
        }
        public static Language GetLangName(string name, string PathToFoldersLang)
        {
            Language tmp = new Language();
            if (File.Exists(PathToFoldersLang + "\\" + name + ".lng"))
            {               
                tmp.langid = name;
            }
            return tmp;
        }
        public static LangParam LoadLangParamFromFile(Language Lang, string PathToFoldersLang)
        {
            LangParam tmp = new LangParam();
            string file = File.ReadAllText(PathToFoldersLang + "\\" + Lang.langid + ".lng", Encoding.UTF8).Replace("\n","").Replace("\r", "");

            for (int i = 0; i < file.Split(';').Length - 1; i++)
            {
                tmp.Text.Add(file.Split(';')[i].Split('=')[0], file.Split(';')[i].Split('=')[1]);
            }
            return tmp;
        }
        public static Language[] GetFromFolder(string PathToFoldersLang)
        {
            List<string> languages = Directory.GetFiles(PathToFoldersLang, "*.lng").ToList();
            List<Language> tmp = new List<Language>();
            foreach (var item in languages)
            {
                Language lg = new Language();               
                lg.langid = item.Split('\\')[item.Split('\\').Length - 1].Split('.')[0];
                tmp.Add(lg);
            }
            return tmp.ToArray();
        }
    }
    public class Language
    {
        internal string langid;
        public string GetName()
        {
            return langid;
        }
    }
    public class LangParam
    {
        internal SortedList<string, string> Text = new SortedList<string, string>();
        public string GetLangText(string name)
        {
            return Text[name];
        }        
    }
}
