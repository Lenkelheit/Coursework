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
        /// Initialize a new instance of <see cref="UploadPhotoCommand"/>
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
        /// Check if command can be executed
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
        /// Execute command
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

                int insertedPhotoCount = 0; // cuz Count() do not change after insert, needs for multiple photos adding
                foreach (var photoPath in photoNames)
                {
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"Photo path {photoPath}");
                    // copy photo to server
                    string serverPath = CopyPhotoToServer(
                        pathToPhoto: photoPath,
                        userId: mainWindowViewModel.DataStorage.LoggedUser.Id,
                        photoId: mainWindowViewModel.UnitOfWork.PhotoRepository.Count() + 1 + insertedPhotoCount);
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"Server photo path {serverPath}");

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
                    ++insertedPhotoCount;
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

        private string CopyPhotoToServer(string pathToPhoto, System.Guid userId, int photoId)
        {
            // create photo folder if neaded
            if (!System.IO.Directory.Exists(Core.Configuration.AppConfig.PHOTOS_SAVE_FOLDER))
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Create photo folder");
                System.IO.Directory.CreateDirectory(Core.Configuration.AppConfig.PHOTOS_SAVE_FOLDER);
            }
            // create folder for current user if neaded
            string userFolder = string.Join(System.IO.Path.DirectorySeparatorChar.ToString(), Core.Configuration.AppConfig.PHOTOS_SAVE_FOLDER, userId);
            if (!System.IO.Directory.Exists(userFolder))
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Create user folder");
                System.IO.Directory.CreateDirectory(userFolder);
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"User folder by path {userFolder} has been created");
            }


            // copy photo to server
            string serverPath = string.Format(Core.Configuration.AppConfig.PHOTOS_SAVE_PATH_FORMAT, userId, photoId, System.IO.Path.GetExtension(pathToPhoto));
            System.IO.File.Copy(pathToPhoto, serverPath);
            return serverPath;
        }
    }
}
