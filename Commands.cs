using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSWithoutNeron
{
    internal static class Commands
    {
        public static Dictionary<string, string> get = new Dictionary<string, string>()
        {
            { "Name", "-name" },
            { "WordToSound", "-wordToSound" },
            { "variable", "-variable" },
            { "Glossary", "-glossary" },
        };
    }
}