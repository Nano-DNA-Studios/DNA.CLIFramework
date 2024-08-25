using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DNA_CLI_Framework
{
    /// <summary>
    /// Process Handler for the Console Application.
    /// </summary>
    public class ConsoleProcessHandler
    {
        /// <summary>
        /// The Application that will handle the Process.
        /// </summary>
        public enum ProcessApplication
        {
            CMD,
            PowerShell,
            Other
        }

        /// <summary>
        /// Process Start Info
        /// </summary>
        private ProcessStartInfo _processStartInfo { get; set; }

        /// <summary>
        /// The Application that will handle the Process.
        /// </summary>
        public ProcessApplication Application { get; set; }

        /// <summary>
        /// Toggle for the Standard Output Redirect.
        /// </summary>
        private bool _outputRedirect = true;

        /// <summary>
        /// The Standard Output of the Process.
        /// </summary>
        private List<string> _standardOutput = new List<string>();

        /// <summary>
        /// The Standard Output of the Process.
        /// </summary>
        public string[] StandardOutput { get => _standardOutput.ToArray(); }

        /// <summary>
        /// The Standard Output of the Process.
        /// </summary>
        private List<string> _standardError = new List<string>();

        /// <summary>
        /// The Standard Output of the Process.
        /// </summary>
        public string[] StandardError { get => _standardError.ToArray(); }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ConsoleProcessHandler(ProcessApplication application)
        {
            _processStartInfo = new ProcessStartInfo();

            Application = application;
            _processStartInfo.FileName = GetApplicationPath(application);
            _processStartInfo.RedirectStandardOutput = true;
            _processStartInfo.RedirectStandardError = true;
            _processStartInfo.CreateNoWindow = true;
            _processStartInfo.UseShellExecute = false;

            _standardOutput = new List<string>();
        }

        /// <summary>
        /// Toggles the Standard Output Redirect Option
        /// </summary>
        /// <param name="redirectState"> The state of the Toggle, True = Display the Standard Output in Console, False = Don't display Output </param>
        public void SetOutputRedirect(bool redirectState)
        {
            _outputRedirect = redirectState;
        }

        /// <summary>
        /// Returns the File Name / Executable of the Application based on the ProcessApplication Enum.
        /// </summary>
        /// <param name="application"> The Application Selected </param>
        /// <returns> The Executable / File Name of the Application </returns>
        private string GetApplicationPath(ProcessApplication application)
        {
            switch (application)
            {
                case ProcessApplication.CMD:
                    return "cmd.exe";
                case ProcessApplication.PowerShell:
                    return "powershell.exe";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Returns the Arguments for the Application based on the ProcessApplication Enum.
        /// </summary>
        /// <param name="application"> Application that will run the Process </param>
        /// <param name="command"> The Commands to run </param>
        /// <returns> The Arguments that will be passed to the Application </returns>
        private string GetApplicationArguments(ProcessApplication application, string command)
        {
            switch (application)
            {
                case ProcessApplication.CMD:
                    return $"/c {command}";
                case ProcessApplication.PowerShell:
                    return $"-Command {command}";
                default:
                    return command;
            }
        }

        /// <summary>
        /// Runs a Process with the given command.
        /// </summary>
        /// <param name="command"> The Command Passed to the CMD </param>
        public void RunProcess(string command)
        {
            _processStartInfo.Arguments = GetApplicationArguments(Application, command);

            using (Process process = new Process())
            {
                process.StartInfo = _processStartInfo;
                process.Start();

                while (!process.StandardOutput.EndOfStream)
                {
                    string? line = process.StandardOutput.ReadLine();

                    if (line != null)
                    {
                        _standardOutput.Add(line);

                        if (_outputRedirect)
                            Console.WriteLine(line);
                    }
                }

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    Console.WriteLine("Error:");
                    Console.WriteLine(process.StandardError.ReadToEnd());
                }
            }
        }
    }
}
