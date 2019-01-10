using System.Windows.Input;
using System.ComponentModel;

namespace ViewModel.ViewModel.User
{
    public class MainWindowViewModel : ViewModelBase
    {
        // CONSTRUCTORS
        public MainWindowViewModel(DataAccess.Entities.User user)
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
        public int SelectedPhotoIndex
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
        public DataAccess.Entities.Photo[] Photos
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
        public ICommand GoHomeCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand SearchUserCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand UploadPhotoCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand AskQuestionCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand SettingCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand LogOutCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand ShowFollowersListCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand ShowFollowingListCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand ShowPhotoCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand FollowCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
