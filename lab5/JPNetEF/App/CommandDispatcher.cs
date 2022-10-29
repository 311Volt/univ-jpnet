using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JPNetEF.App
{
    internal class CommandDispatcher
    {
        class Command
        {
            public CommandCallback callback;
            public int requiredArgs;

            public Command(CommandCallback callback, int requiredArgs)
            {
                this.callback = callback;
                this.requiredArgs = requiredArgs;
            }
        }

        public delegate void CommandCallback(string[] args);
        private Dictionary<String, Command> cmdRegistry = new();

        public void Register(string cmdName, CommandCallback callback, int requiredArgs=0)
        {
            cmdRegistry.Add(cmdName, new Command(callback, requiredArgs));
        }

        public void Dispatch(string cmd)
        {
            string trimCmd = cmd.Trim();
            string op = trimCmd.Split(' ').First();
            string args = trimCmd.Substring(op.Length);
            string[] arrArgs = args.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            
            if(cmdRegistry.ContainsKey(op))
            {
                int nReq = cmdRegistry[op].requiredArgs;
                int nAct = arrArgs.Length;
                if (nReq != nAct)
                {
                    ConsoleUtil.ErrorMessage(String.Format("expected {0} arguments, {1} provided", nReq, nAct));
                    return;
                }

                try
                {
                    cmdRegistry[op].callback(arrArgs);
                }
                catch(Exception ex)
                {
                    ConsoleUtil.ErrorMessage(ex.GetType().Name + ": " + ex.Message);
                }
                return;
            }
            ConsoleUtil.ErrorMessage("no such command: " + op);
        }
    }
}
