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
            Transcriptor transcriptor = new Transcriptor();

            transcriptor.PathToPhoneticDictionaryFile = "D:\\PET\\TextToSpeach\\TTSWithoutNeron\\lang\\en.csv";

            transcriptor.ForcedExtract();
        }
    }
}