namespace Core.Messages.Info.ViewModel.Command.User.AskQuestion
{
    /// <summary>
    /// Consists of all messages thet happen in <see cref="Ask"/> command
    /// </summary>
    public static class Ask
    {
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
    }
}
