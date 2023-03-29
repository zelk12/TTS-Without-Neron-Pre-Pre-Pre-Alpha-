using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TTSWithoutNeron
{
    /// <summary>
    /// Класс транскриптора.
    /// </summary>
    internal partial class Transcriptor
    {
        /// <summary>
        /// Словарь транскриптора.
        /// Содержит имя языка и инструкции о создании транскрипций.
        /// </summary>
        public partial class LanguageDictionary
        {
            /// <summary>
            /// Название языка.
            /// </summary>
            public StandartClass.LanguageName Name { get; private set; } = new StandartClass.LanguageName();

            private StandartClass.Dictionarys dictionary = new StandartClass.Dictionarys();

            /// <summary>
            /// Метод первоначального запуска языка,
            /// -Загружает файл языка
            /// -Создает массив с делением этого файла по строкам
            /// -Создает словари языка, которые используются при работе
            /// </summary>
            /// <param name="_path">Путь к директории где храниться файл языка.</param>
            public void Load(string _path = "")
            {
                StreamReader langFile = new StreamReader(_path);

                string langText = langFile.ReadToEnd();
                string[] langTextLines = textToTextLines(langText);

                Name.Find(langTextLines);
                dictionary.Create(langTextLines);
            }

            /// <summary>
            /// Из единого текстового файла с переносами,
            /// создает массив с делением на отдельные строк.
            /// </summary>
            /// <param name="_input">Текстовый файл где есть переносы по строкам.</param>
            /// <returns>Массив строк из файла.</returns>
            private static string[] textToTextLines(string _input)
            {
                string pattern = @"(\r\n)+";
                string substitution = "\n";

                Regex regex = new Regex(pattern);
                string result = regex.Replace(_input, substitution);

                pattern = @"(,|\n)//.*?(?=(\n|,))";
                substitution = "";
                _input = result;

                regex = new Regex(pattern);
                result = regex.Replace(_input, substitution);

                string[] _langTextSplited = result.Split(new char[] { '\n' });

                return _langTextSplited;
            }

            public class StandartClass
            {
                /// <summary>
                /// Класс обозначающий имя файла
                /// </summary>
                public class LanguageName
                {
                    private static string value = null;

                    public string get()
                    {
                        return value != null ? value : null;
                    }

                    public void set(string _value)
                    {
                        value = _value;
                    }

                    /// <summary>
                    /// Ищет в массиве строк имя языка.
                    /// </summary>
                    /// <param name="_langTextLines">Массив строк языкового файла.</param>
                    public void Find(string[] _langTextLines)
                    {
                        foreach (string line in _langTextLines)
                        {
                            bool nameFinded = false;

                            string[] splitedLine = line.Split(',');

                            IEnumerator lines = splitedLine.GetEnumerator();
                            while (lines.MoveNext() && lines.Current != null)
                            {
                                string lineText = lines.Current.ToString().Trim();

                                if (lineText == Constants.Commands[Constants.CommandsNames.Name] && lines.MoveNext())
                                {
                                    value = lines.Current.ToString().Trim();
                                    nameFinded = true;
                                    break;
                                }
                            }

                            if (nameFinded)
                            {
                                break;
                            }
                        }
                    }
                }

                /// <summary>
                /// Словари используемые для основной работы TTS.
                /// </summary>
                public class Dictionarys
                {
                    private Dictionary<string, string> charsAndSound = new Dictionary<string, string>();
                    private Dictionary<string, string> soundAndSound = new Dictionary<string, string>();

                    private Dictionary<string[], string> variable = new Dictionary<string[], string>();
                    private Dictionary<string, string> wordAndTranscription = new Dictionary<string, string>();

                    /// <summary>
                    /// Создает и заполняет словари.
                    /// </summary>
                    /// <param name="_langTextLines">Массив строк языкового файла.</param>
                    public void Create(string[] _langTextLines)
                    {
                        int state = -1;
                        Dictionary<string, int> switchStates = new Dictionary<string, int>{
                            { Constants.Commands[Constants.CommandsNames.CharsToSound], 0},
                            { Constants.Commands[Constants.CommandsNames.SoundToSound], 1},
                            { Constants.Commands[Constants.CommandsNames.Variable], 2},
                            { Constants.Commands[Constants.CommandsNames.Glossary], 3},
                        };

                        for (int _lineNumber = 0; _lineNumber < _langTextLines.Length; _lineNumber++)
                        {
                            string _line = _langTextLines[_lineNumber];
                            if (_line.Contains('-'))
                            {
                                if (!switchStates.TryGetValue(_line, out state))
                                {
                                    state = -1;
                                }
                            }
                            else
                            {
                                switch (state)
                                {
                                    /// <summary>
                                    /// Заполняет словарь сочетанием символов и создаваемыми ими звуками.
                                    /// </summary>
                                    case 0:
                                        RegexOptions options = RegexOptions.IgnoreCase;

                                        string tempLine = Regex.Replace(@_line, @"(,(?=>)|(?<=>),|,$)", "", options);
                                        string[] tempGlossary = tempLine.Split('>');
                                        string tempNewCharData = tempGlossary[0].Replace(',', '|');

                                        charsAndSound.Add(tempNewCharData, tempGlossary[1]);

                                        tempLine = null;
                                        tempGlossary = null;
                                        tempNewCharData = null;
                                        break;

                                    /// <summary>
                                    /// Заполняет словарь сочетанием звуков и создаваемыми ими в сумме звуками.
                                    /// </summary>
                                    case 1:
                                        tempLine = Regex.Replace(@_line, @"(,(?=>)|(?<=>),|,$)", "");
                                        tempGlossary = tempLine.Split('>');
                                        string tempNewSoundData = tempGlossary[0].Replace(',', '|');

                                        soundAndSound.Add(tempNewSoundData, tempGlossary[1]);

                                        tempLine = null;
                                        tempGlossary = null;
                                        tempNewCharData = null;
                                        break;

                                    /// <summary>
                                    /// Заполняет словарь существующими переменными и их значениями.
                                    /// </summary>
                                    case 2:
                                        options = RegexOptions.IgnoreCase;

                                        string tempName = Regex.Match(@_line, "(?<=\\?').*(?=')", options).Value;
                                        string tempParametrs = Regex.Match(@_line, "(?<=\\(\\?)[gimsnxrN]*(?=\\))").Value;
                                        string tempData = Regex.Match(@_line, "(?<=\\)).*(?=\\))", options).Value;

                                        variable.Add(new string[] { tempName, tempParametrs }, tempData);
                                        break;

                                    /// <summary>
                                    /// Заполняет словарь словами и их транскрипциями.
                                    /// </summary>
                                    case 3:
                                        options = RegexOptions.IgnoreCase;

                                        string tempText = Regex.Replace(@_line, @"(,(?=>)|(?<=>),|,$)", "", options);
                                        tempGlossary = tempText.Split('>');

                                        wordAndTranscription.Add(tempGlossary[0], tempGlossary[1]);
                                        break;

                                    /// <summary>
                                    /// Пишет что данный вид состояния не найден.
                                    /// </summary>
                                    default:
                                        Debug.WriteLine("State " + state.ToString() + " not found.");
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            /// <summary>
            /// Создает транскрипцию текста.
            /// </summary>
            /// <param name="Text">Текст, транскрипцию которого нужно создать.</param>
            /// <returns></returns>
            public string Transcript(string Text)
            {
                return "N/A";
            }
        }
    }
}