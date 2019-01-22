namespace Galagram.ViewModel.Commands.User.Search
{
    /// <summary>
    /// Opens new profile by current id
    /// </summary>
    public class OpenProfileCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.SearchViewModel searchViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="OpenProfileCommand"/>
        /// </summary>
        /// <param name="searchViewModel">
        /// An instance of <see cref="ViewModel.User.SearchViewModel"/>
        /// </param>
        public OpenProfileCommand(ViewModel.User.SearchViewModel searchViewModel)
        {
            this.searchViewModel = searchViewModel;
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(OpenProfileCommand)}");
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(OpenProfileCommand)}");

            // gets user id
            int userId = searchViewModel.Users[searchViewModel.SelectedUserIndex].Id;
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"User id {userId}");

            // sets shown user
            searchViewModel.DataStorage.ShownUser = DataAccess.Context.UnitOfWork.Instance.UserRepository.Get(userId);

            // open new window with current user
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Close all window. Open new Window with current profile");
            Services.WindowManager.Instance.SwitchMainWindow(
                key: nameof(Window.User.MainWindow),
                viewModel: new ViewModel.User.MainWindowViewModel(),
                doCloseAllWindow: true);
        }
    }
}
