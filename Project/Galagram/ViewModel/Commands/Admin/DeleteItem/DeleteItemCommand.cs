using DataAccess.Context;

namespace Galagram.ViewModel.Commands.Admin.DeleteItem
{
    /// <summary>
    /// Deletes selected entity
    /// </summary>
    public class DeleteItemCommand : CommandBase
    {
        // FIELDS
        ViewModel.Admin.DeleteItemViewModel deleteItemViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="DeleteItemCommand"/>
        /// </summary>
        /// <param name="deleteItemViewModel">
        /// An instance of <see cref="ViewModel.Admin.DeleteItemViewModel"/>
        /// </param>
        public DeleteItemCommand(ViewModel.Admin.DeleteItemViewModel deleteItemViewModel)
        {
            this.deleteItemViewModel = deleteItemViewModel;
        }

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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Execute {nameof(DeleteItemCommand)}");

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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(DeleteItemCommand)}");

            // delete from repository
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Delete item from non-generic repository");

            UnitOfWork.Instance
                .GetRepository(deleteItemViewModel.Entity.GetType())
                    .Delete(deleteItemViewModel.Entity);

            // save changes
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Save changes to DataBase");
            UnitOfWork.Instance.Save();

            // go back to previous window
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Go back to previous content");
            Services.NavigationManager.Instance.NavigateToPrevious(Services.DataStorage.Instance.AdminWindowContentControl);
        }
    }
}
