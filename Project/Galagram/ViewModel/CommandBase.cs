namespace Galagram.ViewModel
{
    /// <summary>
    /// An abstract class for all commands
    /// </summary>
    public abstract class CommandBase : System.Windows.Input.ICommand
    {
        // FIELDS
        Enums.Admin.CommandState commandState;

        // CONSTRUCTORS
        /// <summary>
        /// Sets default field value
        /// </summary>
        public CommandBase()
        {
            commandState = Enums.Admin.CommandState.Default;
        }

        // PROPERTIES
        /// <summary>
        /// Gets state of the command executing
        /// </summary>
        public Enums.Admin.CommandState CommandState
        {
            get
            {
                return commandState;
            }
            protected set
            {
                commandState = value;
            }
        }

        // EVENTS
        /// <summary>
        /// Occurs when state of the command has been changed
        /// </summary>
        public virtual event System.EventHandler CanExecuteChanged;

        // METHODS
        /// <summary>
        /// Checks if command can be executed
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public abstract bool CanExecute(object parameter);
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">
        /// Command parameter
        /// </param>
        public abstract void Execute(object parameter);

        /// <summary>
        /// Raises when state of command has been changed
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