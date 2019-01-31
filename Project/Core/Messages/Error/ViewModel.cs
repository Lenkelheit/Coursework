namespace Core.Messages.Error
{
    /// <summary>
    /// Consists of messages that can be thrown in ViewModel project.
    /// </summary>
    public static class ViewModel
    {
        #region EventToCommandBehavior
        /// <summary>
        /// The event of type is not found
        /// <para/>
        /// {0} — event name 
        /// <para/>
        /// {1} — event type 
        /// </summary>
        public static readonly string EVENT_OF_TYPE_IS_NOT_FOUND_FORMAT = "The event '{0}' was not found on type '{1}'";
        #endregion
    }
}
