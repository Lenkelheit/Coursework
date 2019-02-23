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
        bool? isBlocked;

        ICommand openCommand;
        ICommand editCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="AllViewModel"/>
        /// </summary>
        public AllViewModel()
        {
            entities = new ListCollectionView(UnitOfWork.UserRepository.Get().ToArray());
            nicknameSubstring = string.Empty;
            isBlocked = null;

            // commands
            openCommand = new Commands.RelayCommand(NavigateToReadContent);
            editCommand = new Commands.RelayCommand(NavigateToEditContent);
            deleteCommand = new Commands.MultipleCommand(new CommandBase[]
            {
                new Commands.Admin.DeleteCommand(),
                new Commands.Shared.DeletePhotoFolderCommand(),
                new Commands.Shared.DeleteAvatarFromServerCommand()
            });


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
                Logger.LogAsync(Core.LogMode.Info, $"Sets {nameof(NicknameSubstring)}. Old value = {nicknameSubstring}, new value = {value}");

                SetProperty(ref nicknameSubstring, value);
            }
        }
        /// <summary>
        /// Gets or sets filter value that determines if user is blocked
        /// </summary>
        public bool? IsBlocked
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug | Core.LogMode.Info, $"Gets {nameof(IsBlocked)} with value = {isBlocked}");

                return isBlocked;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug | Core.LogMode.Info, $"Sets {nameof(IsBlocked)}. Old value = {isBlocked}, new value = {value}");

                SetProperty(ref isBlocked, value);
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

        // METHODS
        /// <summary>
        /// Sets filter predicate
        /// </summary>
        /// <param name="entity">
        /// The entities for which predicate is applied
        /// </param>
        /// <returns>
        /// Boolean values which determines if entity is allowed by predicate or not
        /// </returns>
        protected override bool FilterPredicate(object entity)
        {
            // gets user
            DataAccess.Entities.User user = (DataAccess.Entities.User)entity;
            bool isShown = true;

            // filtering
            if (isBlocked != null)
            {
                isShown &= user.IsBlocked == isBlocked.Value;
            }

            isShown &= DataAccess.Filters.UserFilter.Has(user, nicknameSubstring);

            return isShown;
        }

        // NAVIGATION METHODS
        private void NavigateToReadContent(object parameter)
        {
            // opens new content, single user
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Opens {typeof(Window.Admin.UserControls.Users.Single).FullName}");
            NavigationManager.NavigateTo(
                parent: DataStorage.AdminWindowContentControl,
                key: typeof(Window.Admin.UserControls.Users.Single).FullName,
                viewModel: new SingleViewModel(user: parameter as DataAccess.Entities.User, isEditingEnabled: false));
        }
        private void NavigateToEditContent(object parameter)
        {
            // opens new content, single user
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Opens {typeof(Window.Admin.UserControls.Users.Single).FullName}");
            NavigationManager.NavigateTo(
                parent: DataStorage.AdminWindowContentControl,
                key: typeof(Window.Admin.UserControls.Users.Single).FullName,
                viewModel: new SingleViewModel(user: parameter as DataAccess.Entities.User, isEditingEnabled: true));
        }


        #region Not Implemented Behaviour
        /// <summary>
        /// Not Implemented Behaviour
        /// </summary>
        public override ICommand CreateCommand => throw new System.NotImplementedException();
        #endregion
    }
}
