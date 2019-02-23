namespace Galagram.ViewModel.Commands.Registration
{
    /// <summary>
    /// Checks if name and password is correct
    /// <para/>
    /// If everything is correct, log in a user
    /// </summary>
    public class LogInCommand : CommandBase
    {
        // FIELDS
        ViewModel.RegistrationViewModel registrationViewModel;
        
        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="LogInCommand"/>
        /// </summary>
        /// <param name="registrationViewModel">
        /// An instance of <see cref="ViewModel.RegistrationViewModel"/>
        /// </param>
        public LogInCommand(ViewModel.RegistrationViewModel registrationViewModel)
        {
            this.registrationViewModel = registrationViewModel;
        }

        // METHODS
        /// <summary>
        /// Checks if command  can be executed
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Exetute {nameof(LogInCommand)}");

            return true;
        }
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">
        /// Command parameter
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Exetute {nameof(LogInCommand)}");

            // checking
            if (!registrationViewModel.IsDataValid()) return;

            // check if name and password is valid
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Check if user nickname and password is in DataBase");
            DataAccess.Structs.ValidNameAndPasswordAndUser validNameAndPasswordAndUser = 
                registrationViewModel.UnitOfWork.UserRepository.IsDataValid(registrationViewModel.Nickname, registrationViewModel.Password);
            
            // name is not valid
            if (!validNameAndPasswordAndUser.ValidNameAndPassword.IsNameValid)
            {
                // shows error message, cancel command executing
                registrationViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.NICKNAME_IS_WRONG);
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"User can not log in, because his nickname is wrong");
                return;
            }
            // password is not valid
            if (!validNameAndPasswordAndUser.ValidNameAndPassword.IsPasswordValid)
            {
                // shows error message, cancel command executing
                registrationViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.PASSWORD_IS_WRONG);
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"User can not log in, because his password is wrong");
                return;
            }
            
            // sets current user as shown and as logged one
            registrationViewModel.DataStorage.LoggedUser = validNameAndPasswordAndUser.User;
            registrationViewModel.DataStorage.ShownUser = validNameAndPasswordAndUser.User;


            // check if want to log in as admin
            bool doLogInAsAdmin = false;
            if (validNameAndPasswordAndUser.User.IsAdmin)
            {
                doLogInAsAdmin = Services.WindowManager.Instance.ShowMessageWindow(
                    text: Core.Messages.Info.ViewModel.IS_NEED_LOG_IN_AS_ADMIN, 
                    header: string.Empty, 
                    buttonType: Window.Enums.MessageBoxButton.YesNo).Value;
            }

            // log in
            if (doLogInAsAdmin)
            {
                // opens admin window
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "User logged in. Registration window close. Admin window opens.");
                registrationViewModel.WindowManager.SwitchMainWindow(
                    key: nameof(Window.Admin.AdminWindow),
                    viewModel: new ViewModel.Admin.AdminWindowViewModel());
            }
            else
            {
                // opens new window with current user
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "User logged in. Registration window close. Main window opens.");
                registrationViewModel.WindowManager.SwitchMainWindow(
                    key: nameof(Window.User.MainWindow),
                    viewModel: new ViewModel.User.MainWindowViewModel());
            }            
        }
    }
}
