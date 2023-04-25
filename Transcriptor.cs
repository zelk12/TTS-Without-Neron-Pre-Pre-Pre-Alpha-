using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TTSWithoutNeron
{
    public class Transcriptor
    {
        /// <summary>
        /// Этап который выполняет транскриптор:
        /// 0 - загрузка данных и формирование словаря, 1 - транскрипция заданного текста.
        /// </summary>
        private byte state = 0;

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

            Dictionary<string, string>.KeyCollection words = phoneticDictionary.WordTranscriptionInstruction.V_Dictionary.Keys;

            foreach (var word in words)
            {
                string transript = phoneticDictionary.WordTranscriptionInstruction.V_Dictionary[word];
                @result = Regex.Replace(@result, $@"{word}\b", transript, RegexOptions.IgnoreCase);
            }

            return result;
        }
    }
}