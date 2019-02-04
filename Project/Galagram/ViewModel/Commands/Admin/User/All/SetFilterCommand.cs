namespace Galagram.ViewModel.Commands.Admin.User.All
{
    /// <summary>
    /// Sets filter for <see cref="DataAccess.Entities.User"/>
    /// </summary>
    public class SetFilterCommand : CommandBase
    {
        // FIELDS
        ViewModel.Admin.User.AllViewModel allUserViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="SetFilterCommand"/>
        /// </summary>
        /// <param name="allUserViewModel">
        /// An instance of <see cref="ViewModel.Admin.User"/>
        /// </param>
        public SetFilterCommand(ViewModel.Admin.User.AllViewModel allUserViewModel)
        {
            this.allUserViewModel = allUserViewModel;
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Execute {nameof(SetFilterCommand)}");

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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(SetFilterCommand)}");

            // sets filter
            allUserViewModel.Filter = UserFilter;
        }

        private bool UserFilter(object entity)
        {
            // gets user
            DataAccess.Entities.User user = (DataAccess.Entities.User)entity;

            // filtering
            return DataAccess.Filters.UserFilter.Has(user, allUserViewModel.NicknameSubstring);
        }
    }
}
