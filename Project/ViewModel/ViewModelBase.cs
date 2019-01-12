using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModel
{
    /// <summary>
    /// An abstract class for all view model classes
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        // FIELDS        
        Galagram.Services.WindowManager windowManager;
        DataAccess.Context.UnitOfWork unitOfWork;

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
