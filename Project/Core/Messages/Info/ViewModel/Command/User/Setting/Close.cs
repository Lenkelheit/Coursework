namespace Core.Messages.Info.ViewModel.Command.User.Setting
{
    /// <summary>
    /// Consists of all messages thet happen in <see cref="Close"/> command
    /// </summary>
    public static class Close
    {
        /// <summary>
        /// user try to exit but he has unsaved changes
        /// </summary>
        public static readonly string UNSAVED_CHANGES_MESSAGE = string.Concat("The changes has not been saved.", System.Environment.NewLine, "Are you sure you want to exit?");
    }
}
