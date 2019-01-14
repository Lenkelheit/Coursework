using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel
{
    /// <summary>
    /// An logic class for <see cref="Galagram.Window.Registration"/>
    /// </summary>
    public class RegistrationViewModel : ViewModelBase
    {
        // FIELD
        string nickname;
        string password;

        readonly ICommand logInCommand;
        readonly ICommand signUpCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="RegistrationViewModel"/>
        /// </summary>
        public RegistrationViewModel() : base()
        {
            nickname = string.Empty;
            password = string.Empty;

            logInCommand = null;
            signUpCommand = null;

            logInCommand  = new Commands.Registration.LogInCommand(this);
            signUpCommand = new Commands.Registration.SignUpCommand(this);

            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Created {nameof(RegistrationViewModel)}");
        }
        // PROPERTIES
        /// <summary>
        /// Gets or sets nickname field
        /// </summary>
        public string Nickname
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Get current NickName {nickname}");
                return nickname;
            }
            set
            {
                nickname = value;
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Set new NickName {nickname}");
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets password field
        /// </summary>
        public string Password
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Get current Password {password}");
                return password;
            }
            set
            {
                password = value;
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Set new Password {password}");
                OnPropertyChanged();
            }
        }
        // COMMAND
        /// <summary>
        /// Get login action
        /// </summary>
        public ICommand LogInCommand
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Get {nameof(LogInCommand)}");
                return logInCommand;
            }
        }
        /// <summary>
        /// Gets sign up action
        /// </summary>
        public ICommand SignUpCommand
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Get {nameof(SignUpCommand)}");
                return signUpCommand;
            }
        }
        // METHODS
        /// <summary>
        /// Check if nickname and password pass all validations rule
        /// </summary>
        /// <returns>
        /// True if nickname and password is correct, otherwise — false
        /// </returns>
        public bool IsDataValid()
        {
            if (string.IsNullOrWhiteSpace(this.nickname))
            {
                WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.NICKNAME_EMPTY);
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"User can not sign up or log in, because his nickname is empty");
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.password))
            {
                WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.PASSWORD_EMPTY);
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"User can not sign up or log in, because his password is empty");
                return false;
            }
            if (this.nickname.Length < Core.Configuration.DBConfig.NICKNAME_MIN_LENGTH)
            {
                WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.NICKNAME_TOO_SHORT);
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"User can not sign up or log in, because his nickname is too short {this.nickname.Length}");
                return false;
            }
            if (this.nickname.Length > Core.Configuration.DBConfig.NICKNAME_MAX_LENGTH)
            {
                WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.NICKNAME_TOO_LONG);
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"User can not sign up or log in, because his nickname is too long {this.nickname.Length}");
                return false;
            }
            if (this.password.Length < Core.Configuration.DBConfig.PASSWORD_MIN_LENGTH)
            {
                WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.PASSWORD_TOO_SHORT);
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"User can not sign up or log in, because his password is too short {this.password.Length}");
                return false;
            }
            if (this.password.Length > Core.Configuration.DBConfig.PASSWORD_MAX_LENGTH)
            {
                WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.PASSWORD_TOO_LONG);
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"User can not sign up or log in, because his password is too long {this.password.Length}");
                return false;
            }

            return true;
        }
        
    }
}
