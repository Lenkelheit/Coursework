using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Galagram.ViewModel
{
    /// <summary>
    /// An abstract class for all view model classes
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        // FIELDS        
        readonly Galagram.Services.WindowManager windowManager;
        readonly Galagram.Services.NavigationManager navigationManager;
        readonly DataAccess.Context.UnitOfWork unitOfWork;
        readonly Core.Logger logger;
        readonly Services.DataStorage dataStorage;

        // EVENTS
        /// <summary>
        /// Occurs when property changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        // CONSTRUCTORS
        /// <summary>
        /// Sets default values
        /// </summary>
        public ViewModelBase()
        {
            windowManager = Galagram.Services.WindowManager.Instance;
            navigationManager = Galagram.Services.NavigationManager.Instance;
            unitOfWork = DataAccess.Context.UnitOfWork.Instance;
            logger = Core.Logger.GetLogger;
            dataStorage = Services.DataStorage.Instance;
        }

        // PROPERTIES
        /// <summary>
        /// Gets window manager
        /// </summary>
        public Galagram.Services.WindowManager WindowManager => windowManager;
        /// <summary>
        /// Gets navigation manager
        /// </summary>
        public Galagram.Services.NavigationManager NavigationManager => navigationManager;
        /// <summary>
        /// Gets data base access object
        /// </summary>
        public DataAccess.Context.UnitOfWork UnitOfWork => unitOfWork;
        /// <summary>
        /// Gets logger
        /// </summary>
        public Core.Logger Logger => logger;
        /// <summary>
        /// Gets data storage
        /// </summary>
        public Services.DataStorage DataStorage => dataStorage;

        // METHODS
        /// <summary>
        /// Raised when property changed
        /// </summary>
        /// <param name="e">
        /// Event argument
        /// </param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raised when property changed
        /// </summary>
        /// <param name="name">
        /// Property name
        /// </param>
        protected virtual void OnPropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        /// <summary>
        /// Sets property value and raise <see cref="OnPropertyChanged(string)"/>
        /// </summary>
        /// <typeparam name="T">
        /// A property type
        /// </typeparam>
        /// <param name="storage">
        /// Original property
        /// </param>
        /// <param name="value">
        /// New value of the property
        /// </param>
        /// <param name="propertyName">
        /// Property name thet raise  <see cref="OnPropertyChanged(string)"/>
        /// </param>
        /// <returns>
        /// True if property has been changed, otherwise — false
        /// </returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, string propertyName = "")
        {
            if (System.Collections.Generic.EqualityComparer<T>.Default.Equals(storage, value))  return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
