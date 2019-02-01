namespace Galagram.Services
{
    /// <summary>
    /// Contains all data that has been used among windows
    /// <para />
    /// Implements a Singleton pattern
    /// </summary>
    public class DataStorage
    {
        // FIELDS
        static DataStorage instance;

        // CONSTRUCTORS
        private DataStorage()
        {
            LoggedUser = null;
            ShownUser = null;
        }
        static DataStorage()
        {
            instance = new DataStorage();
        }

        // PROPERTIES
        /// <summary>
        /// Gets an instance of <see cref="DataStorage"/>
        /// </summary>
        public static DataStorage Instance => instance;
        /// <summary>
        /// Gets or sets Logged user
        /// </summary>
        public DataAccess.Entities.User LoggedUser { get; set; }
        /// <summary>
        /// Gets or sets Shown user
        /// </summary>
        public DataAccess.Entities.User ShownUser { get; set; }
        /// <summary>
        /// Gets or sets admin window content control
        /// </summary>
        public System.Windows.Controls.ContentControl AdminWindowContentControl { get; set; }

        /// <summary>
        /// Gets true if current user and shown user is the same, otherwise — false
        /// </summary>
        public bool IsCurrentUserShown => LoggedUser == ShownUser;

        // METHODS
        /// <summary>
        /// Sets logged user as shown one
        /// </summary>
        public void ShowLoggedUser()
        {
            ShownUser = LoggedUser;
        }
        /// <summary>
        /// Sets all properties to their default values
        /// </summary>
        public void Reset()
        {
            LoggedUser = null;
            ShownUser = null;
            AdminWindowContentControl.Content = null;
        }
    }
}
