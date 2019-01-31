namespace Core.Messages.Error
{
    /// <summary>
    /// Consists of messages that can be thrown in View project.
    /// </summary>
    public static class View
    {
        /// <summary>
        /// The filter string for DropWindow is invalid.
        /// </summary>
        public static readonly string DROP_WINDOW_WRONG_FILTER_STRING = "The filter string is invalid.";

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
        public static readonly string WINDOW_MANAGER_NO_SUCH_KEY_FORMAT = "Key \"{0}\" has not been registered before.";

        /// <summary>
        /// The behaviour for passed enum value is not implemented.
        /// </summary>
        public static readonly string WINDOW_MANAGER_MESSAGE_BOX_BUTTONS_WRONG_ENUM_VALUE = "Current MessageBoxButton enum value is not allowed.";

        /// <summary>
        /// Dialog window does not inherit default interface
        /// <para/>
        /// {0} — interface name
        /// </summary>
        public static readonly string WINDOW_MANAGER_DIALOG_DOES_NOT_INHERIT_DEFAULT_INTERFACE_FORMAT = string.Concat("Dialog does not inherit default interface", System.Environment.NewLine, "The default interface for this dialog is {0}");

        /// Modal window is not opened at all or is not opened as modal
        /// </summary>
        public static readonly string WINDOW_MANAGER_MODAL_WINDOW_IS_NOT_OPENED = "Modal window by current key is not opened";

    }
}
