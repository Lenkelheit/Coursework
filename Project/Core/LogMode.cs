namespace Core
{
    /// <summary>
    /// Log message level
    /// </summary>
    public enum LogMode
    {
        /// <summary>
        /// Explain logic step by step
        /// </summary>
        Debug,
        /// <summary>
        /// Information about work, its efficiency
        /// </summary>
        Info,
        /// <summary>
        /// Something weird happen
        /// <para/>
        /// Caught exception known type 
        /// </summary>
        Warn,
        /// <summary>
        /// Error has been occurred
        /// <para/>
        /// Caught exception unknown type 
        /// </summary>
        Error,
        /// <summary>
        /// Immediately help required
        /// <para/>
        /// The application has shout down with exception
        /// </summary>
        Fatal
    }
}
