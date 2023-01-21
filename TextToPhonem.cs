using System;

namespace TTSWithoutNeron.lang
{
    internal class TextToPhonem
    {
        string langName = "en";
        static string pathToLang = "/lang";

        public string LangName
        {
            get { return langName; }
            set { langName = value; }
        }

        getLangFile()
        {

        }

        static void Translate(string text)
        {
            Console.WriteLine(text);
        }
    }
}
