namespace Galagram.ViewModel.Commands.Admin.Subject.All
{
    /// <summary>
    /// Navigate to delete contet
    /// </summary>
    public class DeleteCommand : CommandBase
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Execute {nameof(DeleteCommand)}");

            return true;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">
        /// Command parameter
        /// <para/>
        /// Entity to delete
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(DeleteCommand)}");

            // navigate
            Services.NavigationManager.Instance.NavigateTo(
                parent: Services.DataStorage.Instance.AdminWindowContentControl,
                key: typeof(Window.Admin.UserControls.DeleteItem).FullName,
                viewModel: new ViewModel.Admin.DeleteItemViewModel(parameter as DataAccess.Interfaces.IEntity));
        }
    }
}
