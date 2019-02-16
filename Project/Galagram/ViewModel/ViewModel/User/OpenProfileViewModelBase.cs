using System.Windows.Input;
using System.Collections.ObjectModel;
using DA = DataAccess.Entities;

namespace Galagram.ViewModel.ViewModel.User
{
    /// <summary>
    /// Determines basic logic for showing a list of <see cref="DA.User"/> with opportunity to call <see cref="Commands.User.OpenProfileCommand"/>
    /// </summary>
    public abstract class OpenProfileViewModelBase : ViewModelBase
    {
        // FIELDS
        /// <summary>
        /// Gets or sets selected index
        /// </summary>
        protected int selectedUserIndex;
        /// <summary>
        /// Gets or sets shown user
        /// </summary>
#warning Change this to array in future
        protected ObservableCollection<DA.User> users;

        /// <summary>
        /// Gets a logic command to open user profile
        /// </summary>
        protected ICommand openProfileCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="OpenProfileViewModelBase"/>
        /// </summary>
        public OpenProfileViewModelBase()
        {
            this.selectedUserIndex = Core.Configuration.Constants.WRONG_INDEX;
            this.users = null;

            this.openProfileCommand = new Commands.User.OpenProfileCommand(this);
        }

        // PROPERTIES
        /// <summary>
        /// Gets or sets selected user index
        /// </summary>
        public int SelectedUserIndex
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(SelectedUserIndex)} with value = {selectedUserIndex}");
                return selectedUserIndex;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Sets {nameof(SelectedUserIndex)}. Old value = {selectedUserIndex}, new value = {value}");

                SetProperty(ref selectedUserIndex, value);
            }
        }
        /// <summary>
        /// Gets or sets shown users
        /// </summary>
        public ObservableCollection<DA.User> Users
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Users)}");
                return users;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug | Core.LogMode.Info, $"Sets {nameof(Users)}. Amount {value.Count}");
                users = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets a logic command to open user profile
        /// </summary>
        public ICommand OpenProfileCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(OpenProfileCommand)}");
                return openProfileCommand;
            }
        }
    }
}
