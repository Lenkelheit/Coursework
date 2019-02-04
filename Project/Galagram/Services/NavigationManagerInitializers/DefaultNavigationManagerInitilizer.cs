namespace Galagram.Services.NavigationManagerInitializers
{
    /// <summary>
    /// Initializes <see cref="NavigationManager"/> with default values
    /// </summary>
    public class DefaultNavigationManagerInitilizer : NavigationManagerInitializerBase
    {
        /// <summary>
        /// Initializes <see cref="NavigationManager"/> with default controls
        /// </summary>
        /// <param name="navigationManager">
        /// An instance of <see cref="NavigationManager"/> in which default controls should be registered.
        /// </param>
        public override void Initialize(NavigationManager navigationManager)
        {
            string[] menuItems = Core.Configuration.AdminConfig.ADMIN_ITEMS;

            // registrate by menu items content
            navigationManager.Registrate(menuItems[0], typeof(Window.Admin.UserControls.Messages.All));
            navigationManager.Registrate(menuItems[1], typeof(Window.Admin.UserControls.Users.All));
            navigationManager.Registrate(menuItems[2], typeof(Window.Admin.UserControls.Comments.All));
            navigationManager.Registrate(menuItems[3], typeof(Window.Admin.UserControls.Subjects.All));

            // registrate other controls by theirs full names
            navigationManager.Registrate(typeof(Window.Admin.UserControls.Messages.Single).FullName, typeof(Window.Admin.UserControls.Messages.Single));
            navigationManager.Registrate(typeof(Window.Admin.UserControls.Users.Single).FullName, typeof(Window.Admin.UserControls.Users.Single));
            navigationManager.Registrate(typeof(Window.Admin.UserControls.Comments.Single).FullName, typeof(Window.Admin.UserControls.Comments.Single));
            navigationManager.Registrate(typeof(Window.Admin.UserControls.Subjects.Single).FullName, typeof(Window.Admin.UserControls.Subjects.Single));
            navigationManager.Registrate(typeof(Window.Admin.UserControls.Photo.Single).FullName, typeof(Window.Admin.UserControls.Photo.Single));

            // delete items window
            navigationManager.Registrate(typeof(Window.Admin.UserControls.DeleteItem).FullName, typeof(Window.Admin.UserControls.DeleteItem));
        }
    }
}
