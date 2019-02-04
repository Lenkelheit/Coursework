using System.Linq;
using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin.Comments
{
    /// <summary>
    /// A logic class for <see cref="Window.Admin.UserControls.Comments.Single"/>
    /// </summary>
    public class SingleViewModel : ViewModelBase
    {
        // FIELDS
        bool isReadOnly;
        DataAccess.Entities.Comment comment;

        string[] likedUserNickname;
        string[] disLikedUserNickname;

        ICommand goBackCommand;
        ICommand deleteOrUpdateCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="SingleViewModel"/>
        /// </summary>
        /// <param name="comment">
        /// An instance of <see cref="DataAccess.Entities.Comment"/>
        /// </param>
        /// <param name="isReadOnly">
        /// Determines if page is only for reading or not
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when <paramref name="comment"/> is null
        /// </exception>
        public SingleViewModel(DataAccess.Entities.Comment comment, bool isReadOnly)
        {
            if (comment == null) throw new System.ArgumentNullException(nameof(comment));

            this.isReadOnly = isReadOnly;
            this.comment = comment;

            ILookup<bool, string> groupedByLike = comment.Likes.ToLookup(c => c.IsLiked, u => u.User.NickName);
            this.likedUserNickname = groupedByLike[true].ToArray();
            this.disLikedUserNickname = groupedByLike[false].ToArray();

            // commands
            goBackCommand = new Commands.Admin.GoBackCommand();
            deleteOrUpdateCommand = isReadOnly ? (ICommand) new Commands.Admin.DeleteCommand()
                                               : (ICommand) new Commands.Admin.UpdateCommand();
        }

        // PROPERTIES
        /// <summary>
        /// Gets value that determines if pages is only for reading
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(IsReadOnly)}, with value = {isReadOnly}");

                return isReadOnly;
            }
        }
        /// <summary>
        /// Gets shown instance of <see cref="DataAccess.Entities.Comment"/>
        /// </summary>
        public DataAccess.Entities.Comment Comment
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Comment)}");

                return comment;
            }
        }
        /// <summary>
        /// Gets an array of user nicknames that likes current comment
        /// </summary>
        public string[] LikedUserNickname
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets a {nameof(LikedUserNickname)} in amount of {likedUserNickname.Length}");

                return likedUserNickname;
            }
        }
        /// <summary>
        /// Gets an array of user nicknames that dislikes current comment
        /// </summary>
        public string[] DisLikedUserNickname
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets a {nameof(DisLikedUserNickname)} in amount of {disLikedUserNickname.Length}");

                return disLikedUserNickname;
            }
        }
        /// <summary>
        /// Gets next operation for current page
        /// </summary>
        public string OperationName
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(OperationName)}");

                return isReadOnly ? "Delete" : "Save changes";
            }
        }

        // COMMANDS
        /// <summary>
        /// Gets action to navigate to previous content
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
        /// Gets action to delete or update current <see cref="DataAccess.Entities.Comment"/>
        /// </summary>
        public ICommand DeleteOrUpdateCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(deleteOrUpdateCommand)}");

                return deleteOrUpdateCommand;
            }
        }
    }
}
