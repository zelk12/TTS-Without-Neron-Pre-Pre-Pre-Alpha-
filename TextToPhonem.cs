using System;
using System.Diagnostics;
using System.IO;
using TTSWithoutNeron;

namespace TTSWithoutNeron.lang
{
    internal class TextToPhonem
    {
        private StreamReader langFile;

        public TextToPhonem(string filePath)
        {
            langFile = MainTTSMethods.GetLangFile(filePath);
            Debug.WriteLine("file connected");
        }

        private string langName = "en.csv";
        private static string pathToLang = "/lang";

        public string LangName
        {
            get { return langName; }
            set { langName = value; }
        }

        public void Translate(string text)
        {
            Console.WriteLine(text);
            Console.ReadKey();
        }
    }
}
