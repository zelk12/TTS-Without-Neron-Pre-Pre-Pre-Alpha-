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
        public string LanguageName = "N/A";

        /// <summary>
        /// Набор инструкций для перевода букв и их сочетаний в звуки.
        /// </summary>
        public LettersTranscriptionInstructionClass LettersTranscriptionInstruction = new LettersTranscriptionInstructionClass();

        /// <summary>
        /// Набор инструкций для слияния звуков воедино.
        /// </summary>
        public SoundMergeInstructionClass SoundMergeInstruction = new SoundMergeInstructionClass();

        /// <summary>
        /// Набор слов и их транскрипций.
        /// </summary>
        public WordTranscriptionInstructionClass WordTranscriptionInstruction = new WordTranscriptionInstructionClass();

        /// <summary>
        /// Хранит переменные..
        /// Формат (название, модификаторы, значение)
        /// </summary>
        public VariableDictionaryClass Variables = new VariableDictionaryClass();

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
                Match confirmedCommand = Regex.Match(dictionaryData[i].Value, "-[A-z]+");

                switch (confirmedCommand.Value)
                {
                    case Constant.PhoneticDictionaryCommands.Name:

                        #region Name

                        string[] textColumns = dictionaryData[i].Value.Split(',');
                        string name = textColumns[1];
                        LanguageName = name;

                        textColumns = null;
                        name = null;

                        #endregion Name

                        break;

                    case Constant.PhoneticDictionaryCommands.LettersTranscriptionInstruction:

                        #region LetterToSound

                        string[] textLines = dictionaryData[i].Value.Split('\n');

                        for (int textLineNumber = 1; textLineNumber < textLines.Length; textLineNumber++)
                        {
                            string textLine = textLines[textLineNumber];
                            textLine = Regex.Replace(textLine, "\r|,\r", "");

                            List<string> lettersBatch = new List<string>();
                            string sound = "N/A";

                            textColumns = textLine.Split(',');

                            bool thisLettersIs = true;
                            for (int textColumnNumber = 0; textColumnNumber < textColumns.Length; textColumnNumber++)
                            {
                                string textColumn = textColumns[textColumnNumber];

                                if (textColumn != ">" & thisLettersIs)
                                {
                                    lettersBatch.Add(textColumn);
                                }
                                else if (textColumn != ">")
                                {
                                    sound = textColumn;
                                }
                                else
                                {
                                    thisLettersIs = false;
                                }
                            }

                            LettersTranscriptionInstruction.Add(sound, lettersBatch);
                        }

                        textColumns = null;
                        textLines = null;

                        #endregion LetterToSound

                        break;

                    case Constant.PhoneticDictionaryCommands.SoundMergeInstruction:

                        #region SoundMerger

                        textLines = dictionaryData[i].Value.Split('\n');

                        for (int textLineNumber = 1; textLineNumber < textLines.Length; textLineNumber++)
                        {
                            string textLine = textLines[textLineNumber];
                            textLine = Regex.Replace(textLine, "\r|,\r", "");

                            List<string> mergedSoundBatch = new List<string>();
                            string sound = "N/A";

                            textColumns = textLine.Split(',');

                            bool thisMergedSounds = true;
                            for (int textColumnNumber = 0; textColumnNumber < textColumns.Length; textColumnNumber++)
                            {
                                string textColumn = textColumns[textColumnNumber];

                                if (textColumn != ">" & thisMergedSounds)
                                {
                                    mergedSoundBatch.Add(textColumn);
                                }
                                else if (textColumn != ">")
                                {
                                    sound = textColumn;
                                }
                                else
                                {
                                    thisMergedSounds = false;
                                }
                            }

                            SoundMergeInstruction.Add(sound, mergedSoundBatch);
                            textLines = null;
                        }

                        textColumns = null;
                        textLines = null;

                        #endregion SoundMerger

                        break;

                    case Constant.PhoneticDictionaryCommands.WordTranscriptionInstruction:

                        #region WordToTranscript

                        textLines = dictionaryData[i].Value.Split('\n');

                        for (int textLineNumber = 1; textLineNumber < textLines.Length; textLineNumber++)
                        {
                            string textLine = textLines[textLineNumber];
                            textLine = Regex.Replace(textLine, "\r|,\r", "");

                            string word = "N/A";
                            string transcription = "N/A";

                            textColumns = textLine.Split(',');

                            bool thisWord = true;
                            for (int textColumnNumber = 0; textColumnNumber < textColumns.Length; textColumnNumber++)
                            {
                                string textColumn = textColumns[textColumnNumber];

                                if (textColumn != ">" & thisWord)
                                {
                                    word = textColumn;
                                }
                                else if (textColumn != ">")
                                {
                                    transcription = textColumn;
                                }
                                else
                                {
                                    thisWord = false;
                                }
                            }

                            WordTranscriptionInstruction.Add(word, transcription);
                        }

                        textColumns = null;
                        textLines = null;

                        #endregion WordToTranscript

                        break;

                    case Constant.PhoneticDictionaryCommands.VariableDictionary:
                        textLines = dictionaryData[i].Value.Split('\n');

                        for (int textLineNumber = 1; textLineNumber < textLines.Length; textLineNumber++)
                        {
                            string textLine = textLines[textLineNumber];
                            textLine = Regex.Replace(textLine, "\r|,\r", "");

                            string vriableName = "N/A";
                            string modificators = "N/A";
                            string value = "N/A";

                            vriableName = Regex.Match(textLine, @"(?<=\\?').+(')").Value;
                            modificators = Regex.Match(textLine, "(?<=\\'\\(\\?).+(?=\\).+)").Value;
                            value = Regex.Match(textLine, "(?<=\\)).+(?=\\))").Value;

                            Variables.Add(vriableName, modificators, value);
                        }

                        textColumns = null;
                        textLines = null;
                        break;

                    default:
                        Debug.Fail("Unknown command.");
                        break;
                }
            }
        }
    }
}