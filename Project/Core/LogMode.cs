namespace Core
{
    /// <summary>
    /// Log message level
    /// </summary>
    [System.Flags]
    public enum LogMode
    {
        /// <summary>
        /// Uses for deactivation log modes.
        /// <para/>
        /// To turn off other modes, set up them to <see cref="Off"/>.
        /// </summary>
        Off = 0,
        /// <summary>
        /// Uses for activation log modes.
        /// <para/>
        /// To turn on other modes, set up them to <see cref="On"/>.
        /// </summary>
        On = Info | Debug | Warn | Error | Fatal,
        /// <summary>
        /// Explain logic step by step
        /// </summary>
        Debug = 2,
        /// <summary>
        /// Information about work, its efficiency
        /// </summary>
        Info = 4,
        /// <summary>
        /// Something weird happen
        /// <para/>
        /// Caught exception known type 
        /// </summary>
        Warn = 8,
        /// <summary>
        /// Error has been occurred
        /// <para/>
        /// Caught exception unknown type 
        /// </summary>
        Error = 16,
        /// <summary>
        /// Immediately help required
        /// <para/>
        /// The application has shout down with exception
        /// </summary>
        Fatal = 32
    }
}
