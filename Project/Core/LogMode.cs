namespace Core
{
    /// <summary>
    /// Log message level
    /// </summary>
    [System.Flags]
    public enum LogMode
    {
        /// <summary>
        /// Explain logic step by step
        /// </summary>
        Debug = 1,
        /// <summary>
        /// Information about work, its efficiency
        /// </summary>
        Info = 2,
        /// <summary>
        /// Something weird happen
        /// <para/>
        /// Caught exception known type 
        /// </summary>
        Warn = 4,
        /// <summary>
        /// Error has been occurred
        /// <para/>
        /// Caught exception unknown type 
        /// </summary>
        Error = 8,
        /// <summary>
        /// Immediately help required
        /// <para/>
        /// The application has shout down with exception
        /// </summary>
        Fatal = 16
    }
}
