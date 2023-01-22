using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TTSWithoutNeron.lang
{
    internal class TextToPhonem
    {
        private string _langFileName = "Unnamed";
        private StreamReader _langFile;
        private List<List<string>> _langFileText = new List<List<string>>();

        #region main file

        public TextToPhonem(string filePath)
        {
            _langFile = MainTTSMethods.GetLangFile(filePath);
            Debug.WriteLine("file connected");

            DateTime startTime = DateTime.Now;

            //clear data
            int i = 0;
            while (DateTime.Now.TimeOfDay.TotalSeconds - startTime.TimeOfDay.TotalSeconds < 60)
            {
                try
                {
                    _langFileText.Add(_langFile.ReadLine().Split(',').ToList<string>());
                }
                catch
                {
                    break;
                }

                for (int j = 0; j < _langFileText[i].Count; j++)
                {
                    if (_langFileText[i][j].Contains("//") || _langFileText[i][j].Equals(""))
                    {
                        _langFileText[i].RemoveAt(j);
                    }

                    if (_langFileText[i].Count == 0)
                    {
                        _langFileText.RemoveAt(i);
                        i--;
                    }
                }

                i++;
            }

            //Comamnd check
            for (i = 0; i < _langFileText.Count; i++)
            {
                for (int j = 0; j < _langFileText[i].Count; j++)
                {
                    if (_langFileText[i][j][0].Equals('-'))
                    {
                        _langFileName = MainTTSLangCommands.CheckCommand(_langFileText[i][j], _langFileText[i]).ToString();
                    }
                }
            }
        }

        #endregion main file

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
            //Console.ReadKey();
        }
    }
}