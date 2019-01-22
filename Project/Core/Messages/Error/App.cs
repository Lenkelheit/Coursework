namespace Core.Messages.Error
{
    /// <summary>
    /// Consists of all important messages that can be thrown in project.
    /// </summary>
    public static class App
    {
        /// <summary>
        /// Used when program has been closed with fatal unhandled error
        /// </summary>
        public static readonly string FATAL_ERROR_CONTINUE = string.Concat("An unexpected error has occured", System.Environment.NewLine, "Please accept our apologies");

        /// <summary>
        /// Used when program has been closed with fatal unhandled error
        /// </summary>
        public static readonly string FATAL_ERROR_CLOSE = string.Concat("The program has been shut down with error", System.Environment.NewLine, "Please accept our apologies");
    }
}
