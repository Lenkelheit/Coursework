using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Galagram.ViewModel.ViewModel.User
{
    /// <summary>
    /// A logic class for <see cref="FollowViewModel"/>
    /// </summary>
    public class FollowViewModel : ViewModelBase
    {
        // FIELDS
        Enums.User.FollowMode followMode;

        int selectedFollowIndex;
        #warning set it to array after optimization in future milestones
        ObservableCollection<DataAccess.Entities.User> follow;

        ICommand deleteFollowCommand;
        ICommand openProfileCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="FollowViewModel"/>
        /// </summary>
        /// <param name="followMode">
        /// Determines in which mode open window
        /// </param>
        public FollowViewModel( Enums.User.FollowMode followMode)
        {
            this.selectedFollowIndex = Core.Configuration.Constants.WRONG_INDEX;
            this.followMode = followMode;

            if (followMode == Enums.User.FollowMode.Followers)
            {
                this.follow = new ObservableCollection<DataAccess.Entities.User>(DataStorage.ShownUser.Followers);
            }
            else if (followMode == Enums.User.FollowMode.Following)
            {
                this.follow = new ObservableCollection<DataAccess.Entities.User>(DataStorage.ShownUser.Following);
            }

            this.deleteFollowCommand = new Commands.User.Follow.DeleteFollowCommand(this);
            this.openProfileCommand = new Commands.User.Follow.OpenProfileCommand(this);
        }

        // PROPERTIES
        /// <summary>
        /// Gets mode in which window is oppened
        /// </summary>
        public Enums.User.FollowMode FollowMode => followMode;
        /// <summary>
        /// Gets follow window header
        /// </summary>
        public string WindowName => followMode.ToString();
        /// <summary>
        /// Gets or sets selected follower index
        /// </summary>
        public int SelectedFollowIndex
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(SelectedFollowIndex)} with value = {selectedFollowIndex}");
                return selectedFollowIndex;
            }
            set
            {
                selectedFollowIndex = value;
                Logger.LogAsync(Core.LogMode.Debug, $"Sets {nameof(SelectedFollowIndex)}. Old value = {selectedFollowIndex}, new value = {value}");

                OnPropertyChanged();              
            }
        }
        /// <summary>
        /// Gets or sets follow user
        /// </summary>
        public ObservableCollection<DataAccess.Entities.User> Follow
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, "Gets follow user");
                return follow;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Sets follow user. Amount {value.Count}");
                follow = value;

                OnPropertyChanged();
            }
        }

        // COMMANDS
        /// <summary>
        /// A logic command to delete follow.
        /// </summary>
        public ICommand DeleteFollowCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(DeleteFollowCommand)}");
                return deleteFollowCommand;
            }
        }
        /// <summary>
        /// A logic command to open user profile
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
