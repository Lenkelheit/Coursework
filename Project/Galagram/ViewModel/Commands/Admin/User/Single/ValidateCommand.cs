namespace Galagram.ViewModel.Commands.Admin.User.Single
{
    /// <summary>
    /// Validates an instance of <see cref="DataAccess.Entities.User"/>
    /// </summary>
    public class ValidateCommand : CommandBase
    {
        // FIELDS
        ViewModel.Admin.User.SingleViewModel userSingleViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="ValidateCommand"/>
        /// </summary>
        /// <param name="userSingleViewModel">
        /// An instance of <see cref="ViewModel.Admin.User.SingleViewModel"/>
        /// </param>
        public ValidateCommand(ViewModel.Admin.User.SingleViewModel userSingleViewModel)
        {
            this.userSingleViewModel = userSingleViewModel;
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(ValidateCommand)}");

            return true;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">
        /// Command parameter
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(ValidateCommand)}");

            // gets comment
            DataAccess.Entities.User user = (DataAccess.Entities.User)userSingleViewModel.ShownEntity;

            // get new user's nickname
            string userNickname = user.NickName;
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"New user's nickname = {userNickname}");

            // check if right
            if (userNickname.Length > Core.Configuration.DBConfig.NICKNAME_MAX_LENGTH ||
                userNickname.Length < Core.Configuration.DBConfig.NICKNAME_MIN_LENGTH)
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "User's nickname is wrong. Interrupt command executing");

                Services.WindowManager.Instance.ShowMessageWindow(Core.Messages.Info.Admin.ADMIN_WRONG_USER_NICKNAME_LENGTH);

                CommandState = Enums.Admin.CommandState.Interrupted;
                return;
            }
            else if (userSingleViewModel.RealNickname != userNickname && !userSingleViewModel.UnitOfWork.UserRepository.IsNicknameFree(userNickname)) 
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, "Nickname can not be changed. It is occupied");

                userSingleViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.User.Setting.ApplyChanges.NICKNAME_IS_NOT_FREE);

                CommandState = Enums.Admin.CommandState.Interrupted;
                return;
            }

            // get new user's password
            string userPassword = user.Password;
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"New user's password = {userPassword}");

            if (userPassword.Length > Core.Configuration.DBConfig.PASSWORD_MAX_LENGTH ||
                userPassword.Length < Core.Configuration.DBConfig.PASSWORD_MIN_LENGTH)
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "User's password is wrong. Interrupt command executing");

                Services.WindowManager.Instance.ShowMessageWindow(Core.Messages.Info.Admin.ADMIN_WRONG_USER_PASSWORD_LENGTH);

                CommandState = Enums.Admin.CommandState.Interrupted;
                return;
            }
        }
    }
}
