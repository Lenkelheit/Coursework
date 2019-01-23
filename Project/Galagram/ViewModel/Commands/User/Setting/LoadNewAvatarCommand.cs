using Microsoft.Win32;

namespace Galagram.ViewModel.Commands.User.Setting
{
    /// <summary>
    /// Load a photo as a new avatar
    /// </summary>
    public class LoadNewAvatarCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.SettingViewModel settingViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="LoadNewAvatarCommand"/>
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
        /// Check if command can be executed
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
        /// Execute command
        /// </summary>
        /// <param name="parameter">
        /// Command parameters
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(LoadNewAvatarCommand)}");

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = false,
            };
            
            if (openFileDialog.ShowDialog() == true)
            {
                settingViewModel.Logger.LogAsync(Core.LogMode.Debug, "Load new avatar");

                // create temp folder, and log it
                settingViewModel.CreateTempFolderIfNotExist();

                // get random name
                string localAvatarPath = openFileDialog.FileName;
                string serverTempAvatarPath = settingViewModel.GetRandomTempFileName(fileExtension: System.IO.Path.GetExtension(localAvatarPath));

                settingViewModel.Logger.LogAsync(Core.LogMode.Debug, $"New temp server path {serverTempAvatarPath}");

                // copy photo to temp folder
                System.IO.File.Copy(localAvatarPath, serverTempAvatarPath);

                // show temp photo
                settingViewModel.TempAvatarPath = serverTempAvatarPath;
            }
        }
    }
}
