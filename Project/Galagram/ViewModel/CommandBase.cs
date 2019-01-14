namespace Galagram.ViewModel
{
    /// <summary>
    /// An abstract class for all commands
    /// </summary>
    public abstract class CommandBase : System.Windows.Input.ICommand
    {
        /// <summary>
        /// Occurs when state of the command has been changed
        /// </summary>
        public event System.EventHandler CanExecuteChanged;
        /// <summary>
        /// Check if command  can be executed
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public abstract bool CanExecute(object parameter);
        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">
        /// Command parameter
        /// </param>
        public abstract void Execute(object parameter);

        /// <summary>
        /// Raise when state of command has been changed
        /// </summary>
        /// <param name="e">
        /// Event arguments
        /// </param>
        protected virtual void OnCanExecuteChanged(System.EventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }
    }
}
