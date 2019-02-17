using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Galagram.ViewModel.ViewModel.User
{
    /// <summary>
    /// A logic class for <see cref="FollowViewModel"/>
    /// </summary>
    public class FollowViewModel : OpenProfileViewModelBase
    {
        // FIELDS
        Enums.User.FollowMode followMode;
        
        ICommand deleteFollowCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="FollowViewModel"/>
        /// </summary>
        /// <param name="followMode">
        /// Determines in which mode open window
        /// </param>
        public FollowViewModel(Enums.User.FollowMode followMode) : base()
        {
            this.followMode = followMode;

            if (followMode == Enums.User.FollowMode.Followers)
            {
                this.users = new ObservableCollection<DataAccess.Entities.User>(DataStorage.ShownUser.Followers);
            }
            else if (followMode == Enums.User.FollowMode.Following)
            {
                this.users = new ObservableCollection<DataAccess.Entities.User>(DataStorage.ShownUser.Following);
            }

            this.deleteFollowCommand = new Commands.User.Follow.DeleteFollowCommand(this);
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
    }
}
