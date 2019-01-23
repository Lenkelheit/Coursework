namespace Galagram.ViewModel.Commands.User.MainWindow
{
    /// <summary>
    /// Opens setting windom modal
    /// </summary>
    public class SettingCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.MainWindowViewModel mainWindowViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="SettingCommand"/>
        /// </summary>
        /// <param name="mainWindowViewModel">
        /// An instance of <see cref="ViewModel.User.MainWindowViewModel"/>
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when passed command argument is null
        /// </exception>
        public SettingCommand(ViewModel.User.MainWindowViewModel mainWindowViewModel)
        {
            if (mainWindowViewModel == null) throw new System.ArgumentNullException(nameof(mainWindowViewModel));

            this.mainWindowViewModel = mainWindowViewModel;
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(SettingCommand)}");
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(SettingCommand)}");
            
            /*
            // reference on temp avatar, in case if user changed his avatar
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Sets reference to temp avatar");
            // if user has no avatar, exception here
            string constAvatar = mainWindowViewModel.User.MainPhotoPath;
            string tempAvatar = string.Format(Core.Configuration.AppConfig.AVATAR_FORMAT, "temp" + mainWindowViewModel.User.Id, System.IO.Path.GetExtension(constAvatar));
            System.IO.File.Copy(constAvatar, tempAvatar, overwrite: true);
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"Avatar = {constAvatar}, temp avatar = {tempAvatar}");
            mainWindowViewModel.User.MainPhotoPath = tempAvatar;
            mainWindowViewModel.UpdateShownUser();
            */
            // open setting window
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Open modal Setting window");
            mainWindowViewModel.WindowManager.ShowWindowDialog(nameof(Window.User.Setting), new ViewModel.User.SettingViewModel());
            /*
            // reference back, maybe on new avatar
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Reference avatar back. Remove temp avatar");
            mainWindowViewModel.User.MainPhotoPath = constAvatar;
            
            // update user
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Update shown user");
            mainWindowViewModel.UpdateShownUser();

            // delete temp file
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Delete temp avatar path");
            System.IO.File.Delete(tempAvatar);// some one still have reference on this..
            */
        }
    }
}
