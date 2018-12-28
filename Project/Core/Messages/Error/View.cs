namespace Core.Messages.Error
{
    /// <summary>
    /// Consist of messages that can be thrown in View.
    /// </summary>
    public static class View
    {
        /// <summary>
        /// Passed argument is an interface or abstract class type. WindowManager can not registrate this type. 
        /// <para/>
        /// Takes argument name.
        /// </summary>
        public static readonly string WINDOW_MANAGER_REGISTRATE_INTERFACE_FORMAT = "Can not registrate an interface or abstract class. Argument name is {0}.";

        /// <summary>
        /// Current key has been already registered.
        /// <para/>
        /// Takes key name.
        /// </summary>
        public static readonly string WINDOW_MANAGER_REGISTRATE_BY_THE_SAME_KEY_FORMAT = "Type by key {0} has been already registered.";

        /// <summary>
        /// Current key has not been registered before.
        /// </summary>
        public static readonly string WINDOW_MANAGER_NO_SUCH_KEY_FORMAT= "Key \"{0}\" has not been registered before. ";

        /// <summary>
        /// The behaviour for passed enum value is not implemented.
        /// </summary>
        public static readonly string WINDOW_MANAGER_MESSAGE_BOX_BUTTONS_WONG_ENUM_VALUE = "Current MessageBoxButton enum value is not allowed.";
    }
}
