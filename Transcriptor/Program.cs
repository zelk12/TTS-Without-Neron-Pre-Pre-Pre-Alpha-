using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTSWithoutNeron.Transcriptor.User_Interface;

namespace Transcriptor
{
    internal static class Program
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

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UserInterface());
        }
    }
}