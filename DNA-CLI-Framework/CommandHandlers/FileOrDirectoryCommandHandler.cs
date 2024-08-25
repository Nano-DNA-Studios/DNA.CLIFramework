
namespace DNA_CLI_Framework.CommandHandlers
{
    /// <summary>
    /// Handles Commands that are passed in by the User, checks if the Command is a File or Directory and then executes the Command. If the Command is a Directory, it will execute the Command for each File in the Directory, otherwise it will only run for the File
    /// </summary>
    public class FileOrDirectoryCommandHandler : CommandHandler
    {
        /// <summary>
        /// Default Command to Execute
        /// </summary>
        /// <param name="args"> Arguments Passed by the User </param>
        /// <param name="commandPrefix"> The Prefix for Commands </param>
        public FileOrDirectoryCommandHandler(string[] args, string commandPrefix) : base(args, commandPrefix)
        {
        }

        /// <inheritdoc/>
        public override void HandleCommands()
        {
            if (DefaultCommandArgs.Length == 0)
                return;

            if (DefaultCommandArgs[0] == string.Empty)
            {
                HandleAdditionalCommands();
                return;
            }

            string fullPath = Path.GetFullPath(DefaultCommandArgs[0]);

            if (Directory.Exists(fullPath))
            {
                string[] files = Directory.GetFiles(fullPath);

                foreach (string file in files)
                {
                    DefaultCommandArgs[0] = file;

                    HandleDefaultCommand();
                    HandleAdditionalCommands();
                }
            }
            else if (File.Exists(fullPath))
            {
                HandleDefaultCommand();
                HandleAdditionalCommands();
            }
        }

        /// <inheritdoc/>
        protected override void HandleAdditionalCommands()
        {
            HandleAdditional();
        }

        /// <inheritdoc/>
        protected override void HandleDefaultCommand()
        {
            HandleDefault();
        }
    }
}
