using System.IO;

namespace Galagram.ViewModel.Commands.User.MainWindow
{
    /// <summary>
    /// Opens open file dialog to select photo to upload
    /// </summary>
    public class UploadPhotoCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.MainWindowViewModel mainWindowViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="UploadPhotoCommand"/>
        /// </summary>
        /// <param name="mainWindowViewModel">
        /// An instance of <see cref="ViewModel.User.MainWindowViewModel"/>
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when passed command argument is null
        /// </exception>
        public UploadPhotoCommand(ViewModel.User.MainWindowViewModel mainWindowViewModel)
        {
            if (mainWindowViewModel == null) throw new System.ArgumentNullException(nameof(mainWindowViewModel));

            this.mainWindowViewModel = mainWindowViewModel;
        }

        // METHODS
        /// <summary>
        /// Checks if command can be executed
        /// </summary>
        /// <param name="parameter">
        /// Additional parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(UploadPhotoCommand)}");
            return true;
        }
        /// <summary>
        /// Executes command
        /// </summary>
        /// <param name="parameter">
        /// Command parameters
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(UploadPhotoCommand)}");

            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Open file dialog to upload photo");
            string[] photoNames = Services.WindowManager.Instance.OpenFileDialog(filterString: Core.Configuration.DBConfig.PHOTO_EXTENSION,
                                                                                 isMultiselectAllowed: true);
            // user select a photo
            if (photoNames != null)
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Adding photo");
                
                foreach (string photoPath in photoNames)
                {
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"Photo path {photoPath}");

                    // get random free photo name
                    string serverPath = GetRandomFreeName(mainWindowViewModel.DataStorage.LoggedUser.Id.ToString(), photoPath);
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"Server photo path {serverPath}");

                    // copy photo to server
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, "Copy photo to server");
                    CopyPhotoToServer(userId: mainWindowViewModel.DataStorage.LoggedUser.Id.ToString(),
                                      pathToPhoto: photoPath,
                                      serverPath: serverPath);

                    // create photo
                    DataAccess.Entities.Photo photo = new DataAccess.Entities.Photo
                    {
                        User = mainWindowViewModel.DataStorage.LoggedUser,
                        Path = serverPath
                    };

                    // add photo to a view, if only user is on his own page
                    if (mainWindowViewModel.IsCurrentUserShown)
                    {
                        Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Add photo to view");
                        mainWindowViewModel.Photos.Add(photo);
                    }

                    // insert photo to DB
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Insert photo to photo repositories");
                    mainWindowViewModel.UnitOfWork.PhotoRepository.Insert(photo);
                }

                // save to DB
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Save changes to DataBase");                             
                mainWindowViewModel.UnitOfWork.Save();

                // go to your profile, if can
                if (mainWindowViewModel.GoHomeCommand.CanExecute(null))
                {
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Go to your profile");
                    mainWindowViewModel.GoHomeCommand.Execute(null);
                }
            }
        }

        private string CopyPhotoToServer(string userId, string pathToPhoto, string serverPath)
        {
            // create photo folder if needed
            if (!Directory.Exists(Core.Configuration.AppConfig.PHOTOS_SAVE_FOLDER))
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Create photo folder");
                DirectoryInfo photosDirectory = Directory.CreateDirectory(Core.Configuration.AppConfig.PHOTOS_SAVE_FOLDER);
                photosDirectory.Attributes = Core.Configuration.AppConfig.PHOTOS_FOLDER_ATTRIBUTES;
            }

            // create folder for current user if neaded
            string userFolder = string.Join(Path.DirectorySeparatorChar.ToString(), Core.Configuration.AppConfig.PHOTOS_SAVE_FOLDER, userId);
            if (!Directory.Exists(userFolder))
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Create user folder");
                DirectoryInfo photoDirectory = Directory.CreateDirectory(userFolder);
                photoDirectory.Attributes = Core.Configuration.AppConfig.PHOTOS_FOLDER_ATTRIBUTES;

                Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"User folder by path {userFolder} has been created");
            }

            // copy photo to server
            File.Copy(pathToPhoto, serverPath);
            return serverPath;
        }
        private string GetRandomFreeName(string userId, string currentFilePath)
        {

            System.Random random = new System.Random();
            string serverPath;
            string fileExtension = Path.GetExtension(currentFilePath);

            // gets random free name
            do
            {
                int fileHashName = random.Next().GetHashCode();

                serverPath = string.Format(Core.Configuration.AppConfig.PHOTOS_SAVE_PATH_FORMAT, userId, fileHashName, fileExtension);
            } while (File.Exists(serverPath));

            return serverPath;
        }
    }
}
