namespace Galagram.Services.WindowManagerInitializers
{
    /// <summary>
    /// Base class for window initializers classes
    /// </summary>
    public abstract class WindowManagerInitializerBase
    {
        // PROPERTIES
        /// <summary>
        /// Gets message box name
        /// </summary>
        public abstract string MessageBoxName { get; }
        /// <summary>
        /// Gets open file dialog name
        /// </summary>
        public abstract string OpenFileDialogName { get; }

        // METHODS
        /// <summary>
        /// If overriden, initialize <see cref="WindowManager"/> with default window
        /// </summary>
        /// <param name="windowManager">
        /// An instance of <see cref="WindowManager"/> in which default window should be registered.
        /// </param>
        public abstract void Initialize(WindowManager windowManager);
    }
}
