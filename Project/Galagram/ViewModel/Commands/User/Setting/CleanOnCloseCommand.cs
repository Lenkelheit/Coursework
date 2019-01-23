namespace Galagram.ViewModel.Commands.User.Setting
{
    /// <summary>
    /// Clean all garbage resources
    /// </summary>
    public class CleanOnCloseCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.SettingViewModel settingViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="CleanOnCloseCommand"/>
        /// </summary>
        /// <param name="settingViewModel">
        /// An instance of <see cref="ViewModel.User.SettingViewModel"/>
        /// </param>
        public CleanOnCloseCommand(ViewModel.User.SettingViewModel settingViewModel)
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(CleanOnCloseCommand)}");
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(CleanOnCloseCommand)}");

            // remove temp folder and all its content, if folder exist
            if (System.IO.Directory.Exists(Core.Configuration.AppConfig.TEMP_FOLDER))
            {
                settingViewModel.Logger.LogAsync(Core.LogMode.Debug, "Remove temp folder");
                System.IO.Directory.Delete(Core.Configuration.AppConfig.TEMP_FOLDER, true);
            }
        }
    }
}
