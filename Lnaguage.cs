using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TTSWithoutNeron
{
    internal class Lnaguage
    {
        public class ForWork
        {
            public static class Name
            {
                private static string value;

                public static string get()
                {
                    return value != null ? value : "N/A";
                }

                public static void set(string newValue)
                {
                    value = newValue;
                }

                public static void Find(string[] _langTextLines)
                {
                    foreach (string line in _langTextLines)
                    {
                        string[] _splitedLine = line.Split(',');

                        IEnumerator _lines = _splitedLine.GetEnumerator();
                        while (_lines.MoveNext() && _lines.Current != null)
                        {
                            string _lineText = _lines.Current.ToString().Trim();

                            if (_lineText == Commands.get["Name"] && _lines.MoveNext())
                            {
                                value = _lines.Current.ToString().Trim();
                            }
                        }
                    }
                }
            }

            public void Load(string path = "")
            {
                StreamReader _langFile = new StreamReader(path);

                string _langText = _langFile.ReadToEnd();
                string[] _langTextLines = textToTextLines(_langText);

                Name.Find(_langTextLines);
                @Dictionary.Create(_langTextLines);
            }

            public static class @Dictionary
            {
                public static void Create(string[] _langTextLines)
                {
                }
            }

            private static string[] textToTextLines(string input)
            {
                string pattern = @"\r\n+";
                string substitution = "\n";

                Regex regex = new Regex(pattern);
                string result = regex.Replace(input, substitution);

                pattern = @"(,|\n)//.*?(?=(\n|,))";
                substitution = "";
                input = result;

                regex = new Regex(pattern);
                result = regex.Replace(input, substitution);

                string[] _langTextSplited = result.Split(new char[] { '\n' });

                return _langTextSplited;
            }
        }
    }
}