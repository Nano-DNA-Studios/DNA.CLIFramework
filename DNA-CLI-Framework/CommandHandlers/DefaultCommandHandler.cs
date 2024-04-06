
namespace DNA_CLI_Framework.CommandHandlers
{
    /// <summary>
    /// The Default Command Handler for the CLI Application
    /// </summary>
    public class DefaultCommandHandler : CommandHandler
    {
        /// <summary>
        /// Initializes a new instance of the CommandHandler class
        /// </summary>
        /// <param name="commandPrefix"> The Prefix that Identifies a single Command </param>
        public DefaultCommandHandler(string[] args, string commandPrefix) : base(args, commandPrefix)
        {
        }

        /// <summary>
        /// Handles all the Commands passed in by the User
        /// </summary>
        public override void HandleCommands()
        {
            if (DefaultCommandArgs.Length == 0)
                return;

            HandleDefaultCommand();
            HandleAdditionalCommands();
        }

        /// <summary>
        /// Handles the Default Command for the User
        /// </summary>
        protected override void HandleDefaultCommand()
        {
            HandleDefault();
        }

        /// <summary>
        /// Handles all Additonal Commands passed in by the User
        /// </summary>
        protected override void HandleAdditionalCommands()
        {
            HandleAdditional();
        }
    }
}
