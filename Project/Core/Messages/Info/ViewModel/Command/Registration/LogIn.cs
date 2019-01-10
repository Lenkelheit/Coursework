namespace Core.Messages.Info.ViewModel.Command.Registration
{
    /// <summary>
    /// Consists of all messages thet happen in <see cref="ViewModel.Command.Registration.LogIn"/>
    /// </summary>
    public class LogIn
    {
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
        public static readonly string PASSWORD_IS_WRONG = "Current password is wrond";

    }
}
