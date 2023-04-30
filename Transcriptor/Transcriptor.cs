using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Transcriptor
{
    public class Transcriptor
    {
        /// <summary>
        /// Фонетический словарь используемый транскриптором.
        /// </summary>
        private PhoneticDictionary phoneticDictionary = new PhoneticDictionary();

        /// <summary>
        /// Путь до локального места хранения словаря.
        /// </summary>
        public string PathToPhoneticDictionaryFile;

        /// <summary>
        /// Производит принудительное создание словаря с удалением предыдущего словаря.
        /// </summary>
        public void DictionaryCreate()
        {
            if (phoneticDictionary != null)
            {
                phoneticDictionary = null;
            }
            phoneticDictionary = new PhoneticDictionary();
            phoneticDictionary.PreparationVariable(PathToPhoneticDictionaryFile);
        }

        /// <summary>
        /// Транскрибирует входной текст.
        /// </summary>
        /// <param name="inputText">Входной текст.</param>
        /// <returns></returns>
        public string Transcript(string inputText)
        {
            string result = WordTranscript(inputText);
            result = LetterTranscript(result);

            return result;
        }

        /// <summary>
        /// Транскрибирует на основе библиотеки слов.
        /// </summary>
        /// <param name="inputText">Входной текст.</param>
        /// <returns></returns>
        private string WordTranscript(string inputText)
        {
            string result = inputText;

            WordTranscriptionInstructionClass wordsInstruction = phoneticDictionary.WordTranscriptionInstruction;
            Dictionary<string, string>.KeyCollection words = wordsInstruction.V_Dictionary.Keys;

            foreach (var word in words)
            {
                string transript = phoneticDictionary.WordTranscriptionInstruction.V_Dictionary[word];
                @result = Regex.Replace(@result, $@"{word}\b", transript, RegexOptions.IgnoreCase);
            }

            return result;
        }

        /// <summary>
        /// Транскрибирует символы и их сочетания.
        /// </summary>
        /// <param name="inputText">Входящий текст</param>
        /// <returns></returns>
        private string LetterTranscript(string inputText)
        {
            string result = inputText;

            LettersTranscriptionInstructionClass lettersInstruction = phoneticDictionary.LettersTranscriptionInstruction;
            Dictionary<string, string[]>.KeyCollection sounds = lettersInstruction.V_Dictionary.Keys;

            VariableDictionaryClass lettersVariable = phoneticDictionary.Variables;
            Dictionary<string, string[]>.KeyCollection variablesName = phoneticDictionary.Variables.V_Dictionary.Keys;

            foreach (var sound in sounds)
            {
                string[] instructions = phoneticDictionary.LettersTranscriptionInstruction.V_Dictionary[sound];

                for (int i = 0; i < instructions.Length; i++)
                {
                    string instuction = instructions[i];
                    foreach (string variableName in variablesName)
                    {
                        string[] variableParametrsAndValue = lettersVariable.V_Dictionary[variableName];

                        RegexOptions options = SetRegexOptions(variableParametrsAndValue[0]);

                        instuction = Regex.Replace(@instuction, $@"\(\?\&{variableName}\)", variableParametrsAndValue[1]);

                        result = Regex.Replace(inputText, instuction, sound, options);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Переводит символьное отображение параметров в RegexOptions
        /// </summary>
        /// <param name="paramerts">Символьное представление параметров</param>
        /// <returns></returns>
        private RegexOptions SetRegexOptions(string paramerts)
        {
            RegexOptions options = new RegexOptions();

            foreach (var paramert in paramerts)
            {
                switch (paramert)
                {
                    case 'i':
                        options |= RegexOptions.IgnoreCase;
                        break;

                    case 'm':
                        options |= RegexOptions.Multiline;
                        break;

                    case 's':
                        options |= RegexOptions.Singleline;
                        break;

                    case 'n':
                        options |= RegexOptions.ExplicitCapture;
                        break;

                    case 'x':
                        options |= RegexOptions.IgnorePatternWhitespace;
                        break;

                    case 'R':
                        options |= RegexOptions.RightToLeft;
                        break;

                    default:
                        break;
                }
            }

            return options;
        }
    }
}