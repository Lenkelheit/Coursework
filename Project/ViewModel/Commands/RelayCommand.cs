namespace ViewModel.Commands
{
    /// <summary>
    /// A WPF command which represent action as <see cref="System.Action"/>.
    /// </summary>
    public class RelayCommand : System.Windows.Input.ICommand
    {
        // FIELDS
        private System.Action<object> execute;
        private System.Func<object, bool> canExecute;

        // EVENT
        /// <summary>
        /// Raise when <see cref="CanExecute(object)"/> changed.
        /// </summary>
        public event System.EventHandler CanExecuteChanged
        {
            add
            {
                System.Windows.Input.CommandManager.RequerySuggested += value;
            }
            remove
            {
                System.Windows.Input.CommandManager.RequerySuggested -= value;
            }
        }

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="execute">
        /// An method that should be executed.
        /// </param>
        /// <param name="canExecute">
        /// An method that checks if command can be executed.
        /// </param>
        public RelayCommand(System.Action<object> execute, System.Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        // METHODS
        /// <summary>
        /// Checks if command can be executed.
        /// </summary>
        /// <param name="parameter">
        /// The parameter that define if command can be executed.
        /// </param>
        /// <returns>
        /// True — if command can be executed, otherwise — false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }
        /// <summary>
        /// Execute a command.
        /// </summary>
        /// <param name="parameter">
        /// The parameter which command needs to be executed.
        /// </param>
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
