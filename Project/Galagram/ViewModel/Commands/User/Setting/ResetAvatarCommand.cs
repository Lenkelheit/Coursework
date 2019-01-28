using System;

namespace Galagram.ViewModel.Commands.User.Setting
{
    /// <summary>
    /// Resets avatar to its default value
    /// </summary>
    public class ResetAvatarCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.SettingViewModel settingViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="ResetAvatarCommand"/>
        /// </summary>
        /// <param name="settingViewModel">
        /// An instance of <see cref="ViewModel.User.SettingViewModel"/>
        /// </param>
        public ResetAvatarCommand(ViewModel.User.SettingViewModel settingViewModel)
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(ResetAvatarCommand)}");
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(ResetAvatarCommand)}");

            // sets temp avatar path to NULL
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Sets {nameof(settingViewModel.TempAvatarPath)} to NULL");
            settingViewModel.TempAvatarPath = null;
        }
    }
}
