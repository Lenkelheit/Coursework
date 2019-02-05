using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin.Subject
{
    /// <summary>
    /// A logic class for <see cref="Window.Admin.UserControls.Subjects.All"/>
    /// </summary>
    public class AllViewModel : AllItemViewModelBase
    {
        // FIELDS
        ListCollectionView subjects;

        readonly ICommand editCommand;
        readonly ICommand createCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="AllViewModel"/>
        /// </summary>
        public AllViewModel() : base()
        {
            subjects = new ListCollectionView(UnitOfWork.SubjectRepository.Get().ToArray());

            createCommand = new Commands.RelayCommand(NavigateToCreate);
            editCommand = new Commands.RelayCommand(NavigateToEdit);

            Logger.LogAsync(Core.LogMode.Debug, $"Initialize {nameof(AllViewModel)}");
        }

        // PROPERTIES
        /// <summary>
        /// Gets subject
        /// </summary>
        public override ListCollectionView Entities
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Entities)} in amount of {subjects.Count}");

                return subjects;
            }
        }
        
        // COMMANDS
        /// <summary>
        /// Gets an action to edit subject
        /// </summary>
        public override ICommand EditCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(EditCommand)}");

                return editCommand;
            }
        }
        /// <summary>
        /// Returns an action to create a new subject
        /// </summary>
        public override ICommand CreateCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(CreateCommand)}");

                return createCommand;
            }
        }

        // NAVIGATION METHODS
        private void NavigateToCreate(object parameter)
        {
            // opens new content, create subject
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Opens {typeof(Window.Admin.UserControls.Subjects.Single).FullName}");
            NavigationManager.NavigateTo(
                parent: DataStorage.AdminWindowContentControl,
                key: typeof(Window.Admin.UserControls.Subjects.Single).FullName,
                viewModel: new SingleViewModel(subject: new DataAccess.Entities.Subject(), isNew: true));
        }
        private void NavigateToEdit(object parameter)
        {
            // opens edit window 
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Opens {typeof(Window.Admin.UserControls.Subjects.Single).FullName}");
            NavigationManager.NavigateTo(
                parent: DataStorage.AdminWindowContentControl,
                key: typeof(Window.Admin.UserControls.Subjects.Single).FullName,
                viewModel: new SingleViewModel(subject: parameter as DataAccess.Entities.Subject, isNew: false));
        }
        #region Not Implemented
        /// <summary>
        /// Not implemented behaviour
        /// </summary>
        public override ICommand OpenCommand => throw new System.NotImplementedException();
        /// <summary>
        /// When overridden in a derived class, sets filter predicate
        /// </summary>
        /// <param name="entity">
        /// The entities for which predicate is applied
        /// </param>
        /// <returns>
        /// Boolean values which determines if entity is allowed by predicate or not
        /// </returns>
        protected override bool FilterPredicate(object entity)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
