namespace Galagram.ViewModel.Commands.Admin.Subject.All
{
    /// <summary>
    /// Opens edit window for <see cref="DataAccess.Entities.Subject"/>
    /// </summary>
    public class EditCommand : CommandBase
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(EditCommand)}");

            return true;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">
        /// Command parameter
        /// <para/>
        /// True if value should be created, false if updated
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(EditCommand)}");

            // opens  edit window 
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Opens {typeof(Window.Admin.UserControls.Subjects.Single).FullName}");
            Services.NavigationManager.Instance.NavigateTo(
                parent: Services.DataStorage.Instance.AdminWindowContentControl,
                key: typeof(Window.Admin.UserControls.Subjects.Single).FullName,
                viewModel: new ViewModel.Admin.Subject.SingleViewModel(parameter as DataAccess.Entities.Subject));


        }
    }
}
