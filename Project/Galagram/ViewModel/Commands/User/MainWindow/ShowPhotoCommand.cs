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
        /// Initializes a new instance of <see cref="ShowPhotoCommand"/>
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(ShowPhotoCommand)}");
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(ShowPhotoCommand)}");

            // gets index 
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Gets photo index");
            int index = mainWindowViewModel.SelectedPhotoIndex;
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"Photo index = {index}");

            // check index
            if (index == Core.Configuration.Constants.WRONG_INDEX)
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Operation canceled. Wrong index");
                return;
            }
            
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Open modal {nameof(Window.User.PhotoInside)} window");
            mainWindowViewModel.WindowManager.ShowWindowDialog(key: nameof(Window.User.PhotoInside), 
                                                               viewModel: new ViewModel.User.PhotoInsideViewModel(photo: mainWindowViewModel.Photos[index]));
        }
    }
}
