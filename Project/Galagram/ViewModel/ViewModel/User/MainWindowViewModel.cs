using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Galagram.ViewModel.ViewModel.User
{
    /// <summary>
    /// An logic class for <see cref="Galagram.Window.User.MainWindow"/>
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        // FIELDS
        DataAccess.Entities.User loggedInUser; // user that logged in or signed up
        DataAccess.Entities.User currentPageUser; // user which page is shown
        int selectedPhotoIndex;
        ObservableCollection<DataAccess.Entities.Photo> photos;

        ICommand goHomeCommand;
        ICommand askQuestionCommand;
        ICommand followCommand;
        ICommand logOutCommand;
        ICommand searchUserCommand;
        ICommand settingCommand;
        ICommand showFollowersListCommand;
        ICommand showFollowingListCommand;
        ICommand showPhotoCommand;
        ICommand uploadPhotoCommand;
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="MainWindowViewModel"/>
        /// </summary>
        /// <param name="loggedUser">
        /// Current user
        /// </param>
        /// <param name="shownUser">
        /// A user, which profile is shown
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when logged user is null
        /// </exception>
        public MainWindowViewModel(DataAccess.Entities.User loggedUser, DataAccess.Entities.User shownUser)
        {
            if (loggedUser == null) throw new System.ArgumentNullException(nameof(loggedUser));
            if (shownUser == null) throw new System.ArgumentNullException(nameof(shownUser));

            this.loggedInUser = loggedUser;
            this.currentPageUser = shownUser;
            this.selectedPhotoIndex = Core.Configuration.Constants.WRONG_INDEX;
            this.photos = new ObservableCollection<DataAccess.Entities.Photo>(currentPageUser.Photos);

            this.goHomeCommand = new Commands.User.MainWindow.GoHomeCommand(this);
            this.askQuestionCommand = new Commands.User.MainWindow.AskQuestionCommand(this);
            this.followCommand = new Commands.User.MainWindow.FollowCommand(this);
            this.logOutCommand = new Commands.User.MainWindow.LogOutCommand(this);
            this.searchUserCommand = new Commands.User.MainWindow.SearchUserCommand(this);
            this.settingCommand = new Commands.User.MainWindow.SettingCommand(this);
            this.showFollowersListCommand = new Commands.User.MainWindow.ShowFollowersListCommand(this);
            this.showFollowingListCommand = new Commands.User.MainWindow.ShowFollowingListCommand(this);
            this.showPhotoCommand = new Commands.User.MainWindow.ShowPhotoCommand(this);
            this.uploadPhotoCommand = new Commands.User.MainWindow.UploadPhotoCommand(this);
        }
        // PROPERTIES
        /// <summary>
        /// Gets or sets user that is shown
        /// </summary>
        public DataAccess.Entities.User User
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(User)}");
                return currentPageUser;
            }
            set
            {
                currentPageUser = value;
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Sets {nameof(User)}");
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets selected photo index
        /// </summary>
        public int SelectedPhotoIndex
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(SelectedPhotoIndex)} value {this.selectedPhotoIndex}");
                return selectedPhotoIndex;
            }
            set
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Sets {nameof(SelectedPhotoIndex)} value {this.selectedPhotoIndex} to {value}");
                selectedPhotoIndex = value;
            }
        }
        /// <summary>
        /// Gets photo collection
        /// </summary>
        public ObservableCollection<DataAccess.Entities.Photo> Photos
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Photos)} with amount {photos.Count}");
                return photos;
            }
        }
        // COMMANDS
        internal DataAccess.Entities.User LoggedUser => loggedInUser;
        /// <summary>
        /// Gets true if current user and shown user is the same, otherwise — false
        /// </summary>
        public bool IsCurrentUserShown => loggedInUser == currentPageUser;
        /// <summary>
        /// Gets go home action
        /// </summary>
        public ICommand GoHomeCommand
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Get {nameof(GoHomeCommand)}");
                return goHomeCommand;
            }
        }
        /// <summary>
        /// Gets action to search user
        /// </summary>
        public ICommand SearchUserCommand
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(SearchUserCommand)}");
                return searchUserCommand;
            }
        }
        /// <summary>
        /// Gets action to upload photo
        /// </summary>
        public ICommand UploadPhotoCommand
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(UploadPhotoCommand)}");
                return uploadPhotoCommand;
            }
        }
        /// <summary>
        /// Gets action that open modal window with ask question option
        /// </summary>
        public ICommand AskQuestionCommand
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Get {nameof(AskQuestionCommand)}");
                return askQuestionCommand;
            }
        }
        /// <summary>
        /// Gets setting action
        /// </summary>
        public ICommand SettingCommand
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(SettingCommand)}");
                return settingCommand;
            }
        }
        /// <summary>
        /// Gets action that log out system
        /// </summary>
        public ICommand LogOutCommand
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Get {nameof(LogOutCommand)}");
                return logOutCommand;
            }
        }
        /// <summary>
        /// Gets action to show shown user followers
        /// </summary>
        public ICommand ShowFollowersListCommand
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(ShowFollowersListCommand)}");
                return showFollowersListCommand;
            }
        }
        /// <summary>
        /// Gets action to show shown user following
        /// </summary>
        public ICommand ShowFollowingListCommand
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(ShowFollowingListCommand)}");
                return showFollowersListCommand;
            }
        }
        /// <summary>
        /// Gets action to show selected photo
        /// </summary>
        public ICommand ShowPhotoCommand
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(ShowPhotoCommand)}");
                return showPhotoCommand;
            }
        }
        /// <summary>
        /// Gets action to follow shown user
        /// </summary>
        public ICommand FollowCommand
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(FollowCommand)}");
                return followCommand;
            }
        }
        
        // METHODS
        /// <summary>
        /// Sets logged user as shown one
        /// </summary>
        public void GoToCurrentUser()
        {
            currentPageUser = loggedInUser;
            photos = new ObservableCollection<DataAccess.Entities.Photo>(currentPageUser.Photos);

            OnPropertyChanged(nameof(User));
            OnPropertyChanged(nameof(Photos));
        }
    }
}
