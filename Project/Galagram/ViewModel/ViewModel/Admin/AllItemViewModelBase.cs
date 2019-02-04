using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin
{
    /// <summary>
    /// Represents basic abstract class for panels where all items from DataBase are shown
    /// </summary>
    public abstract class AllItemViewModelBase : ViewModelBase
    {
        // FIELDS
        DataAccess.Interfaces.IEntity selectedEntity;

        ICommand deleteCommand;

        ICommand resetFilter;

        // CONSTRUCTORS
        /// <summary>
        /// Sets default value
        /// </summary>
        public AllItemViewModelBase()
        {
            selectedEntity = null;

            deleteCommand = new Commands.Admin.DeleteCommand();

            resetFilter = new Commands.Admin.ResetFilterCommand(this);
        }

        // PROPERTIES
        #region Items
        /// <summary>
        /// Gets or sets selected item
        /// </summary>
        public DataAccess.Interfaces.IEntity SelectedItem
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(SelectedItem)}");

                return selectedEntity;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Sets {nameof(SelectedItem)}");

                SetProperty(ref selectedEntity, value);
            }
        }
        /// <summary>
        /// Gets an collection of entites
        /// </summary>
        public abstract System.Windows.Data.ListCollectionView Entities { get; }
        #endregion

        // CRUD
        #region CRUD
        /// <summary>
        /// When overridden in a derived class, return command to opens view with creating new entity
        /// </summary>
        public abstract ICommand CreateCommand { get; }
        /// <summary>
        /// When overridden in a derived class, return command to opens view with full information about entity
        /// </summary>
        public abstract ICommand OpenCommand { get; }
        /// <summary>
        /// When overridden in a derived class, return command to opens view with editing entity
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
        #endregion

        // FILTER
        #region Filter
        /// <summary>
        /// Gets or sets filter valuee
        /// </summary>
        public System.Predicate<object> Filter
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Filter)}");

                return Entities.Filter;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Sets {nameof(Filter)}");

                Entities.Filter = value;
            }
        }
        /// <summary>
        /// When overridden in a derived class, gets action to set filter
        /// </summary>
        public abstract ICommand SetFilterCommand { get; }
        /// <summary>
        /// When overridden in a derived class, gets action to reset filter
        /// </summary>
        public ICommand ResetFilterCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(resetFilter)}");

                return resetFilter;
            }
        }
        #endregion
    }
}
