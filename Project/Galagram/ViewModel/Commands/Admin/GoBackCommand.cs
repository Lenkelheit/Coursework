namespace Galagram.ViewModel.Commands.Admin
{
    /// <summary>
    /// Go back to previous navigation content
    /// </summary>
    public class GoBackCommand : CommandBase
    {
        /// <summary>
        /// Check if command  can be executed
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Exetute {nameof(GoBackCommand)}");

            return true;
        }
        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">
        /// Command parameter
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Exetute {nameof(GoBackCommand)}");

            // go back to previous content with its view model
            Services.NavigationManager.Instance.NavigateToPrevious(Services.DataStorage.Instance.AdminWindowContentControl);
        }
    }
}
