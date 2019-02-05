namespace Galagram.ViewModel.Commands.User.Setting
{
    /// <summary>
    /// Cleans all garbage resources
    /// </summary>
    public class CloseCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.SettingViewModel settingViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="CloseCommand"/>
        /// </summary>
        /// <param name="settingViewModel">
        /// An instance of <see cref="ViewModel.User.SettingViewModel"/>
        /// </param>
        public CloseCommand(ViewModel.User.SettingViewModel settingViewModel)
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(CloseCommand)}");
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(CloseCommand)}");

            // field changed and changes has not been saved
            if (settingViewModel.DoesFieldChanged())
            {
                if (Services.WindowManager.Instance.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.User.Setting.Close.UNSAVED_CHANGES_MESSAGE,
                    Core.Messages.Info.MessageBoxHeader.WARNING,
                    Window.Enums.MessageBoxButton.YesNo) == false)
                {
                    // user press 'No'. 
                    // interrupt command execution
                    return;
                }
            }            

            // close window
            settingViewModel.Logger.LogAsync(Core.LogMode.Debug, $"Close window {nameof(Window.User.Setting)}");
            Services.WindowManager.Instance.CloseModalWindow(nameof(Window.User.Setting));

            // remove temp folder and all its content, if folder exist
            if (System.IO.Directory.Exists(Core.Configuration.AppConfig.TEMP_FOLDER))
            {
                settingViewModel.Logger.LogAsync(Core.LogMode.Debug, "Remove temp folder");
                System.IO.Directory.Delete(Core.Configuration.AppConfig.TEMP_FOLDER, true);
            }
        }
    }
}
