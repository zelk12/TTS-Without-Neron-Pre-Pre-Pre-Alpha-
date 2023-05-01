using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace TTSWithoutNeron.Transcriptor
{
    public static class Transcriptor
    {
        /// <summary>
        /// Фонетический словарь используемый транскриптором.
        /// </summary>
        private static PhoneticDictionary phoneticDictionary = new PhoneticDictionary();

        /// <summary>
        /// Путь до локального места хранения словаря.
        /// </summary>
        private static string PathToPhoneticDictionaryFile;

        /// <summary>
        /// Производит принудительное создание словаря с удалением предыдущего словаря.
        /// </summary>
        private static void DictionaryCreate()
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
        private static string Transcript(string inputText)
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
        private static string WordTranscript(string inputText)
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
        private static string LetterTranscript(string inputText)
        {
            string result = inputText;

            LettersTranscriptionInstructionClass lettersInstruction = phoneticDictionary.LettersTranscriptionInstruction;

            string[] sounds = lettersInstruction.V_Dictionary.Keys.ToArray();
            string[][] transcriptInstructions = lettersInstruction.V_Dictionary.Values.ToArray();

            string[] variablesName = phoneticDictionary.Variables.V_Dictionary.Keys.ToArray();
            string[] variablesValue = phoneticDictionary.Variables.V_Dictionary.Values.ToArray();

            for (int soundNumber = 0; soundNumber < sounds.Length; soundNumber++)
            {
                for (int instructionNumber = 0; instructionNumber < transcriptInstructions[soundNumber].Length; instructionNumber++)
                {
                    for (int variablesNumber = 0; variablesNumber < variablesValue.Length; variablesNumber++)
                    {
                        string transcriptInstuction = transcriptInstructions[soundNumber][instructionNumber];
                        bool variableFinded = Regex.IsMatch(transcriptInstuction, $@"\(\?\&{variablesName[variablesNumber]}\)");
                        if (variableFinded)
                        {
                            transcriptInstuction = Regex.Replace(transcriptInstuction, $@"\(\?\&{variablesName[variablesNumber]}\)", variablesValue[variablesNumber]);
                            result = Regex.Replace(result, transcriptInstuction, sounds[soundNumber]);
                        }
                        
                    }
                }
            }

            return result;
        }

        public static void Launch(string path)
        {
            PathToPhoneticDictionaryFile = path;
            DictionaryCreate();
        }

        public static string TranscriptCreate(string text)
        {
            return Transcript(text);
        }
    }
}