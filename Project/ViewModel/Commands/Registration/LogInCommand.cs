namespace ViewModel.Commands.Registration
{
    /// <summary>
    /// Check if name and password is correct
    /// <para/>
    /// If everything is correct, log in a user
    /// </summary>
    public class LogInCommand : CommandBase
    {
        // FIELDS
        ViewModel.RegistrationViewModel registrationViewModel;
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="LogInCommand"/>
        /// </summary>
        /// <param name="registrationViewModel">
        /// An instance of <see cref="ViewModel.RegistrationViewModel"/>
        /// </param>
        public LogInCommand(ViewModel.RegistrationViewModel registrationViewModel)
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
                registrationViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.LogIn.NICKNAME_TOO_SHORT);
                Core.Logger.LogAsync(Core.LogMode.Debug, $"User can not log in, because his nickname is too short {registrationViewModel.Nickname.Length}");
                return;
            }
            if (registrationViewModel.Nickname.Length > Core.Configuration.DBConfig.NICKNAME_MAX_LENGTH)
            {
                registrationViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.LogIn.NICKNAME_TOO_LONG);
                Core.Logger.LogAsync(Core.LogMode.Debug, $"User can not log in, because his nickname is too long {registrationViewModel.Nickname.Length}");
                return;
            }
            if (registrationViewModel.Password.Length < Core.Configuration.DBConfig.PASSWORD_MIN_LENGTH)
            {
                registrationViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.LogIn.PASSWORD_TOO_SHORT);
                Core.Logger.LogAsync(Core.LogMode.Debug, $"User can not log in, because his password is too short {registrationViewModel.Password.Length}");
                return;
            }
            if (registrationViewModel.Nickname.Length > Core.Configuration.DBConfig.PASSWORD_MAX_LENGTH)
            {
                registrationViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.LogIn.PASSWORD_TOO_LONG);
                Core.Logger.LogAsync(Core.LogMode.Debug, $"User can not log in, because his password is too long {registrationViewModel.Password.Length}");
                return;
            }

            // check if name and password is valid
            DataAccess.Structs.ValidNamaAndPassword validNamaAndPassword = registrationViewModel.UnitOfWork.UserRepository.IsDataValid(registrationViewModel.Nickname, registrationViewModel.Password);

            if (!validNamaAndPassword.IsNameValid)
            {
                registrationViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.LogIn.NICKNAME_IS_WRONG);
                Core.Logger.LogAsync(Core.LogMode.Debug, $"User can not log in, because his nickname is wrong");
                return;
            }
            if (!validNamaAndPassword.IsNameValid)
            {
                registrationViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.LogIn.PASSWORD_IS_WRONG);
                Core.Logger.LogAsync(Core.LogMode.Debug, $"User can not log in, because his password is wrong");
                return;
            }

            // open new window with current user
            Core.Logger.LogAsync(Core.LogMode.Debug, "User logged in. Registration window close. Main window opens.");
            registrationViewModel.WindowManager.SwitchMainWindow(
                key: nameof(Galagram.Window.User.MainWindow),
                viewModel: new ViewModel.User.MainWindowViewModel(user: registrationViewModel.UnitOfWork.UserRepository.GetByNickname(registrationViewModel.Nickname)));
        }
    }
}
