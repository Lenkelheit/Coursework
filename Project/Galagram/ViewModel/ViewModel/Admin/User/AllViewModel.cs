using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin.User
{
    /// <summary>
    /// A logic class for <see cref="Window.Admin.UserControls.Users.All"/>
    /// </summary>
    public class AllViewModel : AllItemViewModelBase
    {
        // FIELDS
        ListCollectionView entities;
        string nicknameSubstring;

        ICommand openCommand;
        ICommand editCommand;
        ICommand setFilterCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="AllViewModel"/>
        /// </summary>
        public AllViewModel()
        {
            entities = new ListCollectionView(UnitOfWork.UserRepository.Get().ToArray());
            nicknameSubstring = string.Empty;

            // commands
            openCommand = new Commands.RelayCommand(NavigateToReadContent);
            editCommand = new Commands.RelayCommand(NavigateToEditContent);
            setFilterCommand = new Commands.Admin.User.All.SetFilterCommand(this);
            
            Logger.LogAsync(Core.LogMode.Debug, $"Initializes {nameof(AllViewModel)}");
        }

        // PROPERTIES
        /// <summary>
        /// Gets or sets filter substring
        /// </summary>
        public string NicknameSubstring
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(NicknameSubstring)} with value {nicknameSubstring}");

                return nicknameSubstring;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Info, $"Sets {nameof(NicknameSubstring)}. Old value = {nicknameSubstring}, new value = {nicknameSubstring}");

                SetProperty(ref nicknameSubstring, value);
            }
        }
        /// <summary>
        /// Gets filtered entites list
        /// </summary>
        public override ListCollectionView Entities
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(Entities)} in amount of {entities.Count}");

                return entities;
            }
        }

        // COMMANDS
        /// <summary>
        /// Gets action to navigate to read contet
        /// </summary>
        public override ICommand OpenCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(OpenCommand)}");

                return openCommand;
            }
        }
        /// <summary>
        /// Gets action to navigate to edit content
        /// </summary>
        public override ICommand EditCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(EditCommand)}");

                return editCommand;
            }
        }
        /// <summary>
        /// Gets action to set filters value
        /// </summary>
        public override ICommand SetFilterCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(SetFilterCommand)}");

                return setFilterCommand;
            }
        }
        // NAVIGATION METHODS
        private void NavigateToReadContent(object parameter)
        {
            // opens new content, single user
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Opens {typeof(Window.Admin.UserControls.Users.Single).FullName}");
            NavigationManager.NavigateTo(
                parent: DataStorage.AdminWindowContentControl,
                key: typeof(Window.Admin.UserControls.Users.Single).FullName,
                viewModel: new SingleViewModel(user: parameter as DataAccess.Entities.User, isReadOnly: true));
        }
        private void NavigateToEditContent(object parameter)
        {
            // opens new content, single user
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Opens {typeof(Window.Admin.UserControls.Users.Single).FullName}");
            NavigationManager.NavigateTo(
                parent: DataStorage.AdminWindowContentControl,
                key: typeof(Window.Admin.UserControls.Users.Single).FullName,
                viewModel: new SingleViewModel(user: parameter as DataAccess.Entities.User, isReadOnly: false));
        }

        #region Not Implemented Behaviour
        /// <summary>
        /// Not Implemented Behaviour
        /// </summary>
        public override ICommand CreateCommand => throw new System.NotImplementedException();
        #endregion
    }
}
