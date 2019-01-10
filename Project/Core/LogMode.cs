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
        /// </summary>
        Warn,
        /// <summary>
        /// Error has been occurred
        /// </summary>
        Error,
        /// <summary>
        /// Immediately help required
        /// </summary>
        Fatal
    }
}
