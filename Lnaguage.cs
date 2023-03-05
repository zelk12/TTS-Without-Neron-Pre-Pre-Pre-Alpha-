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
    internal partial class Lnaguage
    {
        public partial class ForWork
        {
            private StandartClass.Name _name = new StandartClass.Name();
            private StandartClass.Dictionarys _dictionary = new StandartClass.Dictionarys();

            public void Load(string path = "")
            {
                StreamReader _langFile = new StreamReader(path);

                string _langText = _langFile.ReadToEnd();
                string[] _langTextLines = textToTextLines(_langText);

                _name.Find(_langTextLines);
                _dictionary.Create(_langTextLines);
            }

            private static string[] textToTextLines(string input)
            {
                string pattern = @"(\r\n)+";
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

            private class StandartClass
            {
                public class Name
                {
                    private static string _value = "N/A";

                    public string get()
                    {
                        return _value != null ? _value : "N/A";
                    }

                    public void set(string newValue)
                    {
                        _value = newValue;
                    }

                    public void Find(string[] _langTextLines)
                    {
                        foreach (string line in _langTextLines)
                        {
                            string[] _splitedLine = line.Split(',');

                            IEnumerator _lines = _splitedLine.GetEnumerator();
                            while (_lines.MoveNext() && _lines.Current != null)
                            {
                                string _lineText = _lines.Current.ToString().Trim();

                                if (_lineText == Commands.get[Commands.Names.Name] && _lines.MoveNext())
                                {
                                    _value = _lines.Current.ToString().Trim();
                                }
                            }
                        }
                    }
                }

                public class Dictionarys
                {
                    private Dictionary<string, string> _charsAndSound = new Dictionary<string, string>();
                    private Dictionary<string, string> _soundAndSound = new Dictionary<string, string>();

                    private Dictionary<string, string> _variable = new Dictionary<string, string>();
                    private Dictionary<string, string> _wordAndTranscription = new Dictionary<string, string>();

                    public void Create(string[] _langTextLines)
                    {
                        int state = -1;
                        Dictionary<string, int> _switchStates = new Dictionary<string, int>{
                            { Commands.get[Commands.Names.CharsToSound], 0},
                            { Commands.get[Commands.Names.SoundToSound], 1},
                            { Commands.get[Commands.Names.Variable], 2},
                            { Commands.get[Commands.Names.Glossary], 3},
                        };

                        for (int _lineNumber = 0; _lineNumber < _langTextLines.Length; _lineNumber++)
                        {
                            string _line = _langTextLines[_lineNumber];
                            if (_line.Contains('-'))
                            {
                                if (!_switchStates.TryGetValue(_line, out state))
                                {
                                    state = -1;
                                }
                            }
                            else
                            {
                                switch (state)
                                {
                                    case 0:
                                        RegexOptions options = RegexOptions.IgnoreCase;

                                        string _tempLine = Regex.Replace(@_line, @"(,(?=>)|(?<=>),|,$)", "", options);
                                        string[] _tempGlossary = @_tempLine.Split('>');
                                        string _tempNewCharData = _tempGlossary[0].Replace(',', '|');

                                        _charsAndSound.Add(_tempNewCharData, _tempGlossary[1]);
                                        break;

                                    case 1:
                                        options = RegexOptions.IgnoreCase;

                                        _tempLine = Regex.Replace(@_line, @"(,(?=>)|(?<=>),|,$)", "", options);
                                        _tempGlossary = @_tempLine.Split('>');
                                        string _tempNewSoundData = _tempGlossary[0].Replace(',', '|');

                                        _soundAndSound.Add(_tempNewSoundData, _tempGlossary[1]);
                                        break;

                                    case 2:
                                        options = RegexOptions.IgnoreCase;

                                        string _tempName = Regex.Match(@_line, @"(?<=').*(?=')", options).Value;
                                        string _tempData = Regex.Match(@_line, @"(?<='.*').*(?!')(?=\))", options).Value;

                                        _variable.Add(_tempName, _tempData);
                                        break;

                                    case 3:
                                        options = RegexOptions.IgnoreCase;

                                        string _tempText = Regex.Replace(@_line, @"(,(?=>)|(?<=>),|,$)", "", options);
                                        _tempGlossary = @_tempText.Split('>');

                                        _wordAndTranscription.Add(_tempGlossary[0], _tempGlossary[1]);
                                        break;

                                    default:
                                        Debug.WriteLine("State " + state.ToString() + " not found.");
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}