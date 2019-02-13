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

        // EXTERN METHODS
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(System.IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(System.IntPtr hWnd);


        // METHODS
        /// <summary>
        /// Raises <see cref="System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">
        /// Contains the argument of <see cref="System.Windows.Application.Startup"/> event.
        /// </param>
        protected override void OnStartup(System.Windows.StartupEventArgs e)
        {
            DataAccess.Context.UnitOfWork.Instance.AppContext.Database.Log += delegate (string message)
            {
                message = message.Trim();
                if (!string.IsNullOrWhiteSpace(message))
                {
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.DataBase, message.TrimEnd());
                }
            };

            // It is for single instance of application, when other application will be created it will redirect to first main one
            // and that other will be closed.
            bool isCreatedNewMutex;
            mutex = new Mutex(initiallyOwned: true, name: AppConfig.APP_NAME + "Mutex", createdNew: out isCreatedNewMutex);

            // close another instance of the application
            if (!isCreatedNewMutex)
            {
                // Second application has been started up - redirected to main one
#pragma warning disable CS0618 // Type or member is obsolete
                Core.Logger.GetLogger.Log(Core.LogMode.Info, System.Environment.NewLine + "Second application has been started up - redirected to main one");
#pragma warning restore CS0618 // Type or member is obsolete

                // search for original program process
                int currentProcessId = Process.GetCurrentProcess().Id; // second process id
                Process process = Process.GetProcessesByName(AppConfig.APP_NAME).First(p => p.Id != currentProcessId);

                // show original program main window
                ShowWindow(process.MainWindowHandle, 9); // 9 = SW_RESTORE, If the window is minimized or maximized, the system restores it to its original size and position.
                SetForegroundWindow(process.MainWindowHandle); 

                // close second application
                this.Shutdown();
                return;
            }


            // starts the application
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, System.Environment.NewLine + "Application has been started up");

            // handle unhadled exception
            this.DispatcherUnhandledException += FatalClose;

            // call base startup
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

            // show exception message
            Services.WindowManager.Instance.ShowMessageWindow(Core.Messages.Error.App.FATAL_ERROR_CONTINUE);

            // close APP if setted
            if (AppConfig.DO_CLOSE_APP_ON_FATAL_ERROR)
            {
                this.Shutdown();
            }
        }
    }
}
