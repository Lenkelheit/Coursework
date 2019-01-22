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

        ICommand applyChangesCommand;
        ICommand loadNewAvatarCommand;
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
                Logger.LogAsync(Core.LogMode.Debug, $"Sets new temp avatar path. Old = {tempAvatarPath}, new = {value}");
                tempAvatarPath = value;
                changedField.Set((int)SettingFieldChanged.Avatar, DataStorage.LoggedUser.MainPhotoPath != tempAvatarPath);

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
                changedField.Set((int)SettingFieldChanged.Nickname, DataStorage.LoggedUser.NickName != newNickname);

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
                changedField.Set((int)SettingFieldChanged.Password, DataStorage.LoggedUser.Password != newPassword);

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
        // METHODS
        /// <summary>
        /// Gets value if filled has been changed
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
        /// Sets all field to empty
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
    }
}
