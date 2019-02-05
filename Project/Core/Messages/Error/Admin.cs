namespace Core.Messages.Error
{
    /// <summary>
    /// Consists of messages happened in Admin section
    /// </summary>
    public static class Admin
    {
        /// <summary>
        /// Can not get data from Data Base.
        /// <para/>
        /// {0} — entity's name
        /// <para/>
        /// {1} — entity's id
        /// </summary>
        public static readonly string ROW_MISSING_FORMAT = string.Concat("Table with entity {0} missing a row.", System.Environment.NewLine, "Entity id is {1}");

        #region MenuItem to ViewModel factory
        /// <summary>
        /// Current key has not been registered before.
        /// <para/>
        /// Takes key name.
        /// </summary>
        public static readonly string FACTORY_NO_SUCH_KEY_FORMAT = "Key \"{0}\" has not been registered before.";
        /// <summary>
        /// Current key has been already registered.
        /// <para/>
        /// Takes key name.
        /// </summary>
        public static readonly string FACTORY_REGISTRATE_BY_THE_SAME_KEY_FORMAT = "Type by key {0} has been already registered.";
        #endregion
    }
}
