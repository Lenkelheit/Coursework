namespace Core.Messages.Info
{
    /// <summary>
    /// Consists of all messages thet happened in ViewModels
    /// </summary>
    public static class ViewModel
    {
        #region SETTINGS
        #region APPLY CHANGES
        /// <summary>
        /// Password is empty
        /// </summary>
        public readonly static string EMPTY_PASSWORD = "Password field is empty";
        /// <summary>
        /// User's password and written one is different
        /// </summary>
        public readonly static string PASSWORD_IS_NOT_THE_SAME = "Password field is wrong";
        /// <summary>
        /// User can not change nickname, it is occupied
        /// </summary>
        public readonly static string NICKNAME_IS_NOT_FREE = string.Concat("Nickname can not be changed", System.Environment.NewLine, "Current nickname is occupied");
        /// <summary>
        /// Any of the fields were not changed
        /// </summary>
        public readonly static string NO_CHANGES = "There were no changes";
        /// <summary>
        /// All changes has been successfully applied
        /// </summary>
        public readonly static string CHANGES_APPLIED = "All changes has been successfully applied";
        #endregion
        #region CLOSE
        /// <summary>
        /// User try to exit but he has unsaved changes
        /// </summary>
        public static readonly string UNSAVED_CHANGES_MESSAGE = string.Concat("The changes has not been saved.", System.Environment.NewLine, "Are you sure you want to exit?");
        #endregion
        #region REMOVE ACCOUNT
        /// <summary>
        /// Verify if user do want to delete his account
        /// </summary>
        public readonly static string DO_DELETE_ACCOUNT = "Are you sure you want to delete your account?";
        #endregion

        #endregion

        #region ASK QUESTIONS
        /// <summary>
        /// Subject to message is not selected
        /// </summary>
        public static readonly string SUBJECT_IS_NOT_SELECTED = "Please, select subject to your message";
        /// <summary>
        /// Message text is empty
        /// </summary>
        public static readonly string EMPTY_MESSAGE = "Please, write your message";
        /// <summary>
        /// Message text is too short
        /// </summary>
        public static readonly string MESSAGE_TOO_SHORT = $"Message can not be shorter than {Core.Configuration.DBConfig.ADMIN_MESSAGE_MIN_LENGTH}";
        /// <summary>
        /// Message text is too long
        /// </summary>
        public static readonly string MESSAGE_TOO_LONG = $"Message can not be longer than {Core.Configuration.DBConfig.ADMIN_MESSAGE_MAX_LENGTH}";
        /// <summary>
        /// Message has been sent
        /// </summary>
        public static readonly string MESSAGE_SENT = "Your message has been successfully sent";
        #endregion

        #region REGISTRATION
        /// <summary>
        /// User is admin. Ask him do he want to log in regularly or as admin
        /// </summary>
        public static readonly string IS_NEED_LOG_IN_AS_ADMIN = "Do you want to log in as admin?";

        /// <summary>
        /// Nickname can not be empty
        /// </summary>
        public static readonly string NICKNAME_EMPTY = "Nickname can not be empty";
        /// <summary>
        /// Password can not be empty
        /// </summary>
        public static readonly string PASSWORD_EMPTY = "Password can not be empty";
        /// <summary>
        /// Short nickname message
        /// </summary>
        public static readonly string NICKNAME_TOO_SHORT = string.Format("Nickname can not be shorter than {0} symbols.", Configuration.DBConfig.NICKNAME_MIN_LENGTH);
        /// <summary>
        /// Long nickname message
        /// </summary>
        public static readonly string NICKNAME_TOO_LONG = string.Format("Nickname can not be longer than {0} symbols.", Configuration.DBConfig.NICKNAME_MAX_LENGTH);
        /// <summary>
        /// Short password message
        /// </summary>
        public static readonly string PASSWORD_TOO_SHORT = string.Format("Password can not be shorter than {0} symbols.", Configuration.DBConfig.PASSWORD_MIN_LENGTH);
        /// <summary>
        /// Long password message
        /// </summary>
        public static readonly string PASSWORD_TOO_LONG = string.Format("Password can not be longer than {0} symbols.", Configuration.DBConfig.PASSWORD_MAX_LENGTH);
        /// <summary>
        /// Nickname is wrong
        /// </summary>
        public static readonly string NICKNAME_IS_WRONG = "Current nickname is wrong";
        /// <summary>
        /// Password is wrong
        /// </summary>
        public static readonly string PASSWORD_IS_WRONG = "Current password is wrong";
        /// <summary>
        /// Nickname is not available message
        /// </summary>
        public static readonly string NICKNAME_IS_NOT_AVAILABLE = "Current nickname is not available";
        #endregion
    }
}
