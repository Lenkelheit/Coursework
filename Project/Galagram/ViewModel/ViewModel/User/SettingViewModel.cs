using System.Linq;
using System.Windows.Input;
using System.ComponentModel;

using Galagram.ViewModel.Enums.User;

namespace Galagram.ViewModel.ViewModel.User
{
    /// <summary>
    /// A logic class for <see cref="Galagram.Window.User.Setting"/> window
    /// </summary>
    public class SettingViewModel : ViewModelBase
    {
        // FIELDS
        System.Collections.BitArray changedField;
        
        string tempAvatarPath;
        string newNickname;
        string password;
        string newPassword;

        readonly ICommand applyChangesCommand;
        readonly ICommand loadNewAvatarCommand;
        readonly ICommand closeCommand;
        readonly ICommand resetAvatarCommand;
        readonly ICommand removeAccountCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="SearchViewModel"/>
        /// </summary>
        public SettingViewModel()
        {
            this.changedField = new System.Collections.BitArray(System.Enum.GetNames(typeof(SettingFieldChanged)).Length);
            
            this.tempAvatarPath = DataStorage.LoggedUser.MainPhotoPath;
            this.newNickname = string.Empty;
            this.password = string.Empty;
            this.newPassword = string.Empty;

            this.applyChangesCommand = new Commands.User.Setting.ApplyChangesCommand(this);
            this.loadNewAvatarCommand = new Commands.User.Setting.LoadNewAvatarCommand(this);
            this.closeCommand = new Commands.User.Setting.CloseCommand(this);
            this.resetAvatarCommand = new Commands.User.Setting.ResetAvatarCommand(this);
            this.removeAccountCommand = new Commands.User.Setting.RemoveAccountCommand(this);
        }

        // PROPERTIES
        /// <summary>
        /// Gets or sets temporary avatar that is shown
        /// </summary>
        public string TempAvatarPath
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets temporary avatar path = {tempAvatarPath}");

                return tempAvatarPath;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Sets {nameof(TempAvatarPath)}. Old = {tempAvatarPath}, new = {value}");
                tempAvatarPath = value;
                changedField.Set((int)SettingFieldChanged.Avatar, true);

                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets user's new nickname
        /// </summary>
        public string NewNickname
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(NewNickname)} with value = {newNickname}");
                return newNickname;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets or sets {nameof(NewNickname)}. Old value = {newNickname}, new value = {newNickname}");
                newNickname = value;
                changedField.Set((int)SettingFieldChanged.Nickname, DataStorage.LoggedUser.NickName != newNickname && !string.IsNullOrWhiteSpace(newNickname));

                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets user's password to varify with original one
        /// </summary>
        public string Password
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Password)} with value = {password}");

                return password;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Sets {nameof(Password)}. Old value = {password}, new value = {value}");
                password = value;

                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets new user password
        /// </summary>
        public string NewPassword
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets new {nameof(NewPassword)} with value {newPassword}");

                return newPassword;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Sets new {nameof(NewPassword)}. Old value = {newPassword}, new value = {value}");
                newPassword = value;
                changedField.Set((int)SettingFieldChanged.Password, DataStorage.LoggedUser.Password != newPassword && !string.IsNullOrWhiteSpace(newPassword));

                OnPropertyChanged();
            }
        }

        // COMMANDS
        /// <summary>
        /// Gets action to applu changes to current user
        /// </summary>
        public ICommand ApplyChangesCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(ApplyChangesCommand)}");

                return applyChangesCommand;
            }
        }
        /// <summary>
        /// Gets action to load new avatar
        /// </summary>
        public ICommand LoadNewAvatarCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(LoadNewAvatarCommand)}");

                return loadNewAvatarCommand;
            }
        }
        /// <summary>
        /// Gets action to clean up all garbage after setting
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(CloseCommand)}");

                return closeCommand;
            }
        }
        /// <summary>
        /// Resets avatar to its default value
        /// </summary>
        public ICommand ResetAvatarCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(ResetAvatarCommand)}");

                return resetAvatarCommand;
            }
        }
        /// <summary>
        /// Gets action to remove user account
        /// </summary>
        public ICommand RemoveAccountCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(RemoveAccountCommand)}");

                return removeAccountCommand;
            }
        }

        // METHODS
        /// <summary>
        /// Gets value if field has been changed
        /// </summary>
        /// <param name="fielIndex">
        /// Field index
        /// </param>
        /// <returns>
        /// True if value by current index has been changed, otherwise — false
        /// </returns>
        public bool DoesFieldChanged(int fielIndex)
        {
            return changedField.Get(fielIndex);
        }
        /// <summary>
        /// Gets indicator if any field has been changed
        /// </summary>
        /// <returns>
        /// True if any field has been changed, otherwise — false
        /// </returns>
        public bool DoesFieldChanged()
        {
            return changedField.Cast<bool>().Any(v => v == true);
        }
        /// <summary>
        /// Sets all fields to empty
        /// </summary>
        public void ResetFields()
        {
            newNickname = string.Empty;
            newPassword = string.Empty;            
            password = string.Empty;

            OnPropertyChanged(nameof(NewNickname));
            OnPropertyChanged(nameof(NewPassword));
            OnPropertyChanged(nameof(Password));

            changedField.SetAll(false);
        }
        /// <summary>
        /// Creates folder with temporary file, if current folder does not exist yet
        /// <para/>
        /// And LOG message about it
        /// </summary>
        public void CreateTempFolderIfNotExist()
        {
            // create temp folder
            if (!System.IO.Directory.Exists(Core.Configuration.AppConfig.TEMP_FOLDER))
            {
                Logger.LogAsync(Core.LogMode.Debug, "Create Temp Folder");
                System.IO.Directory.CreateDirectory(Core.Configuration.AppConfig.TEMP_FOLDER);
            }
        }
        /// <summary>
        /// Generates for passed file temp file name
        /// </summary>
        /// <param name="fileExtension">
        /// File extension with . 
        /// </param>
        /// <returns>
        /// Free file name
        /// </returns>
        public string GetRandomTempFileName(string fileExtension)
        {
            System.Random random = new System.Random();
            string serverPath;

            // gets random free name
            do
            {
                int fileHashName = random.Next().GetHashCode();

                serverPath = string.Format(Core.Configuration.AppConfig.TEMP_FILE_FORMAT, fileHashName, fileExtension);
            } while (System.IO.File.Exists(serverPath));

            return serverPath;
        }
        /// <summary>
        /// Check if nickname and password pass all validations rule
        /// </summary>
        /// <returns>
        /// True if nickname and password is correct, otherwise — false
        /// </returns>
        public bool IsNewNicknameValid()
        {
            if (string.IsNullOrWhiteSpace(this.newNickname))
            {
                WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.NICKNAME_EMPTY);
                Logger.LogAsync(Core.LogMode.Debug, $"User can not change nickname, because his nickname is empty");
                return false;
            }
            if (this.newNickname.Length < Core.Configuration.DBConfig.NICKNAME_MIN_LENGTH)
            {
                WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.NICKNAME_TOO_SHORT);
                Logger.LogAsync(Core.LogMode.Debug, $"User can not change nickname, because his nickname is too short {this.newNickname.Length}");
                return false;
            }
            if (this.newNickname.Length > Core.Configuration.DBConfig.NICKNAME_MAX_LENGTH)
            {
                WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.NICKNAME_TOO_LONG);
                Logger.LogAsync(Core.LogMode.Debug, $"User can not change nickname, because his nickname is too long {this.newNickname.Length}");
                return false;
            }
            return true;
        }
        /// <summary>
        /// Check if nickname and password pass all validations rule
        /// </summary>
        /// <returns>
        /// True if nickname and password is correct, otherwise — false
        /// </returns>
        public bool IsNewPasswordValid()
        {
            if (string.IsNullOrWhiteSpace(this.newPassword))
            {
                WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.PASSWORD_EMPTY);
                Logger.LogAsync(Core.LogMode.Debug, $"User can not change password, because his password is empty");
                return false;
            }

            if (this.newPassword.Length < Core.Configuration.DBConfig.PASSWORD_MIN_LENGTH)
            {
                WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.PASSWORD_TOO_SHORT);
                Logger.LogAsync(Core.LogMode.Debug, $"User can not change password, because his password is too short {this.newPassword.Length}");
                return false;
            }
            if (this.newPassword.Length > Core.Configuration.DBConfig.PASSWORD_MAX_LENGTH)
            {
                WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.PASSWORD_TOO_LONG);
                Logger.LogAsync(Core.LogMode.Debug, $"User can not change password, because his password is too long {this.newPassword.Length}");
                return false;
            }

            return true;
        }
    }
}
