namespace Core.Messages.Error
{
    /// <summary>
    ///  Consists of messages that can be thrown in DataAccess project.
    /// </summary>
    public static class DataAccess
    {
        /// <summary>
        /// The connection to Data Base has been failed
        /// </summary>
        public static readonly string CAN_NOT_CONNECT_TO_DB_MESSAGE = null;
        /// <summary>
        /// An DataBase file has been missed
        /// </summary>
        public static readonly string NO_FILE_MESSAGE_FORMAT = null;


        // I am not sure that this messages should be here, but let them be, for now...
        /// <summary>
        /// Current user has not been registered before
        /// </summary>
        public static readonly string NO_SUCH_PROFILE_MESSAGE_FORMAT = null;
    }
}
