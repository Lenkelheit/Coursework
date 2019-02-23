using System.Windows.Input;

namespace Galagram.ViewModel.Commands
{
    /// <summary>
    /// Represents multiple command
    /// <para/>
    /// Implements Chain-of-Responsibility pattern
    /// </summary>
    public class MultipleCommand : CommandBase
    {
        // FIELDS
        CommandBase[] commands;

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
        /// Initializes a new instance of <see cref="MultipleCommand"/>
        /// </summary>
        /// <param name="commands">
        /// The array of <see cref="CommandBase"/>
        /// </param>
        public MultipleCommand(CommandBase[] commands)
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
            // check if all commands can be executed
            foreach (CommandBase command in commands)
            {
                if (!command.CanExecute(parameter))
                {
                    return false;
                }
            }
            
            // all command can be executed
            return true;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">
        /// Command parameter
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(MultipleCommand)}");

            // execute command one by one
            foreach (CommandBase command in commands)
            {
                command.Execute(parameter);

                // stop command executing, if current command has been interrupted
                if (command.CommandState == Enums.Admin.CommandState.Interrupted) 
                {
                    return;
                }
            }
        }
    }
}
