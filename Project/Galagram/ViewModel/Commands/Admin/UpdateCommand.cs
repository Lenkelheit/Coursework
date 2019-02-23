using DataAccess.Interfaces;

namespace Galagram.ViewModel.Commands.Admin
{
    /// <summary>
    /// Updates an instance of the entity
    /// </summary>
    public class UpdateCommand : CommandBase
    {
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(UpdateCommand)}");

            return true;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">
        /// Command parameter
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(UpdateCommand)}");

            // gets entity
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Gets entity");
            IEntity entityToUpdate = (IEntity)parameter;

            // update entity
            System.Type entityType = entityToUpdate.GetType();
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Updates entity of type = {entityType.Name}");
            DataAccess.Context.UnitOfWork.Instance
                .GetRepository(entityType)
                    .Update(entityToUpdate);

            // save changes
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Save changes to database");
            DataAccess.Context.UnitOfWork.Instance.Save();

            // go back to previous content with its view model
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Go back to previous content");
            Services.NavigationManager.Instance.NavigateToPrevious(parent: Services.DataStorage.Instance.AdminWindowContentControl, doSearchForDefault: true);
        }
    }
}
