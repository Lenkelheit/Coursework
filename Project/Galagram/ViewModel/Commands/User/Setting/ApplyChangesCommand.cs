using Galagram.ViewModel.Enums.User;

namespace Galagram.ViewModel.Commands.User.Setting
{
    /// <summary>
    /// Change current user
    /// </summary>
    public class ApplyChangesCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.SettingViewModel settingViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="ApplyChangesCommand"/>
        /// </summary>
        /// <param name="settingViewModel">
        /// An instance of <see cref="ViewModel.User.SettingViewModel"/>
        /// </param>
        public ApplyChangesCommand(ViewModel.User.SettingViewModel settingViewModel)
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(ApplyChangesCommand)}");
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(ApplyChangesCommand)}");
            // checking
            #region checking
            // no changes
            if (!settingViewModel.DoesFieldChanged())
            {
                settingViewModel.Logger.LogAsync(Core.LogMode.Debug, "There were no changes");
                settingViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.User.Setting.ApplyChanges.NO_CHANGES);
                return;
            }

            // check fields
            if (string.IsNullOrWhiteSpace(settingViewModel.Password))
            {
                settingViewModel.Logger.LogAsync(Core.LogMode.Debug, "Changes can not be applied. Password is emoty");
                settingViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.User.Setting.ApplyChanges.EMPTY_PASSWORD);
                return;
            }
            // is password right
            if (settingViewModel.Password != settingViewModel.DataStorage.LoggedUser.Password)
            {
                settingViewModel.Logger.LogAsync(Core.LogMode.Debug, $"Changes can not be applied. Password is wrong. User password = {settingViewModel.DataStorage.LoggedUser.Password}, written password = {settingViewModel.Password}");
                settingViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.User.Setting.ApplyChanges.PASSWORD_IS_NOT_THE_SAME);
                return;
            }
            #endregion

            // apply changes
            // avatar
            #region update avatar
            if (settingViewModel.DoesFieldChanged((int)SettingFieldChanged.Avatar))
            {
                settingViewModel.Logger.LogAsync(Core.LogMode.Debug, "Sets new avatar");
                settingViewModel.Logger.LogAsync(Core.LogMode.Info, $"Avatar = {settingViewModel.DataStorage.LoggedUser.MainPhotoPath}, temp avatar = {settingViewModel.TempAvatarPath}");

                // create folder if not exist
                if (!System.IO.Directory.Exists(Core.Configuration.AppConfig.AVATAR_FOLDER))
                {
                    settingViewModel.Logger.LogAsync(Core.LogMode.Debug, "Create avatar folder");
                    System.IO.Directory.CreateDirectory(Core.Configuration.AppConfig.AVATAR_FOLDER);
                }

                // move to constant avatar path
                settingViewModel.Logger.LogAsync(Core.LogMode.Debug, "Move avatar to constant folder");
                string tempAvatarPath = settingViewModel.TempAvatarPath;
                string constAvatarPath = string.Format(Core.Configuration.AppConfig.AVATAR_FORMAT, settingViewModel.DataStorage.LoggedUser.Id, System.IO.Path.GetExtension(tempAvatarPath));

                // move photo to that folder
                // move if not exist, overwrite if exist
                System.IO.File.Copy(tempAvatarPath, constAvatarPath, overwrite: true);

                // sets new avatar
                // sets if null, do nothing if exist
                settingViewModel.DataStorage.LoggedUser.MainPhotoPath = constAvatarPath;
            }
            #endregion

            // nickname
            #region update nickname
            if (settingViewModel.DoesFieldChanged((int)SettingFieldChanged.Nickname))
            {
                settingViewModel.Logger.LogAsync(Core.LogMode.Debug, "Sets new nickname");
                settingViewModel.Logger.LogAsync(Core.LogMode.Info, $"Old nickname = {settingViewModel.DataStorage.LoggedUser.NickName}, new nickname = {settingViewModel.NewNickname}");

                if (!settingViewModel.UnitOfWork.UserRepository.IsNicknameFree(settingViewModel.NewNickname) || !settingViewModel.IsNewNicknameValid())// do not change nickname, its occupied or wrong
                {
                    settingViewModel.Logger.LogAsync(Core.LogMode.Info, "Nickname can not be changed. Its occupied or not valid");
                    settingViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.User.Setting.ApplyChanges.NICKNAME_IS_NOT_FREE);
                }
                else // sets new nickname
                {
                    settingViewModel.DataStorage.LoggedUser.NickName = settingViewModel.NewNickname;
                }
            }
            #endregion
            // password
            #region update password
            if (settingViewModel.DoesFieldChanged((int)SettingFieldChanged.Password))
            {
                settingViewModel.Logger.LogAsync(Core.LogMode.Debug, "Sets new password");
                settingViewModel.Logger.LogAsync(Core.LogMode.Info, $"Old password = {settingViewModel.DataStorage.LoggedUser.Password}, new password = {settingViewModel.Password}");

                // change password if can, or show error message
                if (settingViewModel.IsNewPasswordValid())
                {
                    settingViewModel.DataStorage.LoggedUser.Password = settingViewModel.NewPassword;
                }
            }
            #endregion

            // update databese
            settingViewModel.Logger.LogAsync(Core.LogMode.Debug, "Update user");
            settingViewModel.UnitOfWork.UserRepository.Update(settingViewModel.DataStorage.LoggedUser);
            settingViewModel.UnitOfWork.Save();

            // reset fields on setting window
            settingViewModel.Logger.LogAsync(Core.LogMode.Debug, "Reset fields to their default values");
            settingViewModel.ResetFields();
            
            // notify user about success
            settingViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.User.Setting.ApplyChanges.CHANGES_APPLIED);
        }
    }
}
