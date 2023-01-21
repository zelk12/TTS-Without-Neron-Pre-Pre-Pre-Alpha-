using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSWithoutNeron
{
    internal class MainTTSLangCommands
    {
        static public string name { get; private set; } = "-name:";
        static public string sound { get; private set; } = "-sound";
        static public string arg { get; private set; } = "-arg";
        static public string glossary { get; private set; } = "-glossary";
    }
}
