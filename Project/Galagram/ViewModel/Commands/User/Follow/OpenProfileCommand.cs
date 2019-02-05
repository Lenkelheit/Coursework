namespace Galagram.ViewModel.Commands.User.Follow
{
    /// <summary>
    /// Opens selected user profile
    /// </summary>
    public class OpenProfileCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.FollowViewModel followViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="OpenProfileCommand"/>
        /// </summary>
        /// <param name="followViewModel">
        /// An instance of <see cref="ViewModel.User.FollowViewModel"/>
        /// </param>
        public OpenProfileCommand(ViewModel.User.FollowViewModel followViewModel)
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Exetute {nameof(OpenProfileCommand)}");

            return true;
        }
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">
        /// Command parameter
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Exetute {nameof(OpenProfileCommand)}");

            // gets selected index
            int index = followViewModel.SelectedFollowIndex;
            if (index == Core.Configuration.Constants.WRONG_INDEX)
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(OpenProfileCommand)} suspended. Wrong index value {index}");
                return;
            }

            // gets user id
            System.Guid userId = followViewModel.Follow[index].Id;
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"User id {userId}");

            // sets new shown user
            followViewModel.DataStorage.ShownUser = DataAccess.Context.UnitOfWork.Instance.UserRepository.Get(userId);

            // opens new window with current user
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Close all window. Open new Window with current profile");
            Services.WindowManager.Instance.SwitchMainWindow(
                key: nameof(Window.User.MainWindow),
                viewModel: new ViewModel.User.MainWindowViewModel(),
                doCloseAllWindow: true);
        }
    }
}
