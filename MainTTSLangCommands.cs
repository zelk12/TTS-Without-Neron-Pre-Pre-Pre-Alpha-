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
        static public string Name { get; private set; } = "-name";
        static public string Sound { get; private set; } = "-sound";
        static public string Arg { get; private set; } = "-arg";
        static public string Glossary { get; private set; } = "-glossary";
        #endregion Commands name

        #region All name in one argument
        static public string[] AllNames { get; private set; } = new string[] { Name, Sound, Arg, Glossary };
        #endregion All name in one argument

        #region Commands
        private void CheckCommand(string comandName)
        {

        }

        #endregion Commands
    }
}
