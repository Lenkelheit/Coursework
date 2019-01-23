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
        public readonly static string EMPTY_PASSWORD = string.Concat("Changes will not be setted", System.Environment.NewLine, "Password field is empty");
        /// <summary>
        /// User's password and written one is different
        /// </summary>
        public readonly static string PASSWORD_IS_NOT_THE_SAME = string.Concat("Changes will not be setted", System.Environment.NewLine, "Password field is wrong");
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
    }
}
