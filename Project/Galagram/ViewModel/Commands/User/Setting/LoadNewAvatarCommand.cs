namespace Galagram.ViewModel.Commands.User.Setting
{
    /// <summary>
    /// Loads a photo as a new avatar
    /// </summary>
    public class LoadNewAvatarCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.SettingViewModel settingViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="LoadNewAvatarCommand"/>
        /// </summary>
        /// <param name="settingViewModel">
        /// An instance of <see cref="ViewModel.User.SettingViewModel"/>
        /// </param>
        public LoadNewAvatarCommand(ViewModel.User.SettingViewModel settingViewModel)
        {
            this.settingViewModel = settingViewModel;
        }

        // METHODS
        /// <summary>
        /// Checks if command can be executed
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(LoadNewAvatarCommand)}");
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(LoadNewAvatarCommand)}");

            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Open file dialog");
            string[] fileNames = Services.WindowManager.Instance.OpenFileDialog(filterString: Core.Configuration.DBConfig.PHOTO_EXTENSION);

            if (fileNames != null)
            {
                settingViewModel.Logger.LogAsync(Core.LogMode.Debug, "Load new avatar");

                // create temp folder, and log it
                settingViewModel.CreateTempFolderIfNotExist();

                // get avatar path
                string localAvatarPath = fileNames[0];
                string serverTempAvatarPath = string.Format(Core.Configuration.AppConfig.TEMP_FILE_FORMAT, System.IO.Path.GetFileName(localAvatarPath));

                settingViewModel.Logger.LogAsync(Core.LogMode.Debug, $"New temp server path {serverTempAvatarPath}");

                // copy photo to temp folder
                System.IO.File.Copy(localAvatarPath, serverTempAvatarPath);

                // show temp photo
                settingViewModel.TempAvatarPath = serverTempAvatarPath;
            }
        }
    }
}
