namespace Galagram.Window.Interfaces
{
    /// <summary>
    /// Provides the basic mechanism for interaction with MessageBox
    /// </summary>
    public interface IMessageBox
    {
        /// <summary>
        /// Gets or sets message box header
        /// </summary>
        string Header { get; set; }
        /// <summary>
        /// Gets or sets message box text
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Open a window and returns only when the newly opened window is closed
        /// </summary>
        /// <param name="messageBoxButton">
        /// Specifies the buttons that are displayed on a message box. 
        /// </param>
        /// <returns>
        /// A <see cref="System.Nullable"/> value of type <see cref="System.Boolean"/> that specifies whether the activity was accepted (true) or canceled (false).
        /// <para/>
        /// The return value is the value of the <see cref="System.Windows.Window.DialogResult"/> property before a window closes.
        /// </returns>   
        bool? ShowDialog(Enums.MessageBoxButton messageBoxButton);

    }
}
