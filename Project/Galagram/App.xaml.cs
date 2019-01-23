using System;
using System.Windows.Threading;

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
            this.DispatcherUnhandledException += FatalClose;
            base.OnStartup(e);
        }

        private void FatalClose(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // it will not work on Debug mode
            // log all exception and inners one
            for (Exception exception = e.Exception; exception != null; exception = exception.InnerException)
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Fatal, $"The program has been closed with fatal unhandled error. Exception: {exception}");
            }
            e.Handled = true;

            Services.WindowManager.Instance.ShowMessageWindow(Core.Messages.Error.App.FATAL_ERROR_CONTINUE);
        }
    }
}
