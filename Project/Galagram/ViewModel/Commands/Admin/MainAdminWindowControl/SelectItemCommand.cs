namespace Galagram.ViewModel.Commands.Admin.MainAdminWindowControl
{
    /// <summary>
    /// Opens required control in contents or close window
    /// </summary>
    public class SelectItemCommand : CommandBase
    {
        // FIELDS
        ViewModel.Admin.AdminWindowViewModel adminWindowViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="ViewModel.Admin.AdminWindowViewModel"/>
        /// </summary>
        /// <param name="adminWindowViewModel">
        /// An instance of <see cref="ViewModel.Admin.AdminWindowViewModel"/>
        /// </param>
        public SelectItemCommand(ViewModel.Admin.AdminWindowViewModel adminWindowViewModel)
        {
            this.adminWindowViewModel = adminWindowViewModel;
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Exetute {nameof(SelectItemCommand)}");

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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Exetute {nameof(SelectItemCommand)}");

            // get selected index
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Gets menu item index");

            int index = adminWindowViewModel.MenuItemIndex;
            if (index != Core.Configuration.Constants.WRONG_INDEX)
            {
                if (index == adminWindowViewModel.ExitIndex) 
                {
                    // exit from admin panel
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Exit from window panel");

                    // resets deta storage
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Resets data storage");
                    Services.DataStorage.Instance.Reset();

                    // switch to registration window
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Switch to registration window");

                    Services.WindowManager.Instance.SwitchMainWindow(
                        key: nameof(Window.Registration),
                        viewModel: new ViewModel.RegistrationViewModel());
                }
                else
                {
                    // change admin window content
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Change admin window content");

                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Clear history");
                    adminWindowViewModel.NavigationManager.ClearHistory();

                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Shows new content");
                    Services.DataStorage.Instance.AdminWindowContentControl.Content = Services.NavigationManager.Instance.NavigateTo(
                        key: adminWindowViewModel.MenuItems[index], 
                        viewModel: null);
                }
            }

        }
    }
}
