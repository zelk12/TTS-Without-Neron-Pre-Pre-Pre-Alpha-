using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public int LettersTranscriptionInstruction;

        /// <summary>
        /// Набор инструкций для слияния звуков воедино.
        /// </summary>
        public string SoundMergeInstruction;

        /// <summary>
        /// Набор слов и их транскрипций.
        /// </summary>
        public string WordTranscriptionInstruction;

        /// <summary>
        /// Хранит переменные..
        /// Формат (название, модификаторы, значение)
        /// </summary>
        public List<List<List<string>>> VariableDictionary = new List<List<List<string>>>();

        /// <summary>
        /// Загружает словарь в программу и извлекает из словаря данные.
        /// Помещает полученый словарь в переменные.
        /// </summary>
        /// <param name="path">Путь до места хранения словаря.</param>
        public void PreparationVariable(string path)
        {
            MatchCollection tempDictionaryData = Extract(path);
            DictionaryDataSortToVariable(tempDictionaryData);
        }

        /// <summary>
        /// Загружает словарь в программу и извлекает из словаря данные.
        /// </summary>
        /// <param name="path">Путь до места хранения словаря.</param>
        private MatchCollection Extract(string path)
        {
            string fullPhoneticDictionaryData = File.ReadAllText(path);

            fullPhoneticDictionaryData = Regex.Replace(fullPhoneticDictionaryData, "//.*?(,|$|\n|\n\r|\r\n)", "");
            fullPhoneticDictionaryData = Regex.Replace(fullPhoneticDictionaryData, "^(\n|\r\n|\n\r)(|,)", "", RegexOptions.Multiline);

            return Regex.Matches(fullPhoneticDictionaryData, "-.+((\n[^-].+)+|$)", RegexOptions.Multiline);
        }

        /// <summary>
        /// Помещает полученый словарь в переменные.
        /// </summary>
        /// <param name="dictionaryData">Данные фонетического словаря.</param>
        private void DictionaryDataSortToVariable(MatchCollection dictionaryData)
        {
            for (int i = 0; i < dictionaryData.Count; i++)
            {
                Match confirmedCommand = Regex.Match(dictionaryData[i].Value, "-.+");

                switch (confirmedCommand.Value)
                {
                    case Constant.PhoneticDictionaryCommands.Name:
                        break;

                    case Constant.PhoneticDictionaryCommands.LettersTranscriptionInstruction:
                        break;

                    case Constant.PhoneticDictionaryCommands.WordTranscriptionInstruction:
                        break;

                    case Constant.PhoneticDictionaryCommands.VariableDictionary:
                        break;

                    default:
                        Debug.Fail("Unknown command.");
                        break;
                }
            }
        }
    }
}