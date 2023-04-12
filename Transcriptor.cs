using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// Производит принудительное создание словаря.
        /// </summary>
        public void ForcedDictyonaryCreate()
        {
            phoneticDictionary.PreparationVariable(PathToPhoneticDictionaryFile);
        }
    }
}