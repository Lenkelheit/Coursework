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

            // You don't have to change entity if you want to go back.
            // The entity is detached after it has been created and it isn't yet in database.
            if (parameter != null && DataAccess.Context.UnitOfWork.Instance.AppContext.Entry(parameter).State != System.Data.Entity.EntityState.Detached)
                DataAccess.Context.UnitOfWork.Instance.AppContext.Entry(parameter).State = System.Data.Entity.EntityState.Unchanged;

            // go back to previous content with its view model
            Services.NavigationManager.Instance.NavigateToPrevious(Services.DataStorage.Instance.AdminWindowContentControl);
        }
    }
}
