namespace DNA_CLI_Framework.CommandHandlers
{
    /// <summary>
    /// The CLI Framework Command Handler, responsible for handling the Commands passed in by the User
    /// </summary>
    public abstract class CommandHandler
    {
        /// <summary>
        /// The Prefix that seperates individual additional commands
        /// </summary>
        public string CommandPrefix { get; private set; }

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
        public abstract void HandleCommands(string[] args);

        /// <summary>
        /// Handles the Default Command for the User
        /// </summary>
        /// <param name="commandArguments"> The Command Arguments for the Default Command </param>
        /// <param name="isOnlyDefaultCommand"> Flag Identifying if only the Default Command will be activated or if there are trailing Commands </param>
        protected abstract void HandleDefaultCommand(string commandArguments, bool isOnlyDefaultCommand);

        /// <summary>
        /// Handles all Additonal Commands passed in by the User
        /// </summary>
        /// <param name="commands"> The Arguments for all the Additional Commands </param>
        protected abstract void HandleAdditionalCommands(string[] commands);
    }
}
