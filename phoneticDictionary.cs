using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TTSWithoutNeron
{
    /// <summary>
    /// Класс фонетических словарей, хранит в себе данные словаря.
    /// Позволяет загрузить и извлечь данные из локального словаря
    /// </summary>
    internal class phoneticDictionary
    {
        /// <summary>
        /// Имя языка для которого создан словарь
        /// </summary>
        public string LanguageName;

        /// <summary>
        /// Набор инструкций для перевода букв и их сочетаний в звуки.
        /// </summary>
        public int LettersTranscripInstruction;

        /// <summary>
        /// Набор инструкций для слияния звуков воедино.
        /// </summary>
        public int SoundMergeInstruction;

        /// <summary>
        /// Набор слов и их транскрипций.
        /// </summary>
        public int WordTranscriptInstruction;

        /// <summary>
        /// Загружает словарь в программу и извлекает из словаря данные, записывая их в свой массив.
        /// </summary>
        /// <param name="path">Путь до места хранения словаря.</param>
        public void Extract(string path)
        {
            string fullPhoneticDictionaryData_temp = File.ReadAllText(path);

            fullPhoneticDictionaryData_temp = Regex.Replace(fullPhoneticDictionaryData_temp, "//.*?(,|$|\n|\n\r|\r\n)", "");
            fullPhoneticDictionaryData_temp = Regex.Replace(fullPhoneticDictionaryData_temp, "^(\n|\r\n|\n\r)(|,)", "", RegexOptions.Multiline);

            var test = Regex.Split(fullPhoneticDictionaryData_temp, "-");
        }
    }
}