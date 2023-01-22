using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSWithoutNeron
{
    internal class MainTTSLangCommands
    {
        #region Commands name
        public class CommandsName
        {
            static public string Name { get; private set; } = "-name";
            static public string Sound { get; private set; } = "-sound";
            static public string Arg { get; private set; } = "-arg";
            static public string Glossary { get; private set; } = "-glossary";
        }
        #endregion Commands name

        #region All name in one argument
        static public string[] AllNames { get; private set; } = new string[] { CommandsName.Name, CommandsName.Sound, CommandsName.Arg, CommandsName.Glossary };
        #endregion All name in one argument

        #region Commands
        public static void CheckCommand(string comandName, string line = null)
        {

        }

        #endregion Commands
    }
}
