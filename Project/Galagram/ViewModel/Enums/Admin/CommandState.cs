namespace Galagram.ViewModel.Enums.Admin
{
    /// <summary>
    /// Specifies state of the command executing
    /// </summary>
    public enum CommandState
    {
        /// <summary>
        /// State isn't changed
        /// </summary>
        Default = 0,
        /// <summary>
        /// Command is executed successfully
        /// </summary>
        Executed = 1,
        /// <summary>
        /// Command is interrupted
        /// </summary>
        Interrupted = 2
    }
}
