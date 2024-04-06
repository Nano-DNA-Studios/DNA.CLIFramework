namespace DNA_CLI_Framework.Data
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

        /// <inheritdoc/>
        public abstract string COMMAND_PREFIX { get; }
    }
}
