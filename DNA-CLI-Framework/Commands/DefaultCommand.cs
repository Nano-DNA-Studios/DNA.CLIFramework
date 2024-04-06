
namespace DNA_CLI_Framework.Commands
{
    /// <summary>
    /// Represents a Default Command that can be executed by the CLI Tool, can execute solo with no trailing commands or with trailing commands
    /// </summary>
    public abstract class DefaultCommand : IDefaultCommand
    {
        /// <inheritdoc />
        public abstract void Execute(string[] args);

        /// <inheritdoc />
        public abstract void ExecuteSolo(string[] args);
    }
}
