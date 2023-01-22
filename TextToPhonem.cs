using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TTSWithoutNeron;

namespace TTSWithoutNeron.lang
{
    internal class TextToPhonem
    {
        private string langFileName = "Unnamed";
        private StreamReader langFile;
        private List<List<string>> langFileText = new List<List<string>>();

        #region main file
        public TextToPhonem(string filePath)
        {
            langFile = MainTTSMethods.GetLangFile(filePath);
            Debug.WriteLine("file connected");

            DateTime startTime = DateTime.Now;

            //clear data
            int i = 0;
            while (DateTime.Now.TimeOfDay.TotalSeconds - startTime.TimeOfDay.TotalSeconds < 60)
            {
                try
                {
                    langFileText.Add(langFile.ReadLine().Split(',').ToList<string>());
                }
                catch (Exception)
                {
                    break;
                }

                for (int j = 0; j < langFileText[i].Count; j++)
                {
                    if (langFileText[i][j].Contains("//") || langFileText[i][j].Equals(""))
                    {
                        langFileText[i].RemoveAt(j);
                    }

                    if (langFileText[i].Count == 0)
                    {
                        langFileText.RemoveAt(i);
                        i--;
                    }
                }

                i++;
            }

            //Comamnd check
            for (i = 0; i < langFileText.Count; i++)
            {
                for (int j = 0; j < langFileText[i].Count; j++)
                {
                    if (langFileText[i][j][0].Equals('-'))
                    {
                        foreach (var name in MainTTSLangCommands.AllNames)
                        {
                            if (langFileText[i][j].Equals(name))
                            {
                                if (MainTTSLangCommands.CommandsName.Name.Equals(langFileText[i][j]) && langFileText[i].Count > 1)
                                {
                                    langFileName = langFileText[i][j + 1];
                                }
                            }
                            Debug.WriteLine(name);
                        }
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
