using System.Windows.Input;

namespace ViewModel.ViewModel
{
    /// <summary>
    /// An logic class for <see cref="Galagram.Window.Registration"/>
    /// </summary>
    public class RegistrationViewModel : ViewModelBase
    {
        // FIELD
        string nickname;
        string password;

        ICommand logInCommand;
        ICommand signUpCommand;
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
        }
        // PROPERTIES
        /// <summary>
        /// Gets or sets nickname field
        /// </summary>
        public string Nickname
        {
            get
            {
                return nickname;
            }
            set
            {
                nickname = value;
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
                return password;
            }
            set
            {
                password = value;
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
                if (logInCommand == null) logInCommand = new Commands.Registration.LogInCommand(this);
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
                if (signUpCommand == null) signUpCommand = new Commands.Registration.SignUpCommand(this);
                return signUpCommand;
            }
        }
        
    }
}
