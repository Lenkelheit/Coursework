namespace Galagram.Services.WindowManagerInitializers
{
    /// <summary>
    /// Base class for window initializers classes
    /// </summary>
    public abstract class WindowManagerInitializerBase
    {
        /// <summary>
        /// If overriden, initialize <see cref="WindowManager"/> with default window
        /// </summary>
        /// <param name="windowManager">
        /// An instance of <see cref="WindowManager"/> in which default window should be registered.
        /// </param>
        public abstract void Initialize(WindowManager windowManager);
    }
}
