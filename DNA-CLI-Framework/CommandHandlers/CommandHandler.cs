using DNA_CLI_Framework.Commands;

namespace DNA_CLI_Framework.CommandHandlers
{
    /// <summary>
    /// The CLI Framework Command Handler, responsible for handling the Commands passed in by the User
    /// </summary>
    public abstract class CommandHandler
    {
        /// <summary>
        /// The Arguments to Execute through the Default Command
        /// </summary>
        protected string[] DefaultCommandArgs { get; private set; }

        /// <summary>
        /// The Arguments to Execute through the Additional Commands
        /// </summary>
        protected string[][] AdditionalCommandArgs { get; private set; }

        /// <summary>
        /// Flag Determining if only the Default Command will be executed
        /// </summary>
        protected bool IsOnlyDefaultCommand { get; private set; }

        /// <summary>
        /// The Prefix that seperates individual additional commands
        /// </summary>
        public string CommandPrefix { get; private set; }

        /// <summary>
        /// Initializes the Command Handler with the Arguments passed in by the User
        /// </summary>
        /// <param name="args"> The Arguments passed by the User </param>
        public CommandHandler(string[] args, string commandPrefix)
        {
            SetCommandPrefix(commandPrefix);
            SetCommandArguments(args);
        }

        /// <summary>
        /// Sets the Command Prefix for the Command Handler
        /// </summary>
        /// <param name="commandPrefix"> The Prefix to Identify a Single Command </param>
        internal void SetCommandPrefix(string commandPrefix)
        {
            CommandPrefix = commandPrefix;
        }

        /// <summary>
        /// Handles all the Commands passed in by the User
        /// </summary>
        /// <param name="args"> The Arguments passed by the User </param>
        public abstract void HandleCommands();

        /// <summary>
        /// Handles the Default Command for the User
        /// </summary>
        protected abstract void HandleDefaultCommand();

        /// <summary>
        /// Handles all Additonal Commands passed in by the User
        /// </summary>
        protected abstract void HandleAdditionalCommands();

        /// <summary>
        /// The Default Method to Handle the Default Command
        /// </summary>
        protected void HandleDefault()
        {
            DefaultCommand? defaultCommand = CommandFactory.GetDefaultCommand();

            if (defaultCommand == null)
                return;

            if (IsOnlyDefaultCommand)
                defaultCommand.ExecuteSolo(DefaultCommandArgs);
            else
                defaultCommand.Execute(DefaultCommandArgs);
        }

        /// <summary>
        /// The Default Method to Handle the Additional Commands
        /// </summary>
        protected void HandleAdditional()
        {
            foreach (string[] commandArgs in AdditionalCommandArgs)
            {
                Command? commandInstance = CommandFactory.GetCommand(commandArgs[0]);

                if (commandInstance != null)
                    commandInstance.Execute(commandArgs.Skip(1).ToArray());
            }
        }

        /// <summary>
        /// Sets the Command Arguments in a Format that can be used by Derived Command Handler
        /// </summary>
        /// <param name="args"> The Arguments to Format </param>
        protected void SetCommandArguments(string[] args)
        {
            string combinedArgs = string.Join(" ", args);
            string[] individualCommands = combinedArgs.Split(CommandPrefix);

            string defaultCommand = individualCommands[0].Trim();
            string[] defaultCommandArgs = defaultCommand.Split(" ");

            string[] commands = individualCommands.Skip(1).ToArray();
            string[][] additionalCommandArgs = new string[commands.Length][];
            foreach (string command in commands)
            {
                List<string> commandArgs = new List<string>();

                commandArgs.Add(command.Split(" ")[0]);

                string subCommands = command.Split(" ").Skip(1).Aggregate((acc, next) => acc + " " + next);

                commandArgs.AddRange(subCommands.Split("\"").Where(arg => !string.IsNullOrWhiteSpace(arg)).ToArray());

                additionalCommandArgs[commands.ToList().IndexOf(command)] = commandArgs.ToArray();
            }
                
            IsOnlyDefaultCommand = commands.Length == 0;
            DefaultCommandArgs = defaultCommandArgs;
            AdditionalCommandArgs = additionalCommandArgs;
        }

        /// <summary>
        /// Returns the Command Arguments in a Format that can be used by Derived Command Handler
        /// </summary>
        /// <returns> The <see cref="DefaultCommandArgs"/> and <see cref="AdditionalCommandArgs"/> formatted in a Tuple </returns>
        protected (string[] defaultCommandArgs, string[][] additionalCommandArgs) GetCommandArguments()
        {
            return (DefaultCommandArgs, AdditionalCommandArgs);
        }
    }
}
