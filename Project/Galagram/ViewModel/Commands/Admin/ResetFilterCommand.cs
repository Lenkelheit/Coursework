namespace Galagram.ViewModel.Commands.Admin
{
    /// <summary>
    /// Resets filter, shows all items
    /// </summary>
    public class ResetFilterCommand : CommandBase
    {
        // FIELDS
        ViewModel.Admin.AllItemViewModelBase itemsModelBase;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="ResetFilterCommand"/>
        /// </summary>
        /// <param name="itemsModelBase">
        /// An instance that inherit <see cref=" ViewModel.Admin.AllItemViewModelBase "/>
        /// </param>
        public ResetFilterCommand(ViewModel.Admin.AllItemViewModelBase itemsModelBase)
        {
            this.itemsModelBase = itemsModelBase;
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Execute {nameof(ResetFilterCommand)}");

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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(ResetFilterCommand)}");

            // reset filter
            itemsModelBase.Filter = null;
        }
    }
}
