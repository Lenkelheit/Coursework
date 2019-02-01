using System.Linq;
using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin.Subject
{
    /// <summary>
    /// A logic class for <see cref="Window.Admin.UserControls.Subjects.All"/>
    /// </summary>
    public class AllSubjectViewModel : ViewModelBase
    {
        // FIELDS
        DataAccess.Entities.Subject[] subjects;

        ICommand editCommand;
        ICommand deleteCommand;
        ICommand createCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="AllSubjectViewModel"/>
        /// </summary>
        public AllSubjectViewModel()
        {
            subjects = UnitOfWork.SubjectRepository.Get().ToArray();


            createCommand = new Commands.Admin.Subject.CreateCommand(this);

            Logger.LogAsync(Core.LogMode.Debug, $"Initialize {nameof(AllSubjectViewModel)}");
        }

        // PROPERTIES
        /// <summary>
        /// Gets subject
        /// </summary>
        public DataAccess.Entities.Subject[] Subjects
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Subject)} in amount of {subjects.Length}");

                return subjects;
            }
        }
        
        // COMMANDS
        /// <summary>
        /// Gets an action to edit subject
        /// </summary>
        public ICommand EditCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(EditCommand)}");

                return editCommand;
            }
        }
        /// <summary>
        /// Gets an action to delete subject
        /// </summary>
        public ICommand DeleteCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(DeleteCommand)}");

                return deleteCommand;
            }
        }
        /// <summary>
        /// Returns an action to create a new subject
        /// </summary>
        public ICommand CreateCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(CreateCommand)}");

                return createCommand;
            }
        }
    }
}
