using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin.Message
{
    /// <summary>
    /// A logic class for <see cref="Window.Admin.UserControls.Messages.Single"/>
    /// </summary>
    public class SingleViewModel : SingleItemViewModelBase
    {
        // FIELDS
        DataAccess.Entities.Message message;

        ICommand deleteCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="SingleViewModel"/>
        /// </summary>
        /// <param name="message">
        /// An instance of <see cref="DataAccess.Entities.Message"/> that should be displayed
        /// </param>
        public SingleViewModel(DataAccess.Entities.Message message) : base(message, false)
        {
            this.message = message;
            
            deleteCommand = new Commands.Admin.DeleteCommand();
        }

        // PROPERTIES
        /// <summary>
        /// Gets allowed operation name
        /// </summary>
        public override string CrudOperationName
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(CrudOperationName)}, with value = {RemoveText}");

                return RemoveText;
            }
        }

        // COMMANDS
        /// <summary>
        /// Gets action to delete current entity
        /// </summary>
        public override ICommand CrudOperation 
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(CrudOperation)}");

                return deleteCommand;
            }
        }
    }
}
