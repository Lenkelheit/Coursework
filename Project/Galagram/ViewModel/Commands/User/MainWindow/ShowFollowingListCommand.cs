namespace Galagram.ViewModel.Commands.User.MainWindow
{
    /// <summary>
    /// Shows shownd user following
    /// </summary>
    public class ShowFollowingListCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.MainWindowViewModel mainWindowViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="ShowFollowingListCommand"/>
        /// </summary>
        /// <param name="mainWindowViewModel">
        /// An instance of <see cref="ViewModel.User.MainWindowViewModel"/>
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when passed command argument is null
        /// </exception>
        public ShowFollowingListCommand(ViewModel.User.MainWindowViewModel mainWindowViewModel)
        {
            if (mainWindowViewModel == null) throw new System.ArgumentNullException(nameof(mainWindowViewModel));

            this.mainWindowViewModel = mainWindowViewModel;
        }

        // METHODS
        /// <summary>
        /// Checks if command can be executed
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(ShowFollowingListCommand)}");
            return true;
        }
        /// <summary>
        /// Executes command
        /// </summary>
        /// <param name="parameter">
        /// Command parameters
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(ShowFollowingListCommand)}");

            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Open modal Follow window for following");
            mainWindowViewModel.WindowManager.ShowWindowDialog(key: nameof(Window.User.Follow),
                                                               viewModel: new ViewModel.User.FollowViewModel(Enums.User.FollowMode.Following));
            mainWindowViewModel.IsFollowingUpdateExplicitly();
        }
    }
}
