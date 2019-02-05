namespace Galagram.Services.NavigationManagerInitializers
{
    /// <summary>
    /// Base class for navigation initializers classes
    /// </summary>
    public abstract class NavigationManagerInitializerBase
    {
        /// <summary>
        /// If overriden, initialize <see cref="NavigationManager"/> with default controls
        /// </summary>
        /// <param name="navigationManager">
        /// An instance of <see cref="NavigationManager"/> in which default controls should be registered.
        /// </param>
        public abstract void Initialize(NavigationManager navigationManager);
    }
}
