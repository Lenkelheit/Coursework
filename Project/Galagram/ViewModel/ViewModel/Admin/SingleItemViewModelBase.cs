using System.Windows.Input;

using DataAccess.Interfaces;

namespace Galagram.ViewModel.ViewModel.Admin
{
    /// <summary>
    /// Eepresents abstract class for single item view model
    /// </summary>
    public abstract class SingleItemViewModelBase : ViewModelBase
    {
        // CONST
        /// <summary>
        /// Create text
        /// </summary>
        protected static readonly string CreateText = "Create";
        /// <summary>
        /// Remove text
        /// </summary>
        protected static readonly string RemoveText = "Delete";
        /// <summary>
        /// Edit text
        /// </summary>
        protected static readonly string EditText = "Save Changes";

        // FIELDS
        IEntity shownEntity;
        bool isWritingEnabled;

        ICommand goBackCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="SingleItemViewModelBase"/>
        /// </summary>
        /// <param name="shownEntity">
        /// The entity to show
        /// </param>
        /// <param name="isWritingEnabled">
        /// Determines if editing is allowed
        /// </param>
        public SingleItemViewModelBase(IEntity shownEntity, bool isWritingEnabled)
        {
            if (shownEntity == null) throw new System.ArgumentException(nameof(shownEntity));

            this.shownEntity = shownEntity;
            this.isWritingEnabled = isWritingEnabled;

            goBackCommand = new Commands.Admin.GoBackCommand();
        }

        // PROPERTIES
        /// <summary>
        /// Gets shown entity
        /// </summary>
        public IEntity ShownEntity
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(ShownEntity)} of type {shownEntity.GetType().Name}");

                return shownEntity;
            }
        }
        /// <summary>
        /// Gets value that determins is editing allowed
        /// </summary>
        public bool IsWritingEnabled
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(IsWritingEnabled)} with value = {isWritingEnabled}");

                return isWritingEnabled;
            }
        }
        /// <summary>
        /// When overridden in a derived class, gets crud operation name (create, update, delete)
        /// </summary>
        public abstract string CrudOperationName { get; }

        // COMMANDS
        /// <summary>
        /// Gets action to show previous value
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
        /// When overridden in a derived class, gets crud operation's action
        /// </summary>
        public abstract ICommand CrudOperation { get; }            
    }
}
