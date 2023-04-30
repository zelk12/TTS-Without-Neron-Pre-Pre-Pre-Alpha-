using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcriptor
{
    internal class Constant
    {
        /// <summary>
        /// Команды содержащиеся в фонетическом словаре.
        /// </summary>
        public static class PhoneticDictionaryCommands
        {
            public const string Name = "-name";
            public const string LettersTranscriptionInstruction = "-lettersToSound";
            public const string SoundMergeInstruction = "-soundMerge";
            public const string WordTranscriptionInstruction = "-glossary";
            public const string VariableDictionary = "-variable";
        }
    }
}