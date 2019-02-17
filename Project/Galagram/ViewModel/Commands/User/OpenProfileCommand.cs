namespace Galagram.ViewModel.Commands.User
{
    /// <summary>
    /// Opens new profile by current id
    /// </summary>
    public class OpenProfileCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.OpenProfileViewModelBase openProfileViewModelBase;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="OpenProfileCommand"/>
        /// </summary>
        /// <param name="openProfileViewModelBase">
        /// An instance of <see cref="ViewModel.User.OpenProfileViewModelBase"/>
        /// </param>
        public OpenProfileCommand(ViewModel.User.OpenProfileViewModelBase openProfileViewModelBase)
        {
            this.openProfileViewModelBase = openProfileViewModelBase;
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(OpenProfileCommand)}");
            return true;
        }
        /// <summary>
        /// Executes command
        /// </summary>
        /// <param name="parameter">
        /// Command parameters
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(OpenProfileCommand)}");

            // gets index
            int index = openProfileViewModelBase.SelectedUserIndex;
            if (index == Core.Configuration.Constants.WRONG_INDEX)
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(OpenProfileCommand)} suspended. Wrong index value {index}");
                return;
            }

            // gets user
            DataAccess.Entities.User selectedUser = openProfileViewModelBase.Users[index];
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"User id {selectedUser.Id}");

            // sets new shown user
            openProfileViewModelBase.DataStorage.ShownUser = selectedUser;

            // open new window with current user
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Close all window. Open new Window with current profile");
            Services.WindowManager.Instance.SwitchMainWindow(
                key: nameof(Window.User.MainWindow),
                viewModel: new ViewModel.User.MainWindowViewModel(),
                doCloseAllWindow: true);
        }
    }
}
