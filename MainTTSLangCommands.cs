using System.Collections.Generic;

namespace TTSWithoutNeron
{
    internal class MainTTSLangCommands
    {
        #region Commands name

        public static class CommandsName
        {
            public static string Name { get; private set; } = "-name";
            public static string Sound { get; private set; } = "-sound";
            public static string Arg { get; private set; } = "-arg";
            public static string Glossary { get; private set; } = "-glossary";
        }

        #endregion Commands name

        #region All name in one argument

        public static string[] AllNames { get; private set; } = new string[] { CommandsName.Name, CommandsName.Sound, CommandsName.Arg, CommandsName.Glossary };

        #endregion All name in one argument

        #region Commands

        public static object CheckCommand(string comandName, List<string> line = null)
        {
            if (comandName.Equals(CommandsName.Name))
            {
                return GetName(line);
            }
            else if (comandName.Equals(CommandsName.Sound))
            {
            }
            else if (comandName.Equals(CommandsName.Arg))
            {
            }
            else if (comandName.Equals(CommandsName.Glossary))
            {
            }

            return null;
        }

        public static string GetName(List<string> line)
        {
            if (line != null)
            {
                int test = line.IndexOf(CommandsName.Name);

                if (line.Count > 1)
                {
                    return line[test + 1];
                }
            }
            return "";
        }

        #endregion Commands
    }
}