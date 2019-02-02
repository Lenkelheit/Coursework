namespace Galagram.ViewModel.Commands.Admin.Subject.All
{
    /// <summary>
    /// An logic class to create new subject
    /// </summary>
    public class CreateCommand : CommandBase
    {
        // METHODS
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Exetute {nameof(CreateCommand)}");

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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Exetute {nameof(CreateCommand)}");
            
            // opens new contetn, single subject

            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Opens {typeof(Window.Admin.UserControls.Subjects.Single).FullName}");
            Services.NavigationManager.Instance.NavigateTo(
                parent: Services.DataStorage.Instance.AdminWindowContentControl,
                key: typeof(Window.Admin.UserControls.Subjects.Single).FullName,
                viewModel: new ViewModel.Admin.Subject.SingleViewModel());

        }
    }
}
