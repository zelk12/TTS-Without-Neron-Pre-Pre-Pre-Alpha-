using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSWithoutNeron
{
    internal class MainTTSMethods
    {
        public static StreamReader GetLangFile(string pathToFile)
        {
            return new StreamReader(pathToFile);
        }
    }
}
