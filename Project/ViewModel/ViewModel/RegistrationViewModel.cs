using System.Windows.Input;
using System.ComponentModel;

namespace ViewModel.ViewModel
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        // EVENT
        public event PropertyChangedEventHandler PropertyChanged;
        // CONSTRUCTORS
        public RegistrationViewModel()
        {
            throw new System.NotImplementedException();
        }
        // PROPERTIES
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
        // COMMAND
        public ICommand LogInCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand SignUpCommand
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
