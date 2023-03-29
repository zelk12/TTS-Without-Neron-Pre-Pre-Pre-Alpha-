using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSWithoutNeron
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Transcriptor.LanguageDictionary languageTrancriptInstruction = new Transcriptor.LanguageDictionary();
            languageTrancriptInstruction.Load("D:\\PET\\TextToSpeach\\TTSWithoutNeron\\lang\\en.csv");
        }
    }
}