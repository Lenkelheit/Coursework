namespace Galagram.ViewModel.Commands.Admin.Message.All
{
    /// <summary>
    /// Resets filter, shows all items
    /// </summary>
    public class ResetFilterCommand : CommandBase
    {
        // FIELDS
        ViewModel.Admin.Message.AllViewModel allMessgesViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="ResetFilterCommand"/>
        /// </summary>
        /// <param name="allMessgesViewModel">
        /// An instance of <see cref="ViewModel.Admin.Message.AllViewModel"/>
        /// </param>
        public ResetFilterCommand(ViewModel.Admin.Message.AllViewModel allMessgesViewModel)
        {
            this.allMessgesViewModel = allMessgesViewModel;
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
            allMessgesViewModel.Filter = null;
        }
    }
}
