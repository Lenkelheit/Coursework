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

            createCommand = new Commands.Admin.Subject.All.CreateCommand();
            editCommand = new Commands.Admin.Subject.All.EditCommand();

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
        #region Not Implemented
        /// <summary>
        /// Not implemented behaviour
        /// </summary>
        public override ICommand OpenCommand => throw new System.NotImplementedException();
        /// <summary>
        /// Not implemented behaviour
        /// </summary>
        public override ICommand SetFilterCommand => throw new System.NotImplementedException();        
        #endregion
    }
}
