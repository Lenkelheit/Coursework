namespace Galagram.ViewModel.Commands.User.MainWindow
{
    /// <summary>
    /// Shows current photo modal window
    /// </summary>
    public class ShowPhotoCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.MainWindowViewModel mainWindowViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="ShowPhotoCommand"/>
        /// </summary>
        /// <param name="mainWindowViewModel">
        /// An instance of <see cref="ViewModel.User.MainWindowViewModel"/>
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when passed command argument is null
        /// </exception>
        public ShowPhotoCommand(ViewModel.User.MainWindowViewModel mainWindowViewModel)
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(ShowPhotoCommand)}");
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(ShowPhotoCommand)}");

            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Open modal PhotoInside window");
            mainWindowViewModel.WindowManager.ShowWindowDialog(nameof(Window.User.PhotoInside), new ViewModel.User.PhotoInsideViewModel(mainWindowViewModel.LoggedUser));
        }
    }
}
