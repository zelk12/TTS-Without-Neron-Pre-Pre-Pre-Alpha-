using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Transcriptor
{
    internal class Program
    {
        private static Transcriptor transcriptor = new Transcriptor();

        private static void Launch(string path)
        {
            transcriptor.PathToPhoneticDictionaryFile = path;
            transcriptor.DictionaryCreate();
        }

        private static string TranscriptCreate(string text)
        {
            return transcriptor.Transcript(text);
        }

        private static void Main(string[] args)
        {
            Launch("D:\\PET\\TextToSpeach\\TTSWithoutNeron\\Lang\\en.csv");

            string result = TranscriptCreate("It was in July, 1805, and the speaker was the well-known Anna Pavlovna Scherer,\n" +
                " maid of honour and confidante of the Empress Maria Feodorovna. With these words she greeted Prince Vasili Kuragin,\n" +
                " a man of high rank and importance, who was the first to arrive at her reception. Anna Pavlovna had been coughing\n" +
                " for the last few days and in spite of her many years was still attractive; but she was now in mourning. In her\n" +
                " mourning dress, with her gray hair dressed à la victime, she received the guests who were arriving....\n");
        }
    }
}