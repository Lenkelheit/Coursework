using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin.Subject
{
    /// <summary>
    /// An logic class for <see cref="Window.Admin.UserControls.Subjects.Single"/>
    /// </summary>
    public class SingleViewModel : ViewModelBase
    {
        // FIELDS
        DataAccess.Entities.Subject subject;
        readonly bool isNew;
        readonly string subjectId;
        string subjectName;

        readonly string buttonName;

        ICommand goBackCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="SingleViewModel"/>
        /// </summary>
        public SingleViewModel()
            : this(Core.Configuration.Constants.WRONG_INDEX) { }
        /// <summary>
        /// Intialize a new instance of <see cref="SingleViewModel"/>
        /// </summary>
        /// <param name="id">
        /// An entity's, to show, id
        /// </param>
        public SingleViewModel(int id)
        {
            // new if there no index
            bool isNew = id == Core.Configuration.Constants.WRONG_INDEX;
            
            if (isNew)
            {
                this.isNew = true;
                this.subject = null;
                this.subjectId = "###";
                this.subjectName = string.Empty;

                this.buttonName = "Create";
            }
            else
            {

                this.isNew = false;

                // try get entity
                this.subject = UnitOfWork.SubjectRepository.Get(id);
                if (subject == null) throw new System.Data.RowNotInTableException(string.Format(Core.Messages.Error.Admin.ROW_MISSING_FORMAT, nameof(DataAccess.Entities.Subject), id));
                
                this.subjectId = subject.Id.ToString();
                this.subjectName = subject.Name;

                this.buttonName = "Update";
            }
        }

        // PROPERTIES
        /// <summary>
        /// Gets operation button's name
        /// </summary>
        public string ButtonName
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(ButtonName)} with value = {buttonName}");

                return buttonName;
            }
        }

        /// <summary>
        /// Gets subject's id
        /// </summary>
        public string SubjectId
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets property {nameof(SubjectId)} with value = {subjectId}");

                return subjectId;
            }
        }
        /// <summary>
        /// Gets or sets subject's id
        /// </summary>
        public string SubjectName
        {
            get
            {
                return subjectName;
            }
            set
            {
                subjectName = value;
            }
        }

        // COMMANDS
        /// <summary>
        /// Gets command to go back to previous content
        /// </summary>
        public ICommand GoBackCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(GoBackCommand)}");

                return goBackCommand;
            }
        }        
    }
}
