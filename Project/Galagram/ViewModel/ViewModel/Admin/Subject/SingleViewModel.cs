using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin.Subject
{
    /// <summary>
    /// An logic class for <see cref="Window.Admin.UserControls.Subjects.Single"/>
    /// </summary>
    public class SingleViewModel : ViewModelBase
    {
        // FIELDS
        bool isNew;

        DataAccess.Entities.Subject subject;
        readonly string subjectId;
        string subjectName;

        readonly string buttonName;

        ICommand goBackCommand;
        ICommand insertUpdateCommand;

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
            this.isNew = isNew;

            if (isNew)
            {
                this.subject = null;
                this.subjectId = "###";
                this.subjectName = string.Empty;

                this.buttonName = "Create";
            }
            else
            {
                // try get entity
                this.subject = UnitOfWork.SubjectRepository.Get(id);
                if (subject == null) throw new System.Data.RowNotInTableException(string.Format(Core.Messages.Error.Admin.ROW_MISSING_FORMAT, nameof(DataAccess.Entities.Subject), id));
                
                this.subjectId = subject.Id.ToString();
                this.subjectName = subject.Name;

                this.buttonName = "Update";
            }

            // commands
            goBackCommand = new Commands.Admin.GoBackCommand();
            insertUpdateCommand = new Commands.Admin.Subject.Single.CreateUpdateCommand(this);

            Logger.LogAsync(Core.LogMode.Debug, $"{nameof(SingleViewModel)} created");
        }
        /// <summary>
        /// Intialize a new instance of <see cref="SingleViewModel"/>
        /// </summary>
        /// <param name="subject">
        /// A subject to update
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when passed subject is null
        /// </exception>
        public SingleViewModel(DataAccess.Entities.Subject subject)
        {
            if (subject == null) throw new System.ArgumentNullException(nameof(subject));

            this.isNew = false;
            this.subject = subject;
            this.subjectId = subject.Id.ToString();
            this.subjectName = subject.Name;

            this.buttonName = "Update";

            // commands
            goBackCommand = new Commands.Admin.GoBackCommand();
            insertUpdateCommand = new Commands.Admin.Subject.Single.CreateUpdateCommand(this);

            Logger.LogAsync(Core.LogMode.Debug, $"{nameof(SingleViewModel)} created");
        }

        // PROPERTIES
        /// <summary>
        /// Gets current subject
        /// </summary>
        public DataAccess.Entities.Subject Subject
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Subject)}");

                return subject;
            }
        }
        /// <summary>
        /// Gets true for insert action, false — update
        /// </summary>
        public bool InsertOrUpdateAction
        {
            get
            {
                return isNew;
            }
        }
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
        /// <summary>
        /// Gets command to insert or update value
        /// </summary>
        public ICommand InsertUpdateCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(InsertUpdateCommand)}");

                return insertUpdateCommand;
            }
        }
    }
}
