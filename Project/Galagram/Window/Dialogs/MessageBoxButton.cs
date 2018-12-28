namespace Galagram.Window.Dialogs
{
    /// <summary>
    ///  Specifies the buttons that are displayed on a message box. 
    /// <para/> 
    /// Used as an argument <see cref="Galagram.Services.WindowManager.ShowMessageWindow(string, string, MessageBoxButton)"/> method.
    /// </summary>
    public enum MessageBoxButton
    {
        /// <summary>
        /// The message box displays an OK button
        /// </summary>
        Ok = 0,
        /// <summary>
        /// The message box displays YES and NO buttons.
        /// </summary>
        YesNo = 1
    }
}
