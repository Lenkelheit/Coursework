using System.Linq;
using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin.User
{
    /// <summary>
    /// A logic class for <see cref="Window.Admin.UserControls.Users.Single"/>
    /// </summary>
    public class SingleViewModel : SingleItemViewModelBase
    {
        // FIELDS
        string realNickname;
        string realAvatarPath;

        DataAccess.Wrappers.PhotoWrapper[] photos;
        int selectedPhotoIndex;

        ICommand resetAvatarCommand;

        ICommand showPhotoCommand;
        
        ICommand deleteOrUpdateCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="SingleViewModel"/>
        /// </summary>
        /// <param name="user">
        /// An instance of <see cref="DataAccess.Entities.User"/> to show
        /// </param>
        /// <param name="isEditingEnabled">
        /// Determines if entities can be changed
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Throwns when <paramref name="user"/> is null
        /// </exception>
        public SingleViewModel(DataAccess.Entities.User user, bool isEditingEnabled)
            : base(shownEntity: user, isWritingEnabled: isEditingEnabled)
        {
            this.realNickname = user.NickName;
            this.realAvatarPath = user.MainPhotoPath;

            this.photos = user.Photos.Select(photo => new DataAccess.Wrappers.PhotoWrapper(photo)).ToArray();
            this.selectedPhotoIndex = Core.Configuration.Constants.WRONG_INDEX;

            // command
            this.resetAvatarCommand = new Commands.RelayCommand((obj) =>
            {
                user.MainPhotoPath = null;
                OnPropertyChanged(nameof(ShownEntity));
            });

            this.showPhotoCommand = new Commands.RelayCommand(NavigateToPhoto);

            this.deleteOrUpdateCommand = isEditingEnabled ? (ICommand)new Commands.MultipleCommand(new CommandBase[]
                                                            {
                                                                new Commands.Admin.User.Single.ValidateCommand(this),
                                                                new Commands.Admin.UpdateCommand()
                                                            })
                                                          : (ICommand)new Commands.MultipleCommand(new CommandBase[]
                                                            {
                                                                new Commands.Admin.DeleteCommand(),
                                                                new Commands.Shared.DeletePhotoFolderCommand(((DataAccess.Entities.User)ShownEntity).Id.ToString()),
                                                                new Commands.Shared.DeleteAvatarFromServerCommand(((DataAccess.Entities.User)ShownEntity).MainPhotoPath)
                                                            });

            Logger.LogAsync(Core.LogMode.Debug, $"Initializes {nameof(SingleViewModel)}");
        }

        // PROPERTIES
        /// <summary>
        /// Gets user's real nickname
        /// </summary>
        public string RealNickname
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug | Core.LogMode.Info, $"Gets {nameof(RealNickname)} with value = {realNickname}");

                return realNickname;
            }
        }
        /// <summary>
        /// Gets original avatar path
        /// </summary>
        public string RealAvatarPath
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug | Core.LogMode.Info, $"Gets {nameof(RealAvatarPath)} with value = {realAvatarPath}");

                return realAvatarPath;
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
        public override string CrudOperationName
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(CrudOperationName)}");

                return IsWritingEnabled ? EditText: RemoveText;
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
        /// Gets action to delete or update entity
        /// </summary>
        public override ICommand CrudOperation
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(CrudOperation)}");

                return deleteOrUpdateCommand;
            }
        }

        // NAVIGATION METHODS
        private void NavigateToPhoto(object parameter)
        {
            if (selectedPhotoIndex != Core.Configuration.Constants.WRONG_INDEX)
            {
                // opens new content, photo
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Opens {typeof(Window.Admin.UserControls.Photo.Single).FullName}");
                NavigationManager.NavigateTo(
                    parent: DataStorage.AdminWindowContentControl,
                    key: typeof(Window.Admin.UserControls.Photo.Single).FullName,
                    viewModel: new Photo.SingleViewModel(photo: photos[selectedPhotoIndex].Photo, isEditingEnabled: IsWritingEnabled));
            }
        }
    }
}
