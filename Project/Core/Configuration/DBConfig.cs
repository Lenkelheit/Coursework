namespace Core.Configuration
{
    /// <summary>
    /// Consists of all important configuration to DataBase.
    /// </summary>
    public class DBConfig
    {
        // COMMENT

        /// <summary>
        /// Comment text min length
        /// </summary>
        public const int COMMENT_TEXT_MIN_LENGTH = 4;
        /// <summary>
        /// Comment text max length
        /// </summary>
        public const int COMMENT_TEXT_MAX_LENGTH = 255;

        // ADMIN MESSAGE

        /// <summary>
        /// Admin message text min length
        /// </summary>
        public const int ADMIN_MESSAGE_MIN_LENGTH = 10;
        /// <summary>
        /// Admin message text max length
        /// </summary>
        public const int ADMIN_MESSAGE_MAX_LENGTH = 1000;

        /// <summary>
        /// Admin message subject text min length
        /// </summary>
        public const int ADMIN_MESSAGE_SUBJECT_MIN_LENGTH = 3;
        /// <summary>
        /// Admin message subject text max length
        /// </summary>
        public const int ADMIN_MESSAGE_SUBJECT_MAX_LENGTH = 25;

        // PHOTOS

        /// <summary>
        /// Photo server path string min length
        /// </summary>
        public const int PHOTO_PATH_MIN_LENGTH = 4;
        /// <summary>
        /// Photo server path string max length
        /// </summary>
        public const int PHOTO_PATH_MAX_LENGTH = 256;
        /// <summary>
        /// Allowed photo extension
        /// <para/>
        /// Should be comma separated and has no spaces
        /// </summary>
        public const string PHOTO_EXTENSION = "jpg,gif,png";


        // USER
        /// <summary>
        /// Avatar server path string min length
        /// </summary>
        public const int AVATAR_MIN_LENGTH = 4;
        /// <summary>
        /// Avatar server path strng max length
        /// </summary>
        public const int AVATAR_MAX_LENGTH = 264;

        /// <summary>
        /// User nickname text min length
        /// </summary>
        public const int NICKNAME_MIN_LENGTH = 3;
        /// <summary>
        /// User nickname text max length
        /// </summary>
        public const int NICKNAME_MAX_LENGTH = 15;

        /// <summary>
        /// Password text min length
        /// </summary>
        public const int PASSWORD_MIN_LENGTH = 3;
        /// <summary>
        /// Password text max length
        /// </summary>
        public const int PASSWORD_MAX_LENGTH = 14;
    }
}
