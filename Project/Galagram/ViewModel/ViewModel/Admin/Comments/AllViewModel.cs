using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin.Comments
{
    /// <summary>
    /// A logic class for <see cref="Window.Admin.UserControls.Comments.All"/>
    /// </summary>
    public class AllViewModel : AllItemViewModelBase
    {
        // FIELDS
        ListCollectionView comments;

        string text;
        System.DateTime? from;
        System.DateTime? to;
        string userNickname;

        ICommand openCommand;
        ICommand editCommand;
        ICommand setFilterCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="AllViewModel"/>
        /// </summary>
        public AllViewModel()
        {
            comments = new ListCollectionView(UnitOfWork.CommentRepository.Get().ToArray());

            text = null;
            from = null;
            to = null;
            userNickname = null;

            // commands
            openCommand = new Commands.RelayCommand(NavigateToOpenComment);
            editCommand = new Commands.RelayCommand(NavigateToEditComment);
            setFilterCommand = new Commands.Admin.Comments.All.SetFilterCommand(this); 
        }

        // PROPERTIES
        /// <summary>
        /// Gets filtered entities list
        /// </summary>
        public override ListCollectionView Entities
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Entities)}");

                return comments;
            }
        }
 
        #region Filter Value
        /// <summary>
        /// Gets or sets text substring to be filter on
        /// </summary>
        public string Text
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(Text)}, with value = {text}");

                return text;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Info, $"Sets {nameof(Text)}, old value = {text}, new value = {value}");

                SetProperty(ref text, value);
            }
        }
        /// <summary>
        /// From filter date
        /// </summary>
        public System.DateTime? From
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(From)} with value = {from ?? null}");

                return from;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Info, $"Sets {nameof(From)} old value = {from ?? null}, new value = {value ?? null}");

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
                Logger.LogAsync(Core.LogMode.Info, $"Sets {nameof(To)} old value = {to ?? null}, new value = {value ?? null}");

                SetProperty(ref to, value);
            }
        }
        /// <summary>
        /// Gets or sets user nickname substring to filter on
        /// </summary>
        public string UserNickname
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(UserNickname)}, with value = {userNickname}");

                return userNickname;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Info, $"Sets {nameof(UserNickname)}, old value = {userNickname}, new value = {value}");

                SetProperty(ref userNickname, value);
            }
        }
        #endregion
        // COMMANDS
        #region CRUD
        /// <summary>
        /// Navigate to full comment content
        /// </summary>
        public override ICommand OpenCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(OpenCommand)}");

                return openCommand;
            }
        }
        /// <summary>
        /// Navigate to edit comment content
        /// </summary>
        public override ICommand EditCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(EditCommand)}");

                return editCommand;
            }
        }
        #endregion
        #region FILTER
        /// <summary>
        /// Gets action to filter items on view
        /// </summary>
        public override ICommand SetFilterCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(SetFilterCommand)}");

                return setFilterCommand;
            }
        }
        #endregion
        #region Not Implemented
        /// <summary>
        /// Not implemented behaviour
        /// </summary>
        public override ICommand CreateCommand => throw new System.NotImplementedException();
        #endregion

        // NAVIGATIONS METHODS
        private void NavigateToOpenComment(object parameter)
        {
            // opens new content, single comment
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Opens {typeof(Window.Admin.UserControls.Comments.Single).FullName}");

            NavigationManager.NavigateTo(
                parent: DataStorage.AdminWindowContentControl,
                key: typeof(Window.Admin.UserControls.Comments.Single).FullName,
                viewModel: new SingleViewModel(comment: parameter as DataAccess.Entities.Comment, isReadOnly: true));
        }
        private void NavigateToEditComment(object parameter)
        {
            // opens new content, single comment
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Opens {typeof(Window.Admin.UserControls.Comments.Single).FullName}");

            NavigationManager.NavigateTo(
                parent: DataStorage.AdminWindowContentControl,
                key: typeof(Window.Admin.UserControls.Comments.Single).FullName,
                viewModel: new SingleViewModel(comment: parameter as DataAccess.Entities.Comment, isReadOnly: false));
        }
    }
}
