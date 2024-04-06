using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNA_CLI_Framework
{
    public class CommandHandler
    {
        private const string COMMAND_PREFIX = "--";
        

        public static void HandleCommand(string[] args)
        {
            if (args.Length < 1)
            {
                //Handle no argument command
                return;
            }

            string combinedArgs = string.Join(" ", args);

            string[] individualCommands = combinedArgs.Split(COMMAND_PREFIX);

            //The Default Command to handle is the first command in the list
            string defaultCommand = individualCommands[0].Trim();

            //The rest of the trailing commands are the arguments
            string[] commands = individualCommands.Skip(1).ToArray();

            bool isOnlyDefaultCommand = commands.Length == 0;


            Console.WriteLine("Default Command: " + defaultCommand);

            HandleDefaultCommand(defaultCommand, isOnlyDefaultCommand);

        }

        private static void HandleDefaultCommand(string command, bool isOnlyDefaultCommand)
        {
            string[] args = command.Split(" ");

            //Use a Factory to instantiate the correct command of type Default Command






        }

        private static void HandleAdditionalCommands(string[] commands)
        {



        }







    }
}
