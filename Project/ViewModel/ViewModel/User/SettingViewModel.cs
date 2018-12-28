using System.Windows.Input;
using System.ComponentModel;

namespace ViewModel.ViewModel.User
{
    public class SettingViewModel : INotifyPropertyChanged
    {
        // EVENT
        public event PropertyChangedEventHandler PropertyChanged;
        // CONSTRUCTORS
        public SettingViewModel(DataAccess.Entities.User user)
        {
            throw new System.NotImplementedException();
        }
        // PROPERTIES
        public DataAccess.Entities.User User
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        public string TempAvatarPath
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        public string Nickname
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        public string Password
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        public string NewPassword
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        // COMMANDS
        public ICommand ApplyChangesCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand LoadNewAvatarCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        // METHODS
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
