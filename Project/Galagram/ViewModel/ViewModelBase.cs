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
        readonly DataAccess.Context.UnitOfWork unitOfWork;
        readonly Core.Logger logger;

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
            unitOfWork = DataAccess.Context.UnitOfWork.Instance;
            logger = Core.Logger.GetLogger;
        }

        // PROPERTIES
        /// <summary>
        /// Gets window manager
        /// </summary>
        public Galagram.Services.WindowManager WindowManager => windowManager;
        /// <summary>
        /// Gets data base access object
        /// </summary>
        public DataAccess.Context.UnitOfWork UnitOfWork => unitOfWork;
        /// <summary>
        /// Gets logger
        /// </summary>
        public Core.Logger Logger => logger;

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
    }
}
