using System.Windows.Input;

namespace Galagram.ViewModel.Commands.Admin
{
    /// <summary>
    /// Represents multiple command
    /// </summary>
    public class MultipleCommand : CommandBase
    {
        // FIELDS
        ICommand[] commands;

        // EVENT
        /// <summary>
        /// Occurs when state of the command has been changed
        /// </summary>
        public override event System.EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="MultipleCommand"/>
        /// </summary>
        /// <param name="commands">
        /// The array of <see cref="ICommand"/>
        /// </param>
        public MultipleCommand(ICommand[] commands)
        {
            this.commands = commands;
        }

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
        public override bool CanExecute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(MultipleCommand)}");

            // check if all commands can be executed
            bool canExecute = true;
            foreach (ICommand command in commands)
            {
                canExecute &= command.CanExecute(parameter);
            }

            Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"{nameof(canExecute)} value = {canExecute}");

            // return
            return canExecute;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">
        /// Command parameter
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(MultipleCommand)}");

            foreach (ICommand command in commands)
            {
                command.Execute(parameter);
                if ((command as CommandBase).CommandState == Enums.Admin.CommandState.Interrupted) 
                {
                    // don't execute next command if current is interrupted
                    (command as CommandBase).CommandState = Enums.Admin.CommandState.Default;
                    return;
                }
            }
        }
    }
}
