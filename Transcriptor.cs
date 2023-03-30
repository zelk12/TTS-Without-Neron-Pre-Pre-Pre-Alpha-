using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTSWithoutNeron
{
    internal class Transcriptor
    {
        /// <summary>
        /// Этап который выполняет транскриптор:
        /// 0 - загрузка данных и формирование словаря, 1 - транскрипция заданного текста.
        /// </summary>
        private byte state = 0;

        private phoneticDictionary phoneticDictionary = new phoneticDictionary();

        public string PathToPhoneticDictionaryFile;

        /// <summary>
        /// Производит принудительное извлечение фонетического словаря.
        /// </summary>
        public void ForcedExtract()
        {
            phoneticDictionary.Extract(PathToPhoneticDictionaryFile);
        }
    }
}