using System.Linq;
using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.User
{
    /// <summary>
    /// A logic class for <see cref="Galagram.Window.User.AskQuestion"/>
    /// </summary>
    public class AskQuestionViewModel : ViewModelBase
    {
        // FIELDS
        readonly DataAccess.Entities.Subject[] subjects;
        int selectedSubjectIndex;
        string messageText;

        readonly ICommand askCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="AskQuestionViewModel"/>
        /// </summary>
        public AskQuestionViewModel()
        {
            this.subjects = UnitOfWork.SubjectRepository.Get().ToArray();
            this.selectedSubjectIndex = Core.Configuration.Constants.WRONG_INDEX;
            this.messageText = string.Empty;

            this.askCommand = new Commands.User.AskQuestion.AskCommand(this);
        }
        // PROPERTIES
        /// <summary>
        /// Gets or sets current subject index
        /// </summary>
        public int SelectedSubjectIndex
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(SelectedSubjectIndex)} {selectedSubjectIndex}");
                return selectedSubjectIndex;
            }
            set
            {
                selectedSubjectIndex = value;
                Logger.LogAsync(Core.LogMode.Debug, $"Sets {nameof(SelectedSubjectIndex)} {selectedSubjectIndex}");
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets subject
        /// </summary>
        public DataAccess.Entities.Subject[] Subjects
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Subjects)}");
                return subjects;
            }
        }
        /// <summary>
        /// Gets ot sets message text
        /// </summary>
        public string Message
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Message)} {messageText}");
                return messageText;
            }
            set
            {
                messageText = value;
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Message)} {messageText}");
                OnPropertyChanged();
            }
        }
        // COMMAND
        /// <summary>
        /// Gets ask command
        /// </summary>
        public ICommand AskCommand
        {
            get
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Get {nameof(AskCommand)}");
                return askCommand;
            }
        }
        // METHODS 
        /// <summary>
        /// Checks if subject and message pass all validations rule
        /// </summary>
        /// <returns>
        /// True if subject and message is correct, otherwise — false
        /// </returns>
        public bool IsDataValid()
        {
            // check if fields are empty
            // inform about empty field

            // selected subject
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Check if subject is selected");
            if (selectedSubjectIndex == Core.Configuration.Constants.WRONG_INDEX)
            {
                WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.User.AskQuestion.Ask.SUBJECT_IS_NOT_SELECTED);
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Message is not send, because subject is not selected, current index is {selectedSubjectIndex}");
                return false;
            }

            // Trim text
            Message = messageText.Trim();
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Message text has been trimed");

            // message text
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Check if message has any text");
            if (string.IsNullOrWhiteSpace(messageText))
            {
                WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.User.AskQuestion.Ask.EMPTY_MESSAGE);
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Message is not send, bacausse it is empty");
                return false;
            }
            if (messageText.Length < Core.Configuration.DBConfig.ADMIN_MESSAGE_MIN_LENGTH)
            {
                WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.User.AskQuestion.Ask.MESSAGE_TOO_SHORT);
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Message is not send, bacausse it is too short. Current Length {messageText.Length}");
                return false;
            }
            if (messageText.Length > Core.Configuration.DBConfig.ADMIN_MESSAGE_MAX_LENGTH)
            {
                WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.User.AskQuestion.Ask.MESSAGE_TOO_LONG);
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Message is not send, bacausse it is too long. Current Length {messageText.Length}");
                return false;
            }
            return true;
        }
        /// <summary>
        /// Sets field to their default values.
        /// </summary>
        public void ResetFields()
        {
            Message = string.Empty;
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Values are reseted");
        }
    }
}
