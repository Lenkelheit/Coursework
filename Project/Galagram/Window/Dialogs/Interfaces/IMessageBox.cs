namespace Galagram.Window.Dialogs.Interfaces
{
    /// <summary>
    /// Provides basic interaction for Message Boxes.
    /// </summary>
    public interface IMessageBox
    {
        /// <summary>
        /// Gets or sets the header of MessageBox.
        /// </summary>
        string Header { get; set; }
        /// <summary>
        /// Gets or sets the text message of MessageBox.
        /// </summary>
        string Text { get; set; }
    }
}
