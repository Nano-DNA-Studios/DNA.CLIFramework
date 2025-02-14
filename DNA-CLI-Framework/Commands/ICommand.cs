
namespace DNA.CLIFramework.Commands
{
    /// <summary>
    /// Represents a Command that can be executed by the CLI Tool
    /// </summary>
    internal interface ICommand
    {
        /// <summary>
        /// The Name of the Command to Handle
        /// </summary>
        public abstract string CommandName { get; }

        /// <summary>
        /// The Description of the Command to Handle
        /// </summary>
        public abstract string CommandDescription { get; }

        /// <summary>
        /// Function that handles executing the Command with Arguments
        /// </summary>
        /// <param name="args"> The Arguments for the Command </param>
        public abstract void Execute(string[] args);
    }
}
