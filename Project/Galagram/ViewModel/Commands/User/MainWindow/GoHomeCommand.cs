namespace Galagram.ViewModel.Commands.User.MainWindow
{
    /// <summary>
    /// Return user to his own page
    /// </summary>
    public class GoHomeCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.MainWindowViewModel mainWindowViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="GoHomeCommand"/>
        /// </summary>
        /// <param name="mainWindowViewModel">
        /// An instance of <see cref="ViewModel.User.MainWindowViewModel"/>
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when passed command argument is null
        /// </exception>
        public GoHomeCommand(ViewModel.User.MainWindowViewModel mainWindowViewModel)
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(GoHomeCommand)} value {!mainWindowViewModel.IsCurrentUserShown}");
            return !mainWindowViewModel.IsCurrentUserShown;
        }
        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="parameter">
        /// Command parameters
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(GoHomeCommand)}");

            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Set logged user as shown user and switch window.");

            mainWindowViewModel.User = mainWindowViewModel.LoggedUser;
            mainWindowViewModel.WindowManager.SwitchMainWindow(nameof(Galagram.Window.User.MainWindow), mainWindowViewModel);
        }
    }
}
