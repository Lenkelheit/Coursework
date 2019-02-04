using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

using static DataAccess.Filters.MessageFilter;

namespace Galagram.ViewModel.ViewModel.Admin.Message
{
    /// <summary>
    /// A logic class for <see cref="Window.Admin.UserControls.Messages.All"/>
    /// </summary>
    public class AllViewModel : AllItemViewModelBase
    {
        // FIELDS
        ListCollectionView messages;

        string[] subjects;
        int subjectIndex;

        System.DateTime? from;
        System.DateTime? to;

        ICommand openCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="AllViewModel"/>
        /// </summary>
        public AllViewModel() : base()
        {
            messages = new ListCollectionView(UnitOfWork.MessageRepository.Get().ToArray());

            subjects = UnitOfWork.SubjectRepository.Get().Select(s => s.Name).ToArray();
            subjectIndex = Core.Configuration.Constants.WRONG_INDEX;

            from = null;
            to = null;

            // commands
            openCommand = new Commands.RelayCommand(NavigateToOpenMessage);
        }

        // PROPERTIES
        /// <summary>
        /// Gets filtered messeges list
        /// </summary>
        public override ListCollectionView Entities
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(Entities)} in amount of {messages.Count}");

                return messages;
            }
        }

        #region Subject
        /// <summary>
        /// Gets subject
        /// </summary>
        public string[] Subjects
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(Subjects)} in amount of {subjects.Length}");

                return subjects;
            }
        }
        /// <summary>
        /// Gets or sets subject index
        /// </summary>
        public int SubjectIndex
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(SubjectIndex)} with value = {subjectIndex}");

                return subjectIndex;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Info, $"Sets {nameof(SubjectIndex)} old value = {subjectIndex}, new value = {value}");

                SetProperty(ref subjectIndex, value);
            }
        }
        #endregion

        #region Date
        /// <summary>
        /// From filter date
        /// </summary>
        public System.DateTime? From
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(From)} with value = {from??null}");

                return from;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(From)} old value = {from ?? null}, new value = {value ?? null}");

                SetProperty(ref from, value);
            }
        }
        /// <summary>
        /// To filter date
        /// </summary>
        public System.DateTime? To
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(To)} with value = {to ?? null}");

                return to;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(From)} old value = {from ?? null}, new value = {value ?? null}");

                SetProperty(ref to, value);
            }
        }
        #endregion

        // COMMAND
        /// <summary>
        /// Not implemented behaviour
        /// </summary>
        public override ICommand CreateCommand => throw new System.NotImplementedException();
        /// <summary>
        /// Not implemented behaviour
        /// </summary>
        public override ICommand EditCommand => throw new System.NotImplementedException();
        /// <summary>
        /// Navigate to full message content
        /// </summary>
        public override ICommand OpenCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(OpenCommand)}");

                return openCommand;
            }
        }

        // METHODS
        /// <summary>
        /// Sets filter predicate
        /// </summary>
        /// <param name="entity">
        /// The entities for which predicate is applied
        /// </param>
        /// <returns>
        /// Boolean values which determines if entity is allowed by predicate or not
        /// </returns>
        protected override bool FilterPredicate(object entity)
        {
            DataAccess.Entities.Message messageToFilter = (DataAccess.Entities.Message)entity;
            bool isShown = true;

            // checks subject
            if (subjectIndex != Core.Configuration.Constants.WRONG_INDEX)
            {
                isShown &= Where(messageToFilter, subjects[subjectIndex]);
            }

            // checks date
            isShown &= Where(messageToFilter, from, to);

            return isShown;
        }

        // NAVIGATIONS METHODS
        private void NavigateToOpenMessage(object parameter)
        {
            // opens new content, single message
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Opens {typeof(Window.Admin.UserControls.Messages.Single).FullName}");
            NavigationManager.NavigateTo(
                parent: DataStorage.AdminWindowContentControl,
                key: typeof(Window.Admin.UserControls.Messages.Single).FullName,
                viewModel: new SingleViewModel(parameter as DataAccess.Entities.Message));
        }
    }
}
