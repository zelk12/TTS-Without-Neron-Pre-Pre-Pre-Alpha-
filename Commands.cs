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

            { "CharsToSound", "-charsToSound" },

            { "SoundToSound", "-soundToSound" },
            { "Variable", "-variable" },
            { "Glossary", "-glossary" },
        };

        public static class Names
        {
            public const string Name = "Name";
            public const string CharsToSound = "CharsToSound";
            public const string SoundToSound = "SoundToSound";
            public const string Variable = "Variable";
            public const string Glossary = "Glossary";
        }
    }
}