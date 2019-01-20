namespace Core.Messages.Info.ViewModel.Command.User.Setting
{
    /// <summary>
    /// Consists of all messages thet happen in <see cref="ApplyChanges"/> command
    /// </summary>
    public static class ApplyChanges
    {
        /// <summary>
        /// Password is empty
        /// </summary>
        public static string EMPTY_PASSWORD = string.Concat("Changes will not be setted", System.Environment.NewLine, "Password field is empty");
        /// <summary>
        /// User's password and written one is different
        /// </summary>
        public static string PASSWORD_IS_NOT_THE_SAME = string.Concat("Changes will not be setted", System.Environment.NewLine, "Password field is wrong");
        /// <summary>
        /// All changes has been successfully applied
        /// </summary>
        public static string CHANGES_APPLIED = "All changes has been successfully applied";
    }
}
