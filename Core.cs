using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using SystemModule;
using System.Net;

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
//https://translate.yandex.net/api/v1/tr.json/translate?id=a24f8d10.5b859386.d50dfdd4-0-0&srv=tr-text&lang=ru-en&reason=auto

namespace MicLocalizationSystem.Translate
{
    public static class OnlineLocalization
    {

        public struct Lang
        {
            public static string RuEng
            {
                get { return "ru-en"; }
            }
            public static string EngRu
            {
                get { return "en-ru"; }
            }
        }
        public static string GetOnline(string lang, string Text)
        {
            return POST("https://translate.yandex.net/api/v1/tr.json/translate?id=a24f8d10.5b859386.d50dfdd4-0-0&srv=tr-text&lang="+lang+"&reason=auto", "text="+Text+"&options=4");
        }
        private static string POST(string Url, string Data)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.GetEncoding(1251).GetBytes(Data);
            req.ContentLength = sentData.Length;
            System.IO.Stream sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            System.Net.WebResponse res = req.GetResponse();
            System.IO.Stream ReceiveStream = res.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8);
            //Кодировка указывается в зависимости от кодировки ответа сервера
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            string Out = String.Empty;
            while (count > 0)
            {
                String str = new String(read, 0, count);
                Out += str;
                count = sr.Read(read, 0, 256);
            }
            return Out;
        }
    }
}

namespace GameModule
{
    public class GameModule
    {
        MicLocalizationSystem.LangParam lng = new MicLocalizationSystem.LangParam();
        public void SetParam(CommandData data)
        {
            if (data.Name == "LoadLangParamFromFile")
            {
               lng = MicLocalizationSystem.LocalizationSettings.LoadLangParamFromFile(MicLocalizationSystem.LocalizationSettings.GetLangName(data.Params["NameLangFile"], data.Params["Path"]), data.Params["Path"]);
            }
            if (data.Name == "PrintLocalized")
            {
                Console.WriteLine(lng.GetLangText(data.Params["Name"]));
            }
        }
    }
}
