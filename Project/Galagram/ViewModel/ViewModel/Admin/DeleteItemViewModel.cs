using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin
{
    /// <summary>
    /// A logic class for <see cref="Window.Admin.UserControls.DeleteItem"/>
    /// </summary>
    public class DeleteItemViewModel : ViewModelBase
    {
        // FIELDS
        DataAccess.Interfaces.IEntity entity;

        ICommand cancelCommand;
        ICommand acceptCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="DeleteItemViewModel"/>
        /// </summary>
        /// <param name="entity">
        /// An entity to delete
        /// </param>
        public DeleteItemViewModel(DataAccess.Interfaces.IEntity entity)
        {
            this.entity = entity;

            cancelCommand = new Commands.Admin.GoBackCommand();
            acceptCommand = new Commands.Admin.DeleteItem.DeleteItemCommand(this);

            Logger.LogAsync(Core.LogMode.Debug, $"Initialize a new instance of {nameof(DeleteItemViewModel)}");
        }

        // PROPERTIES
        /// <summary>
        /// Gets entity id
        /// </summary>
        public string ID
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets entity {nameof(ID)} with value = {entity.Id}");

                return entity.Id.ToString();
            }
        }
        /// <summary>
        /// Gets entity name
        /// </summary>
        public string Name
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets entity {nameof(Name)} with value = {entity.ToString(DataAccess.Enums.EntityStringFormat.Name)}");

                return entity.ToString(DataAccess.Enums.EntityStringFormat.Name);
            }
        }
        /// <summary>
        /// Gets entity to delete
        /// </summary>
        public DataAccess.Interfaces.IEntity Entity
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, "Gets entity to delete");

                return entity;
            }
        }

        // COMMANDS
        /// <summary>
        /// Gets cancel command action
        /// </summary>
        public ICommand CancelCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(CancelCommand)}");

                return cancelCommand;
            }
        }
        /// <summary>
        /// Gets deleting action
        /// </summary>
        public ICommand AcceptCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(AcceptCommand)}");

                return acceptCommand;
            }
        }
    }
}
