using System.Windows.Input;
using System.ComponentModel;

namespace ViewModel.ViewModel.User
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        // EVENT
        public event PropertyChangedEventHandler PropertyChanged;
        // CONSTRUCTORS
        public SearchViewModel(Galagram.Services.WindowManager windowManager, DataAccess.Context.UnitOfWork unitOfWork, DataAccess.Entities.User user)
        {
            throw new System.NotImplementedException();
        }
        // PROPERTIES
        public string Text
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
        public int SelectedUserIndex
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
        public DataAccess.Entities.User[] Users
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
        public ICommand SearchCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand OpenProfileCommand
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
