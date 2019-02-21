namespace Core.Messages.Info
{
    /// <summary>
    /// Consists of all messages happen in Admin Panel
    /// </summary>
    public static class Admin
    {
        /// <summary>
        /// Wrong subject length
        /// </summary>
        public static readonly string ADMIN_WRONG_SUBJECT_LENGTH = string.Concat("Current subject length is not allowed", 
                                                                                System.Environment.NewLine, 
                                                                                "Min length = ", Configuration.DBConfig.ADMIN_MESSAGE_SUBJECT_MIN_LENGTH, 
                                                                                ", max length = ", Configuration.DBConfig.ADMIN_MESSAGE_SUBJECT_MAX_LENGTH);
        /// <summary>
        /// Wrong user's nickname length
        /// </summary>
        public static readonly string ADMIN_WRONG_USER_NICKNAME_LENGTH = string.Concat("Current user's nickname length is not allowed",
                                                                                System.Environment.NewLine,
                                                                                "Min length = ", Configuration.DBConfig.NICKNAME_MIN_LENGTH,
                                                                                ", max length = ", Configuration.DBConfig.NICKNAME_MAX_LENGTH);
        /// <summary>
        /// Wrong user's password length
        /// </summary>
        public static readonly string ADMIN_WRONG_USER_PASSWORD_LENGTH = string.Concat("Current user's password length is not allowed",
                                                                                System.Environment.NewLine,
                                                                                "Min length = ", Configuration.DBConfig.PASSWORD_MIN_LENGTH,
                                                                                ", max length = ", Configuration.DBConfig.PASSWORD_MAX_LENGTH);
        /// <summary>
        /// Wrong user's main photo path length
        /// </summary>
        public static readonly string ADMIN_WRONG_USER_MAIN_PHOTO_PATH_LENGTH = string.Concat("Current user's main photo path length is not allowed",
                                                                                System.Environment.NewLine,
                                                                                "Min length = ", Configuration.DBConfig.AVATAR_MIN_LENGTH,
                                                                                ", max length = ", Configuration.DBConfig.AVATAR_MAX_LENGTH);
    }
}
