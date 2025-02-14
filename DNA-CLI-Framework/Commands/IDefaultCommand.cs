namespace DNA.CLIFramework.Commands
{
    /// <summary>
    /// Represents the Structure for handling the default command case after the CLI Tool keyword. There a 2 execution paths for the Default Command, one with Trailing Commands and one without
    /// </summary>
    internal interface IDefaultCommand
    {
        /// <summary>
        /// Function that handles executing the Default Command with Arguments and Trailing Commands behind it
        /// </summary>
        /// <param name="args"> The Trailing Arguments to </param>
        public void Execute(string[] args);

        /// <summary>
        /// Function that handles executing the Default Command with Arguments with no Trailing Commands
        /// </summary>
        /// <param name="args"></param>
        public void ExecuteSolo(string[] args);
    }
}
