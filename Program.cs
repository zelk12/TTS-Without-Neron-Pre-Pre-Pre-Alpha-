using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSWithoutNeron.lang;

namespace TTSWithoutNeron
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TextToPhonem mainTextToPhonem = new TextToPhonem("D:\\PET\\TextToSpeach\\TTSWithoutNeron\\lang\\en.csv");
            mainTextToPhonem.Translate("Hello world!");
        }
    }
}
