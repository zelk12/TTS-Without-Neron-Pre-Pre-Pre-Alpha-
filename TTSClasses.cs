using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTSWithoutNeron
{
    /// <summary>
    /// Хранит инструкции как объединять буквы в звуки.
    /// Формат (звук, буквы).
    /// </summary>
    internal class LettersTranscriptionInstructionClass
    {
        public Dictionary<string, string[]> V_Dictionary { get; private set; } = new Dictionary<string, string[]>();

        /// <summary>
        /// Добавляет инструкцию в список.
        /// </summary>
        /// <param name="sound">Звук получаемый при сочетании.</param>
        /// <param name="letters">Буквы и их сочетания.</param>
        public void Add(string sound, string[] letters)
        {
            V_Dictionary.Add(sound, letters);
        }

        /// <summary>
        /// Убирает инструкцию из списка.
        /// </summary>
        /// <param name="sound">Звук</param>
        public void Remove(string sound)
        {
            V_Dictionary.Remove(sound);
        }
    }

    /// <summary>
    /// Хранит инструкции как объединять звуки.
    /// </summary>
    internal class SoundMergeInstructionClass
    {
        public Dictionary<string, string[]> V_Dictionary { get; private set; } = new Dictionary<string, string[]>();

        /// <summary>
        /// Добавляет инструкцию в список.
        /// </summary>
        /// <param name="sound">Звук.</param>
        /// <param name="sounds">Сочетание звуков.</param>
        public void Add(string sound, string[] sounds)
        {
            V_Dictionary.Add(sound, sounds);
        }

        /// <summary>
        /// Убирает инструкцию из списка.
        /// </summary>
        /// <param name="sound">Звук</param>
        public void Remove(string sound)
        {
            V_Dictionary.Remove(sound);
        }
    }

    /// <summary>
    /// Хранит слова и их транскрипции
    /// </summary>
    internal class WordTranscriptionInstructionClass
    {
        public Dictionary<string, string> V_Dictionary { get; private set; } = new Dictionary<string, string>();

        public void Add(string word, string transcription)
        {
            V_Dictionary.Add(word, transcription);
        }

        public void Remove(string word)
        {
            V_Dictionary.Remove(word);
        }
    }

    /// <summary>
    /// Список который хранит переменные.
    /// Формат (название, модификаторы, значение).
    /// </summary>
    internal class VariableDictionaryClass
    {
        public Dictionary<string, string[]> V_Dictionary { get; private set; } = new Dictionary<string, string[]>();

        /// <summary>
        /// Добавляет переменную в список переменных.
        /// </summary>
        /// <param name="name">Имя переменной.</param>
        /// <param name="mod">Модификатор ввода.</param>
        /// <param name="value">Значение переменной.</param>
        public void Add(string name, string mod, string value)
        {
            V_Dictionary.Add(name, new string[] { mod, value });
        }

        /// <summary>
        /// Удаляет переменную из списка.
        /// </summary>
        /// <param name="name">Имя переменной.</param>
        public void Remove(string name)
        {
            V_Dictionary.Remove(name);
        }
    }
}