using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNA_CLI_Framework.CommandHandlers
{
    public class FileOrDirectoryCommandHandler : CommandHandler
    {
        public FileOrDirectoryCommandHandler(string[] args, string commandPrefix) : base(args, commandPrefix)
        {
        }

        /// <inheritdoc/>
        public override void HandleCommands()
        {
            if (DefaultCommandArgs.Length == 0)
                return;

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
