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
        public Dictionary<string, List<string>> dictionary { get; private set; } = new Dictionary<string, List<string>>();

        /// <summary>
        /// Добавляет инструкцию в список.
        /// </summary>
        /// <param name="sound">Звук получаемый при сочетании.</param>
        /// <param name="letters">Буквы и их сочетания.</param>
        public void Add(string sound, List<string> letters)
        {
            dictionary.Add(sound, letters);
        }

        /// <summary>
        /// Убирает инструкцию из списка.
        /// </summary>
        /// <param name="sound">Звук</param>
        private void Remove(string sound)
        {
            dictionary.Remove(sound);
        }
    }

    /// <summary>
    /// Хранит инструкции как объединять звуки.
    /// </summary>
    internal class SoundMergeInstructionClass
    {
        public Dictionary<string, List<string>> dictionary { get; private set; } = new Dictionary<string, List<string>>();

        /// <summary>
        /// Добавляет инструкцию в список.
        /// </summary>
        /// <param name="sound">Звук.</param>
        /// <param name="sounds">Сочетание звуков.</param>
        public void Add(string sound, List<string> sounds)
        {
            dictionary.Add(sound, sounds);
        }

        /// <summary>
        /// Убирает инструкцию из списка.
        /// </summary>
        /// <param name="sound">Звук</param>
        private void Remove(string sound)
        {
            dictionary.Remove(sound);
        }
    }

    /// <summary>
    /// Список который хранит переменные.
    /// Формат (название, модификаторы, значение).
    /// </summary>
    internal class VariableDictionaryClass
    {
        public Dictionary<string, string[]> dictionary { get; private set; } = new Dictionary<string, string[]>();

        /// <summary>
        /// Добавляет переменную в список переменных.
        /// </summary>
        /// <param name="name">Имя переменной.</param>
        /// <param name="mod">Модификатор ввода.</param>
        /// <param name="value">Значение переменной.</param>
        public void Add(string name, string mod, string value)
        {
            dictionary.Add(name, new string[] { mod, value });
        }

        /// <summary>
        /// Удаляет переменную из списка.
        /// </summary>
        /// <param name="name">Имя переменной.</param>
        public void Remove(string name)
        {
            dictionary.Remove(name);
        }
    }
}