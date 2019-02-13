namespace Galagram.ViewModel.Commands.User.MainWindow
{
    /// <summary>
    /// Opens showned user followers/following
    /// </summary>
    public class ShowFollowListCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.MainWindowViewModel mainWindowViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="ShowFollowListCommand"/>
        /// </summary>
        /// <param name="mainWindowViewModel">
        /// An instance of <see cref="ViewModel.User.MainWindowViewModel"/>
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when passed command argument is null
        /// </exception>
        public ShowFollowListCommand(ViewModel.User.MainWindowViewModel mainWindowViewModel)
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(ShowFollowListCommand)}");
            return true;
        }
        /// <summary>
        /// Executes command
        /// </summary>
        /// <param name="parameter">
        /// Command parameters
        /// <para/>
        /// <paramref name="parameter"/> is an instance of <see cref="Enums.User.FollowMode"/> that determines in which mode window should be opened
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Throws when <paramref name="parameter"/> is not <see cref="Enums.User.FollowMode"/>
        /// </exception>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(ShowFollowListCommand)}");

            // gets value that determines in which mode window should be opened
            if (!(parameter is Enums.User.FollowMode)) throw new System.ArgumentException();
            Enums.User.FollowMode follwMode = (Enums.User.FollowMode)parameter;
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"Follow mode = {follwMode}");

            // opens follow window
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Open modal Follow window");
            mainWindowViewModel.WindowManager.ShowWindowDialog(key: nameof(Window.User.Follow),
                                                               viewModel: new ViewModel.User.FollowViewModel(follwMode));

            // updates main window after closing follow window
            mainWindowViewModel.IsFollowingUpdateExplicitly();
            mainWindowViewModel.UpdateShownUser();
        }
    }
}
