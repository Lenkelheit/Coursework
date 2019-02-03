using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin
{
    /// <summary>
    /// Represents basic abstract class for panels where all items from DataBase are shown
    /// </summary>
    public abstract class AllItemViewModelBase : ViewModelBase
    {
        // FIELDS
        ICommand deleteCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Sets default value
        /// </summary>
        public AllItemViewModelBase()
        {
            deleteCommand = new Commands.Admin.DeleteCommand();
        }

        /// <summary>
        /// Gets or sets selected entity
        /// </summary>
        public abstract DataAccess.Interfaces.IEntity SelectedItem { get; set; }

        /// <summary>
        /// when overridden in a derived class, return command to opens view with creating new entity
        /// </summary>
        public abstract ICommand CreateCommand { get; }
        /// <summary>
        /// when overridden in a derived class, return command to opens view with full information about entity
        /// </summary>
        public abstract ICommand OpenCommand { get; }
        /// <summary>
        /// when overridden in a derived class, return command to opens view with editing entity
        /// </summary>
        public abstract ICommand EditCommand { get; }
        /// <summary>
        /// Return command to opens view with deleting entity
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
