using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TTSWithoutNeron.Transcriptor
{
    /// <summary>
    /// Класс фонетических словарей, хранит в себе данные словаря.
    /// Позволяет загрузить и извлечь данные из локального словаря
    /// </summary>
    internal class PhoneticDictionary
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
#if DEBUG
            Debug.WriteLine("Extract starting");
#endif
            string fullPhoneticDictionaryData = File.ReadAllText(path);

            fullPhoneticDictionaryData = Regex.Replace(fullPhoneticDictionaryData, "//.*?(,|$|\n|\n\r|\r\n)", "");
            fullPhoneticDictionaryData = Regex.Replace(fullPhoneticDictionaryData, "^(\n|\r\n|\n\r)(|,)", "", RegexOptions.Multiline);

#if DEBUG
            Debug.WriteLine("Extract Done");
#endif

            return Regex.Matches(fullPhoneticDictionaryData, "-.+((\n[^-].+)+|$)", RegexOptions.Multiline);
        }

        /// <summary>
        /// Помещает полученый словарь в переменные.
        /// </summary>
        /// <param name="dictionaryData">Данные фонетического словаря.</param>
        private void DictionaryDataSortToVariable(MatchCollection dictionaryData)
        {
#if DEBUG
            Debug.WriteLine("Variable sort starting");
#endif
            for (int i = 0; i < dictionaryData.Count; i++)
            {
                Match confirmedCommand = Regex.Match(dictionaryData[i].Value, "-[A-z]+");

                switch (confirmedCommand.Value)
                {
                    case Constant.PhoneticDictionaryCommands.Name:

                        NameExtract(dictionaryData, i);
                        break;

                    case Constant.PhoneticDictionaryCommands.LettersTranscriptionInstruction:

                        LettersTranscriptionInstructionExtract(dictionaryData, i);
                        break;

                    case Constant.PhoneticDictionaryCommands.SoundMergeInstruction:

                        SoundMergeInstructionExctract(dictionaryData, i);
                        break;

                    case Constant.PhoneticDictionaryCommands.WordTranscriptionInstruction:

                        WordTranscriptionInstructionExtract(dictionaryData, i);
                        break;

                    case Constant.PhoneticDictionaryCommands.VariableDictionary:

                        VariableExtract(dictionaryData, i);
                        break;

                    default:
                        Debug.Fail("Unknown command.");
                        break;
                }
            }
#if DEBUG
            Debug.WriteLine("Variable sort done");
#endif
        }

        private void NameExtract(MatchCollection dictionaryData, int dataNumber)
        {
#if DEBUG
            Debug.WriteLine("Name searching");
#endif
            string[] textColumns = dictionaryData[dataNumber].Value.Split(',');
            string name = textColumns[1];
            LanguageName = name;
#if DEBUG
            Debug.WriteLine("Name searching Done");
#endif
        }

        private void LettersTranscriptionInstructionExtract(MatchCollection dictionaryData, int i)
        {
#if DEBUG
            Debug.WriteLine("Letters transcription instruction searching");
#endif
            string[] textLines = dictionaryData[i].Value.Split('\n');

            for (int textLineNumber = 1; textLineNumber < textLines.Length; textLineNumber++)
            {
                string textLine = textLines[textLineNumber];
#if DEBUG
                Debug.WriteLine($"working line: {textLine}");
#endif
                textLine = Regex.Replace(textLine, "\r|,\r", "");

                List<string> lettersBatch = new List<string>();
                string sound = "N/A";

                string[] textColumns = textLine.Split(',');

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

                LettersTranscriptionInstruction.Add(sound, lettersBatch.ToArray());
            }
#if DEBUG
            Debug.WriteLine("Letters transcription instruction searching Done");
#endif
        }

        private void SoundMergeInstructionExctract(MatchCollection dictionaryData, int i)
        {
#if DEBUG
            Debug.WriteLine("Sound merge instruction searching");
#endif
            string[] textLines = dictionaryData[i].Value.Split('\n');

            for (int textLineNumber = 1; textLineNumber < textLines.Length; textLineNumber++)
            {
                string textLine = textLines[textLineNumber];
#if DEBUG
                Debug.WriteLine($"working line: {textLine}");
#endif
                textLine = Regex.Replace(textLine, "\r|,\r", "");

                List<string> mergedSoundBatch = new List<string>();
                string sound = "N/A";

                string[] textColumns = textLine.Split(',');

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

                SoundMergeInstruction.Add(sound, mergedSoundBatch.ToArray());
                textLines = null;
            }
#if DEBUG
            Debug.WriteLine("Sound merge instruction searching Done");
#endif
        }

        private void WordTranscriptionInstructionExtract(MatchCollection dictionaryData, int i)
        {
#if DEBUG
            Debug.WriteLine("Word transcription instruction searching");
#endif
            string[] textLines = dictionaryData[i].Value.Split('\n');

            for (int textLineNumber = 1; textLineNumber < textLines.Length; textLineNumber++)
            {
                string textLine = textLines[textLineNumber];
#if DEBUG
                Debug.WriteLine($"working line: {textLine}");
#endif
                textLine = Regex.Replace(textLine, "\r|,\r", "");

                string word = "N/A";
                string transcription = "N/A";

                string[] textColumns = textLine.Split(',');

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

#if DEBUG
            Debug.WriteLine("Word transcription instruction searching Done");
#endif
        }

        private void VariableExtract(MatchCollection dictionaryData, int i)
        {
#if DEBUG
            Debug.WriteLine("Variable searching");
#endif
            string[] textLines = dictionaryData[i].Value.Split('\n');

            for (int textLineNumber = 1; textLineNumber < textLines.Length; textLineNumber++)
            {
                string textLine = textLines[textLineNumber];
#if DEBUG
                Debug.WriteLine($"working line: {textLine}");
#endif
                textLine = Regex.Replace(textLine, "\r|,\r", "");

                string vriableName = "N/A";
                string modificators = "N/A";
                string value = "N/A";

                vriableName = Regex.Match(textLine, @"(?<=\\?').+(?=')").Value;
                value = Regex.Match(textLine, "(?<=\\')\\(.+(?=\\))").Value;

                Variables.Add(vriableName, value);
            }
#if DEBUG
            Debug.WriteLine("Variable searching Done");
#endif
        }
    }
}