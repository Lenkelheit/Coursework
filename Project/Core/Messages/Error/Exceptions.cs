namespace Core.Messages.Error
{
    /// <summary>
    /// Consits of all messeges happed in based on <see cref="System.Exception"/> classes
    /// </summary>
    public class Exceptions
    {
        #region Comparing Exception
        /// <summary>
        /// Comparing type is not allowed for entity
        /// <para/>
        /// {0} — name of wrong comparing type
        /// <para/>
        /// {1} — name of entity for which current comparing type is not allowed
        /// </summary>
        public static readonly string WRONG_COMPARING_TYPE_FORMAT = "Compare type {0} is not allowed for entity type of {1}";
        #endregion
    }
}
