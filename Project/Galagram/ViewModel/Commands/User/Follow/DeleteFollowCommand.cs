using D = DataAccess.Entities;
using Galagram.ViewModel.Enums.User;

namespace Galagram.ViewModel.Commands.User.Follow
{
    /// <summary>
    /// Deletes follow or followers
    /// </summary>
    public class DeleteFollowCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.FollowViewModel followViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="DeleteFollowCommand"/>
        /// </summary>
        /// <param name="followViewModel">
        /// An instance of <see cref="ViewModel.User.FollowViewModel"/>
        /// </param>
        public DeleteFollowCommand(ViewModel.User.FollowViewModel followViewModel)
        {
            this.followViewModel = followViewModel;
        }
        // METHODS
        /// <summary>
        /// Checks if command  can be executed
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Exetute {nameof(DeleteFollowCommand)}");

            return true;
        }
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">
        /// User to delete
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Exetute {nameof(DeleteFollowCommand)}");

            // gets user to delete
            D.User userToUnFollow = (D.User)parameter;
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"User nickname to unfollow {userToUnFollow.NickName}");

            // unfolow someone from you
            // or
            // unfollow oneself from somebody
            if (followViewModel.FollowMode == FollowMode.Followers)
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Remove user from followers");
                followViewModel.DataStorage.ShownUser.Followers.Remove(userToUnFollow);
            }
            else if (followViewModel.FollowMode == FollowMode.Following)
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Remove user from following");
                followViewModel.DataStorage.ShownUser.Following.Remove(userToUnFollow);
            }

            // update view
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Update view");
            followViewModel.Users.Remove(userToUnFollow);

            // update DB
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Save changes to DataBase");
            followViewModel.UnitOfWork.UserRepository.Update(followViewModel.DataStorage.LoggedUser);
            followViewModel.UnitOfWork.UserRepository.Update(followViewModel.DataStorage.ShownUser);
            followViewModel.UnitOfWork.Save();            
        }
    }
}
