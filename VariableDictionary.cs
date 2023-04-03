using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTSWithoutNeron
{
    /// <summary>
    /// Хранит переменные..
    /// Формат (название, модификаторы, значение)
    /// </summary>
    public class VariableDictionary
    {
        public Dictionary<string, string[]> dictionary { get; private set; } = new Dictionary<string, string[]>();

        public void Add(string name, string mod, string value)
        {
            dictionary.Add(name, new string[] { mod, value });
        }

        public void Remove(string name)
        {
            dictionary.Remove(name);
        }
    }
}