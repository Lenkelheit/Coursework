namespace Galagram.ViewModel.Commands.User.MainWindow
{
    /// <summary>
    /// Opens modal window that allow ask question
    /// </summary>
    public class AskQuestionCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.MainWindowViewModel mainWindowViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="AskQuestionCommand"/>
        /// </summary>
        /// <param name="mainWindowViewModel">
        /// An instance of <see cref="ViewModel.User.MainWindowViewModel"/>
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when passed command argument is null
        /// </exception>
        public AskQuestionCommand(ViewModel.User.MainWindowViewModel mainWindowViewModel)
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(AskQuestionCommand)}");
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(AskQuestionCommand)}");

            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Open modal AskQuestion window");
            mainWindowViewModel.WindowManager.ShowWindowDialog(nameof(Window.User.AskQuestion), new ViewModel.User.AskQuestionViewModel(mainWindowViewModel.LoggedUser));
        }
    }
}
