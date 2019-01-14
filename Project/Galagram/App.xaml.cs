namespace Galagram
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        /// <summary>
        /// Raises <see cref="System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">
        /// Contains the argument of <see cref="System.Windows.Application.Startup"/> event.
        /// </param>
        protected override void OnStartup(System.Windows.StartupEventArgs e)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, System.Environment.NewLine + "Application has been started up");
            base.OnStartup(e);
        }
    }
}
