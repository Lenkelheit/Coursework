namespace DataAccess.Wrappers
{
    /// <summary>
    /// Wraps <see cref="Entities.User"/>, to use it in algorithms
    /// </summary>
    public class UserWrapper
    {
        // FIELDS
        Entities.User user;
        string mainPhotoPath;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="UserWrapper"/>
        /// </summary>
        /// <param name="user">
        /// An instance of <see cref="Entities.User"/>
        /// </param>
        public UserWrapper(Entities.User user)
        {
            this.user = user;
            this.mainPhotoPath = user.MainPhotoName != null ? string.Format(Core.Configuration.AppConfig.AVATAR_FORMAT, user.MainPhotoName) : null;
        }

        // PROPERTIES
        /// <summary>
        /// Gets or sets wrapped user
        /// </summary>
        public Entities.User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                this.mainPhotoPath = user.MainPhotoName != null ? string.Format(Core.Configuration.AppConfig.AVATAR_FORMAT, user.MainPhotoName) : null;
            }
        }
        /// <summary>
        /// Gets or sets the main photo path of <see cref="User"/>
        /// </summary>
        public string MainPhotoPath
        {
            get
            {
                return mainPhotoPath;
            }
            set
            {
                user.MainPhotoName = System.IO.Path.GetFileName(value);
                mainPhotoPath = value;
            }
        }
    }
}
