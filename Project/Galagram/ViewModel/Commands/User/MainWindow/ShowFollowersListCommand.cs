namespace Galagram.ViewModel.Commands.User.MainWindow
{
    /// <summary>
    /// Shows shownd user followers
    /// </summary>
    public class ShowFollowersListCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.MainWindowViewModel mainWindowViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="ShowFollowersListCommand"/>
        /// </summary>
        /// <param name="mainWindowViewModel">
        /// An instance of <see cref="ViewModel.User.MainWindowViewModel"/>
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when passed command argument is null
        /// </exception>
        public ShowFollowersListCommand(ViewModel.User.MainWindowViewModel mainWindowViewModel)
        {
            if (mainWindowViewModel == null) throw new System.ArgumentNullException(nameof(mainWindowViewModel));

            this.mainWindowViewModel = mainWindowViewModel;
        }

        // METHODS
        /// <summary>
        /// Check if command can be executed
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(ShowFollowersListCommand)}");
            return true;
        }
        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="parameter">
        /// Command parameters
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(ShowFollowersListCommand)}");

            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Open modal Follow window for followers");
            mainWindowViewModel.WindowManager.ShowWindowDialog(key: nameof(Window.User.Follow),
                                                               viewModel: new ViewModel.User.FollowViewModel(mainWindowViewModel.User, Enums.User.FollowMode.Followers));
            mainWindowViewModel.IsFollowingUpdateExplicitly();
        }
    }
}
