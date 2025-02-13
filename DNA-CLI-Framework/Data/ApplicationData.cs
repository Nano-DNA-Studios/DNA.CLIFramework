
namespace DNA_CLI_Framework.Data
{
    /// <summary>
    /// Singleton that creates the ApplicationData Object from the Data Manager class
    /// </summary>
    public class ApplicationData<T> where T : DataManager, new()
    {
        /// <summary>
        /// The Stored Instance of <see cref="T"/>
        /// </summary>
        private static T _instance;

        /// <summary>
        /// Creates a new Instance of the Data Manager if it hasn't been created yet and Returns the Instance
        /// </summary>
        /// <returns> An Instance of <see cref="T"/></returns>
        public static T Instance()
        {
            if (_instance == null)
                _instance = new T();

            return _instance;
        }
    }
}
