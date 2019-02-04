using System.Linq;
using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin.User
{
    /// <summary>
    /// A logic class for <see cref="Window.Admin.UserControls.Users.Single"/>
    /// </summary>
    public class SingleViewModel : ViewModelBase
    {
        // FIELDS
        DataAccess.Entities.User user;
        bool isReadOnly;

        DataAccess.Wrappers.PhotoWrapper[] photos;
        int selectedPhotoIndex;

        ICommand resetAvatarCommand;

        ICommand showPhotoCommand;

        ICommand goBackCommand;
        ICommand deleteOrUpdateCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="SingleViewModel"/>
        /// </summary>
        /// <param name="user">
        /// An instance of <see cref="DataAccess.Entities.User"/> to show
        /// </param>
        /// <param name="isReadOnly">
        /// Determines if entities can be changed
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Throwns when <paramref name="user"/> is null
        /// </exception>
        public SingleViewModel(DataAccess.Entities.User user, bool isReadOnly)
        {
            if (user == null) throw new System.ArgumentNullException();

            this.user = user;
            this.isReadOnly = isReadOnly;

            this.photos = user.Photos.Select(photo => new DataAccess.Wrappers.PhotoWrapper(photo)).ToArray();
            this.selectedPhotoIndex = Core.Configuration.Constants.WRONG_INDEX;

            // command
            this.resetAvatarCommand = new Commands.RelayCommand((obj) =>
            {
                user.MainPhotoPath = null;
                OnPropertyChanged(nameof(User));
            });

            this.showPhotoCommand = new Commands.RelayCommand(NavigateToPhoto);

            this.goBackCommand = new Commands.Admin.GoBackCommand();
            this.deleteOrUpdateCommand = isReadOnly ? (ICommand)new Commands.Admin.DeleteCommand()
                                                    : (ICommand)new Commands.Admin.UpdateCommand();

            Logger.LogAsync(Core.LogMode.Debug, $"Initializes {nameof(SingleViewModel)}");
        }

        // PROPERTIES
        /// <summary>
        /// Gets shown user
        /// </summary>
        public DataAccess.Entities.User User
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(User)}");

                return user;
            }
        }
        /// <summary>
        /// Gets value that determines if entity can be changed
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(IsReadOnly)}");

                return isReadOnly;
            }
        }
        /// <summary>
        /// Gets or sets selected photo index
        /// </summary>
        public int SelectedPhotoIndex
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(SelectedPhotoIndex)} with value = {selectedPhotoIndex}");

                return selectedPhotoIndex;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Info, $"Sets {nameof(SelectedPhotoIndex)}. Old value = {selectedPhotoIndex}, new value = {value}");

                SetProperty(ref selectedPhotoIndex, value);
            }
        }
        /// <summary>
        /// Gets photos list
        /// </summary>
        public DataAccess.Wrappers.PhotoWrapper[] Photos
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Photos)} in amount of {photos.Length}");

                return photos;
            }
        }
        /// <summary>
        /// Gets or sets allowed operation name
        /// </summary>
        public string OperationName
        {
            get
            {
                return isReadOnly ? "Delete" : "Save Changes";
            }
        }

        // COMMANDS
        /// <summary>
        /// Gets action that navigate to selected photo
        /// </summary>
        public ICommand ShowPhotoCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(ShowPhotoCommand)}");

                return showPhotoCommand;
            }
        }
        /// <summary>
        /// Gets action to reset avatar 
        /// </summary>
        public ICommand ResetAvatarCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(ResetAvatarCommand)}");

                return resetAvatarCommand;
            }
        }
        /// <summary>
        /// Gets action to navigate to previous content
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
        /// Gets action to delete or update entity
        /// </summary>
        public ICommand DeleteOrUpdateCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(DeleteOrUpdateCommand)}");

                return deleteOrUpdateCommand;
            }
        }

        // NAVIGATION METHODS
        private void NavigateToPhoto(object parameter)
        {
            // opens new content, photo
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Opens {typeof(Window.Admin.UserControls.Photo.Single).FullName}");
            NavigationManager.NavigateTo(
                parent: DataStorage.AdminWindowContentControl,
                key: typeof(Window.Admin.UserControls.Photo.Single).FullName,
                viewModel: new Photo.SingleViewModel(photo: parameter as DataAccess.Entities.Photo, isReadOnly: isReadOnly));
        }
    }
}
