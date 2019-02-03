using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin.Message
{
    /// <summary>
    /// A logic class for <see cref="Window.Admin.UserControls.Messages.Single"/>
    /// </summary>
    public class SingleViewModel : ViewModelBase
    {
        // FIELDS
        DataAccess.Entities.Message message;
        
        ICommand goBackCommand;
        ICommand deleteCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="SingleViewModel"/>
        /// </summary>
        /// <param name="message">
        /// An instance of <see cref="DataAccess.Entities.Message"/> that should be displayed
        /// </param>
        public SingleViewModel(DataAccess.Entities.Message message)
        {
            this.message = message;

            goBackCommand = new Commands.Admin.GoBackCommand();
            deleteCommand = new Commands.Admin.DeleteCommand();
        }

        // PROPERTIES
        /// <summary>
        /// Gets displayed message
        /// </summary>
        public DataAccess.Entities.Message Message
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Message)}");

                return message;
            }
        }

        // COMMANDS
        /// <summary>
        /// Gets action to go back to previous content
        /// </summary>
        public ICommand GoBackCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(GoBackCommand)}");

                return goBackCommand;
            }
        }
        /// <summary>
        /// Gets action to delete current entity
        /// </summary>
        public ICommand DeleteCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(DeleteCommand)}");

                return deleteCommand;
            }
        }
    }
}
