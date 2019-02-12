namespace Galagram.ViewModel.Commands.User.Setting
{
    /// <summary>
    /// Removes user account
    /// <para/>
    /// Checks is password field right, asks user does he really want to remove account, removes account and switches to main window
    /// </summary>
    public class RemoveAccountCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.SettingViewModel settingViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="RemoveAccountCommand"/>
        /// </summary>
        /// <param name="settingViewModel">
        /// An instance of <see cref="ViewModel.User.SettingViewModel"/>
        /// </param>
        public RemoveAccountCommand(ViewModel.User.SettingViewModel settingViewModel)
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Execute {nameof(RemoveAccountCommand)}");

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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(RemoveAccountCommand)}");

            // checks is password right
            if (string.IsNullOrWhiteSpace(settingViewModel.Password))
            {
                settingViewModel.Logger.LogAsync(Core.LogMode.Debug, $"Account can not be deleted. Password is empty.");
                settingViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.User.Setting.ApplyChanges.EMPTY_PASSWORD);

                return;
            }

            if (settingViewModel.Password != settingViewModel.DataStorage.LoggedUser.Password)
            {
                settingViewModel.Logger.LogAsync(Core.LogMode.Debug | Core.LogMode.Info, $"Account can not be deleted. Password is wrong. User password = {settingViewModel.DataStorage.LoggedUser.Password}, written password = {settingViewModel.Password}");
                settingViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.User.Setting.ApplyChanges.PASSWORD_IS_NOT_THE_SAME);

                return;
            }

            // ask user, does he realy want to delete account
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Show message dialog to ask user again");
            bool? doDeleteAccount = Services.WindowManager.Instance.ShowMessageWindow(
                text: Core.Messages.Info.ViewModel.Command.User.Setting.RemoveAccount.DO_DELETE_ACCOUNT,
                header: Core.Messages.Info.MessageBoxHeader.WARNING,
                buttonType: Window.Enums.MessageBoxButton.YesNo);            
            
            // cancel executing
            if (!doDeleteAccount.Value)
            {
                settingViewModel.Logger.LogAsync(Core.LogMode.Debug | Core.LogMode.Info, $"Cancel exetuting of {nameof(RemoveAccountCommand)}. User choice = {doDeleteAccount.Value}");

                return;
            }

            // executing
            // remove account from DataBase
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Remove user from DataBase");
            DataAccess.Context.UnitOfWork.Instance.UserRepository.Delete(Services.DataStorage.Instance.LoggedUser);
            DataAccess.Context.UnitOfWork.Instance.Save();

            // move to main window
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Switch to main window");
            new MainWindow.LogOutCommand().Execute(parameter);
        }
    }
}
