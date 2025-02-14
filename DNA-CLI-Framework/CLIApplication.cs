using DNA.CLIFramework.Data;
using DNA.CLIFramework.CommandHandlers;
using System;

namespace DNA.CLIFramework
{
    /// <summary>
    /// Represents the CLI Application, handles all Interaction with the CLI Tool
    /// </summary>s
    public abstract class CLIApplication<T> where T : DataManager, new()
    {
        /// <summary>
        /// The Name of the CLI Application
        /// </summary>
        public abstract string ApplicationName { get; }

        /// <summary>
        /// The Data Manager for the CLI Application
        /// </summary> 
        public static DataManager DataManager { get; set; }

        /// <summary>
        /// The Type of the Command Handler for the CLI Application
        /// </summary>
        public Type CommandHandlerType { get; private set; }

        /// <summary>
        /// Initializes a new Instance of the CLI Application
        /// </summary>
        /// <param name="dataManager"> The Data Manager the CLI Application will use </param>
        public CLIApplication()
        {
            DataManager = ApplicationData<T>.Instance();

            DataManager.ApplicationName = ApplicationName;
        }

        /// <summary>
        /// Initializes a new Instance of the CLI Application
        /// </summary>
        /// <param name="dataManager"> The Data Manager the CLI Application will use </param>
        public CLIApplication(T dataManager)
        {
            if (dataManager == null)
                DataManager = new DefaultDataManager();
            else
                DataManager = dataManager;

            DataManager.ApplicationName = ApplicationName;
        }

        /// <summary>
        /// Runs the CLI Application with the inputted Arguments
        /// </summary>
        /// <param name="args"></param>
        public void RunApplication(string[] args)
        {
            object[] handlerArgs = { args, DataManager.COMMAND_PREFIX };
            CommandHandler? commandHandler = Activator.CreateInstance(CommandHandlerType, handlerArgs) as CommandHandler;

            if (commandHandler != null)
                commandHandler.HandleCommands();
        }

        /// <summary>
        /// Returns a Casted Type of the DataManager
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetDataManager() => (T)DataManager;

        /// <summary>
        /// Sets the Command Handler for the CLI Application
        /// </summary>
        /// <param name="commandHandlerType"></param>
        public void SetCommandHandler<T>() where T : CommandHandler
        {
            CommandHandlerType = typeof(T);
        }
    }
}

