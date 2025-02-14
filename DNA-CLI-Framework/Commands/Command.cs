
namespace DNA.CLIFramework.Commands
{
    /// <summary>
    /// Represents a Command that can be executed by the CLI Tool
    /// </summary>
    public abstract class Command : ICommand
    {
        /// <inheritdoc/>
        public abstract string CommandName { get; }

        /// <inheritdoc/>
        public abstract string CommandDescription { get; }

        /// <inheritdoc/>
        public abstract void Execute(string[] args);
    }
}
