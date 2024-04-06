

namespace DNA_CLI_Framework
{
    /// <summary>
    /// Represents the CLI Application, handles all Interaction with the CLI Tool
    /// </summary>
    public abstract class CLIApplication
    {
        /// <summary>
        /// The Default Command Prefix for the CLI Application  
        /// </summary>
        public const string DEFAULT_COMMAND_PREFIX = "--";

        /// <summary>
        /// The Name of the CLI Application
        /// </summary>
        public abstract string ApplicationName { get; }

        /// <summary>
        /// The Data Manager for the CLI Application
        /// </summary> 
        public static DataManager DataManager { get; set; }

        /// <summary>
        /// The Command Prefix to use for the CLI Application
        /// </summary>
        public abstract string COMMAND_PREFIX { get; }

        /// <summary>
        /// Initializes a new Instance of the CLI Application
        /// </summary>
        /// <param name="applicationName"> The Name of the Application </param>
        /// <param name="dataManager"> The Data Manager the CLI Application will use </param>
        public CLIApplication(DataManager dataManager = null)
        {

            if (dataManager == null)
                DataManager = new DataManager();
            else
                DataManager = dataManager;
        }

        /// <summary>
        /// Runs the CLI Application with the inputted Arguments
        /// </summary>
        /// <param name="args"></param>
        public void RunApplication(string[] args)
        {
            CommandHandler commandHandler = new CommandHandler(COMMAND_PREFIX);

            commandHandler.HandleCommands(args);
        }

        /// <summary>
        /// Returns a Casted Type of the DataManager
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetDataManager<T>() where T : DataManager => (T)DataManager;
    }
}

