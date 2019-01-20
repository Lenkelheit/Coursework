namespace Galagram.ViewModel.Commands.User.Follow
{
    /// <summary>
    /// Delete follow or followers
    /// </summary>
    public class DeleteFollowCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.FollowViewModel followViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="DeleteFollowCommand"/>
        /// </summary>
        /// <param name="followViewModel">
        /// An instance of <see cref="ViewModel.User.FollowViewModel"/>
        /// </param>
        public DeleteFollowCommand(ViewModel.User.FollowViewModel followViewModel)
        {
            this.followViewModel = followViewModel;
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Exetute {nameof(DeleteFollowCommand)}");

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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Exetute {nameof(DeleteFollowCommand)}");
            throw new System.NotImplementedException();
        }
    }
}
