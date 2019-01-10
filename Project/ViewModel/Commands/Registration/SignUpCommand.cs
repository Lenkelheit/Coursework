namespace ViewModel.Commands.Registration
{
    /// <summary>
    /// Check if name and password is correct
    /// <para/>
    /// Check if nickname is available
    /// <para/>
    /// If everything is correct, log in a user
    /// </summary>
    public class SignUpCommand : CommandBase
    {
        // FIELDS
        ViewModel.RegistrationViewModel registrationViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="SignUpCommand"/>
        /// </summary>
        /// <param name="registrationViewModel">
        /// An instance of <see cref="ViewModel.RegistrationViewModel"/>
        /// </param>
        public SignUpCommand(ViewModel.RegistrationViewModel registrationViewModel)
        {
            this.registrationViewModel = registrationViewModel;
        }

        // MEHTODS
        /// <summary>
        /// Check if command  can be executed
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public override bool CanExecute(object parameter)
        {
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
            // checking
            if (registrationViewModel.Nickname.Length < Core.Configuration.DBConfig.NICKNAME_MIN_LENGTH)
            {
                registrationViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.SignUp.NICKNAME_TOO_SHORT);
                Core.Logger.LogAsync(Core.LogMode.Debug, $"User can not sign up, because his nickname is too short {registrationViewModel.Nickname.Length}");
                return;
            }
            if (registrationViewModel.Nickname.Length > Core.Configuration.DBConfig.NICKNAME_MAX_LENGTH)
            {
                registrationViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.SignUp.NICKNAME_TOO_LONG);
                Core.Logger.LogAsync(Core.LogMode.Debug, $"User can not sign up, because his nickname is too long {registrationViewModel.Nickname.Length}");
                return;
            }
            if (registrationViewModel.Password.Length < Core.Configuration.DBConfig.PASSWORD_MIN_LENGTH)
            {
                registrationViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.SignUp.PASSWORD_TOO_SHORT);
                Core.Logger.LogAsync(Core.LogMode.Debug, $"User can not sign up, because his password is too short {registrationViewModel.Password.Length}");
                return;
            }
            if (registrationViewModel.Nickname.Length > Core.Configuration.DBConfig.PASSWORD_MAX_LENGTH)
            {
                registrationViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.SignUp.PASSWORD_TOO_LONG);
                Core.Logger.LogAsync(Core.LogMode.Debug, $"User can not sign up, because his password is too long {registrationViewModel.Password.Length}");
                return;
            }
            if (!registrationViewModel.UnitOfWork.UserRepository.IsNicknameFree(registrationViewModel.Nickname))
            {
                registrationViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.SignUp.NICKNAME_IS_NOT_AVAILABLE);
                Core.Logger.LogAsync(Core.LogMode.Debug, $"User can not sign up, because his nickname is not available");
                return;
            }

            // open new window with current user
            Core.Logger.LogAsync(Core.LogMode.Debug, "User signed up. Registration window close. Main window opens.");
            registrationViewModel.WindowManager.SwitchMainWindow(
                key: nameof(Galagram.Window.User.MainWindow),
                viewModel: new ViewModel.User.MainWindowViewModel(user: new DataAccess.Entities.User()
                {
                    NickName = registrationViewModel.Nickname,
                    Password = registrationViewModel.Password
                }));
        }
    }
}
