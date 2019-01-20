using System.Linq;
using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.User
{
    /// <summary>
    /// A logic class for <see cref="FollowViewModel"/>
    /// </summary>
    public class FollowViewModel : ViewModelBase
    {
        // FIELDS
        Enums.User.FollowMode followMode;

        DataAccess.Entities.User shownUser;
        int selectedFollowIndex;
        DataAccess.Entities.User[] follow;

        ICommand deleteFollowCommand;
        ICommand openProfileCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="FollowViewModel"/>
        /// </summary>
        /// <param name="shownUser">
        /// Shown user
        /// </param>
        /// <param name="followMode">
        /// Determines in which mode open window
        /// </param>
        public FollowViewModel(DataAccess.Entities.User shownUser, Enums.User.FollowMode followMode)
        {
            this.shownUser = shownUser;
            this.selectedFollowIndex = Core.Configuration.Constants.WRONG_INDEX;
            this.followMode = followMode;

            if (followMode == Enums.User.FollowMode.Followers)
            {
                this.follow = shownUser.Followers.ToArray();
            }
            else if (followMode == Enums.User.FollowMode.Following)
            {
                this.follow = shownUser.Following.ToArray();
            }

            this.deleteFollowCommand = new Commands.User.Follow.DeleteFollowCommand(this);
            this.openProfileCommand = new Commands.User.Follow.OpenProfileCommand(this);
        }

        // PROPERTIES
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
        public DataAccess.Entities.User[] Follow
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, "Gets follow user");
                return follow;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Sets follow user. Amount {value.Length}");
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
