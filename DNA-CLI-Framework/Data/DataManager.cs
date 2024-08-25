﻿namespace DNA_CLI_Framework.Data
{
    /// <summary>
    /// The Base Structure for a Data Manager for the CLI Application
    /// </summary>
    public abstract class DataManager : IDataManager
    {
        /// <summary>
        /// The Default Command Prefix for the CLI Application  
        /// </summary>
        public const string DEFAULT_COMMAND_PREFIX = "--";

        /// <summary>
        /// Dicitionary of all the Resource Names and their Values
        /// </summary>
        public Dictionary<string, object> ResourcesDictionary { get; set; }

        /// <summary>
        /// The Absolute Path at which the Application is Located, The EXE, DLLs and Application Resources are located here
        /// </summary>
        public string ApplicationPath { get; private set; }

        /// <summary>
        /// The Current Working Directory at which the Application is Located
        /// </summary>
        public string CWD { get; private set; }

        /// <summary>
        /// The Path of the Applications Cache
        /// </summary>
        public string CachePath { get; private set; }

        /// <inheritdoc/>
        public abstract string COMMAND_PREFIX { get; }

        

        /// <summary>
        /// Default Initializer
        /// </summary>
        public DataManager()
        {
            ApplicationPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            CWD = Directory.GetCurrentDirectory();
        }

        public void SetCacheName (string name)
        {
            CachePath = Path.Combine(CWD, $"{name}Cache");
        }

        public void CreateCacheDirectory (string prefix)
        {
            if (string.IsNullOrEmpty(CachePath))
            {
                Console.WriteLine("Cache Directory has not been set.");
                return;
            }

            if (!Directory.Exists(CachePath))
                Directory.CreateDirectory(CachePath);
        }
    }
}
