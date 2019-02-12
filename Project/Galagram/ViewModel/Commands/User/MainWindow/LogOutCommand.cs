namespace Galagram.ViewModel.Commands.User.MainWindow
{
    /// <summary>
    /// Log out system
    /// </summary>
    public class LogOutCommand : CommandBase
    {
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(LogOutCommand)}");

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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(LogOutCommand)}");

            // reset users on log out
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Resets shown and logged user");
            Services.DataStorage.Instance.LoggedUser = null;
            Services.DataStorage.Instance.ShownUser = null;

            // navigate to registration window
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Switch window to registration window");
            Services.WindowManager.Instance.SwitchMainWindow(key: nameof(Window.Registration), viewModel: new ViewModel.RegistrationViewModel(), doCloseAllWindow: true);
        }
    }
}
