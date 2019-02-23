using System.Linq;
using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin.Comments
{
    /// <summary>
    /// A logic class for <see cref="Window.Admin.UserControls.Comments.Single"/>
    /// </summary>
    public class SingleViewModel : SingleItemViewModelBase
    {
        // FIELDS
        string[] likedUserNickname;
        string[] disLikedUserNickname;

        ICommand deleteOrUpdateCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="SingleViewModel"/>
        /// </summary>
        /// <param name="comment">
        /// An instance of <see cref="DataAccess.Entities.Comment"/>
        /// </param>
        /// <param name="isEditingEnabled">
        /// Determines if page is only for reading or not
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when <paramref name="comment"/> is null
        /// </exception>
        public SingleViewModel(DataAccess.Entities.Comment comment, bool isEditingEnabled)
            :base(shownEntity: comment, isWritingEnabled: isEditingEnabled)
        {
            ILookup<bool, string> groupedByLike = comment.Likes.ToLookup(c => c.IsLiked, u => u.User.NickName);
            this.likedUserNickname = groupedByLike[true].ToArray();
            this.disLikedUserNickname = groupedByLike[false].ToArray();

            // commands
            deleteOrUpdateCommand = isEditingEnabled ? (ICommand)new Commands.MultipleCommand(new CommandBase[]
                                                        {
                                                            new Commands.Admin.Comment.Single.ValidateCommand(this),
                                                            new Commands.Admin.UpdateCommand()
                                                        })
                                                     : (ICommand)new Commands.Admin.DeleteCommand(); 
        }

        // PROPERTIES
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
        /// Gets next operation name for current page
        /// </summary>
        public override string CrudOperationName
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(CrudOperationName)}");

                return IsWritingEnabled ? EditText : RemoveText;
            }
        }

        // COMMANDS
        /// <summary>
        /// Gets action to delete or update current <see cref="DataAccess.Entities.Comment"/>
        /// </summary>
        public override ICommand CrudOperation
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(CrudOperation)}");

                return deleteOrUpdateCommand;
            }
        }
    }
}
