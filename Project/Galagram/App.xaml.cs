using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Threading;

using Core.Configuration;

namespace Galagram
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        // FIELDS
        private static Mutex mutex;

        // EXTERN METHOD
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(System.IntPtr hWnd);

        //METHODS
        /// <summary>
        /// Raises <see cref="System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">
        /// Contains the argument of <see cref="System.Windows.Application.Startup"/> event.
        /// </param>
        protected override void OnStartup(System.Windows.StartupEventArgs e)
        {
            // It is for single instance of application, when other application will be created it will redirect to first main one
            // and that other will be closed.
            bool isCreatedNewMutex;
            mutex = new Mutex(initiallyOwned: true, name: AppConfig.APP_NAME + "Mutex", createdNew: out isCreatedNewMutex);

            if (!isCreatedNewMutex)
            {
                int currentProcessId = Process.GetCurrentProcess().Id;
                Process process = Process.GetProcessesByName(AppConfig.APP_NAME).First(p => p.Id != currentProcessId);

                Core.Logger.GetLogger.Log(Core.LogMode.Info, System.Environment.NewLine + "Second application has been started up - redirected to main one");

                SetForegroundWindow(process.MainWindowHandle);

                this.Shutdown();
                return;
            }

            Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, System.Environment.NewLine + "Application has been started up");

            this.DispatcherUnhandledException += FatalClose;
            base.OnStartup(e);
        }

        private void FatalClose(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // it will not work on Debug mode
            // log all exception and inners one
            for (System.Exception exception = e.Exception; exception != null; exception = exception.InnerException)
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Fatal, $"The program has been closed with fatal unhandled error. Exception: {exception}");
            }
            e.Handled = true;

            Services.WindowManager.Instance.ShowMessageWindow(Core.Messages.Error.App.FATAL_ERROR_CONTINUE);

            if (AppConfig.DO_CLOSE_APP_ON_FATAL_ERROR)
            {
                this.Shutdown();
            }
        }
    }
}
