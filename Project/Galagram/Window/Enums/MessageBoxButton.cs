namespace Galagram.Window.Enums
{
    /// <summary>
    /// Specifies the buttons that are displayed on a message box. 
    /// <para/> 
    /// Used as an argument in <see cref="Galagram.Window.Dialogs.MessageBox.ShowDialog(MessageBoxButton)"/> method.
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
