namespace Galagram.Window.Interfaces
{
    /// <summary>
    /// Provides the basic mechanism for interaction with FileDialog
    /// </summary>
    public interface IFileDialog
    {
        /// <summary>
        /// Gets or sets an option indicating whether dialog allows users to select multiple files.
        /// </summary>
        bool Multiselect { get; set; }
        /// <summary>
        /// Gets an array that contains one file name for each selected file.
        /// </summary>
        string[] FileNames { get; }
        /// <summary>
        /// Gets or sets the filter string that determines what types of files are allowed
        /// </summary>
        string Filter { get; set; }

        /// <summary>
        /// Open a window and returns only when the newly opened window is closed
        /// </summary>
        /// <returns>
        /// A <see cref="System.Nullable"/> value of type <see cref="System.Boolean"/> that specifies whether the activity was accepted (true) or canceled (false).
        /// <para/>
        /// The return value is the value of the <see cref="System.Windows.Window.DialogResult"/> property before a window closes.
        /// </returns> 
        bool? ShowDialog();
    }
}
