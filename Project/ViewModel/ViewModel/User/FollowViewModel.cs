using System.Windows.Input;
using System.ComponentModel;

namespace ViewModel.ViewModel.User
{
    public class FollowViewModel : INotifyPropertyChanged
    {
        // EVENT
        public event PropertyChangedEventHandler PropertyChanged;
        // CONSTRUCTORS
        public FollowViewModel(Galagram.Services.WindowManager windowManager, DataAccess.Context.UnitOfWork unitOfWork, DataAccess.Entities.User user)
        {
            throw new System.NotImplementedException();
        }
        // PROPERTIES
        public int SelectedFollowIndex
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
        public DataAccess.Entities.User[] Follow
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
        public ICommand DeleteFollowCommand
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
