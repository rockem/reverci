using System;
using System.Collections.Generic;

namespace Reverci.command
{
    internal class CommandCommander
    {
        private readonly Dictionary<eCommandType, ICommand> r_CommandDictionary;

        private static class InstanceHolder
        {
            public static readonly CommandCommander Instance = new CommandCommander();
        }

        private class DummyCommand : ICommand
        {
            public void DoCommand()
            {
            }

            public bool Checked { get; set; }

            public bool Enabled { get; set; }

            public string Text { get; set; }

            public bool Visible { get; set; }

            public event StatusChangedDelegate StatusChanged;
        }

        private CommandCommander()
        {
            r_CommandDictionary = new Dictionary<eCommandType, ICommand>();
            foreach (eCommandType command in Enum.GetValues(typeof(eCommandType)))
            {
                r_CommandDictionary.Add(command, new DummyCommand());
            }
        }

        public void RegisterCommand(eCommandType i_CommandType, ICommand i_Command)
        {
            r_CommandDictionary[i_CommandType] = i_Command;
        }

        public void ExecuteCommand(eCommandType i_CommandTypeName)
        {
            // getCommand(i_CommandTypeName).DoCommand();
        }

        public ICommand GetCommand(eCommandType i_CommandType)
        {
            return r_CommandDictionary[i_CommandType];
        }

        public static CommandCommander GetInstance()
        {
            return InstanceHolder.Instance;
        }
    }
}