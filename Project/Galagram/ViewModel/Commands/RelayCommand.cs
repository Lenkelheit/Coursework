namespace Galagram.ViewModel.Commands
{
    /// <summary>
    /// A WPF command which represents action as <see cref="System.Action"/>.
    /// </summary>
    public class RelayCommand : CommandBase
    {
        // FIELDS
        private System.Action<object> execute;
        private System.Func<object, bool> canExecute;

        // EVENT
        /// <summary>
        /// Raises when <see cref="CanExecute(object)"/> changed.
        /// </summary>
        public override event System.EventHandler CanExecuteChanged
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
        /// Initializes a new instance of <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="execute">
        /// A method that should be executed.
        /// </param>
        /// <param name="canExecute">
        /// A method that checks if command can be executed.
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
        /// The parameter that defines if command can be executed.
        /// </param>
        /// <returns>
        /// True — if command can be executed, otherwise — false.
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }
        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <param name="parameter">
        /// The parameter which command needs to be executed.
        /// </param>
        public override void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
