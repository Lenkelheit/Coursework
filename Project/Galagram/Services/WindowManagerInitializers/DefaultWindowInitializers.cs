using Galagram.Window.User;
using Galagram.Window.Dialogs;

namespace Galagram.Services.WindowManagerInitializers
{
    /// <summary>
    /// Initialize <see cref="WindowManager"/> with default value
    /// </summary>
    public class DefaultWindowInitializers : WindowManagerInitializerBase
    {
        // PROPERTIES
        /// <summary>
        /// Gets message box name
        /// </summary>
        public override string MessageBoxName => nameof(MessageBox);
        /// <summary>
        /// Gets open file dialog name
        /// </summary>
        public override string OpenFileDialogName => nameof(DropWindow);

        // METHODS
        /// <summary>
        /// Initialize <see cref="WindowManager"/> with default window
        /// </summary>
        /// <param name="windowManager">
        /// An instance of <see cref="WindowManager"/> in which default window should be registered.
        /// </param>
        public override void Initialize(WindowManager windowManager)
        {
            // registrate all windows
            // registrate main window
            windowManager.Registrate(nameof(Window.Registration), typeof(Window.Registration));
            // registrate dialogs
            windowManager.Registrate(nameof(MessageBox), typeof(MessageBox));
            windowManager.Registrate(nameof(DropWindow), typeof(DropWindow));
            // registrate user windows
            windowManager.Registrate(nameof(AskQuestion), typeof(AskQuestion));
            windowManager.Registrate(nameof(Follow), typeof(Follow));
            windowManager.Registrate(nameof(MainWindow), typeof(MainWindow));
            windowManager.Registrate(nameof(PhotoInside), typeof(PhotoInside));
            windowManager.Registrate(nameof(Search), typeof(Search));
            windowManager.Registrate(nameof(Setting), typeof(Setting));
        }
    }
}
