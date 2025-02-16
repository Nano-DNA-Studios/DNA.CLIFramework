using DNA.CLIFramework.Commands;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DNA.CLIFramework
{
    /// <summary>
    /// Command Factory that is responsible for creating instances of Commands based on the Command Name
    /// </summary>
    public static class CommandFactory
    {
        /// <summary>
        /// Mapping of Command Names to their respective Types
        /// </summary>
        private static Dictionary<string, Type> Commands { get; set; }

        /// <summary>
        /// The Default Command Type as the Base Command to Execute
        /// </summary>
        private static Type DefaultCommand { get; set; }

        /// <summary>
        /// Initializes the Command Factory by Mapping all the CommandNames to their respective Types
        /// </summary>
        static CommandFactory()
        {
            Commands = new Dictionary<string, Type>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    RegisterCommands(type);
                }
            }

            if (DefaultCommand == null)
                throw new Exception("No Default Command Found, there must be a Default Command to Execute");
        }

        /// <summary>
        /// Registers the Commands and Adds them to the Commands Dictionary, or sets the Default Command
        /// </summary>
        /// <param name="type"></param>
        /// <exception cref="Exception"></exception>
        private static void RegisterCommands(Type type)
        {
            if (type.IsSubclassOf(typeof(Command)) && !type.IsAbstract)
            {
                try
                {
                    Command? command = (Command?)Activator.CreateInstance(type);

                    if (command != null)
                        Commands.Add(command.CommandName.ToLower(), type);
                }
                finally
                {
                }
            }
            else if (type.IsSubclassOf(typeof(DefaultCommand)))
            {
                if (DefaultCommand == null)
                    DefaultCommand = type;
                else
                    throw new Exception("Multiple Default Commands Found, there can only be one Default Command per Program");
            }
        }

        /// <summary>
        /// Gets an instance of the Command based on the Command Name
        /// </summary>
        /// <param name="commandName"> The Name of the Command to Execute </param>
        /// <returns> A New Instance of the Command to Handle </returns>
        public static Command? GetCommand(string commandName)
        {
            string commandNameLower = commandName.ToLower();

            if (Commands.TryGetValue(commandNameLower, out Type? commandType))
            {
                if (commandType != null)
                    return Activator.CreateInstance(commandType) as Command;
            }

            Console.WriteLine("Command Not Found: " + commandName);

            return null;
        }

        /// <summary>
        /// Gets an Instance of the Default Command to Execute
        /// </summary>
        /// <returns> A New Instance of the Default Command Type to Execute </returns>
        public static DefaultCommand? GetDefaultCommand()
        {
            if (DefaultCommand != null)
                return Activator.CreateInstance(DefaultCommand) as DefaultCommand;

            return null;
        }
    }
}
