using System.Linq;
using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin.Photo
{
    /// <summary>
    /// A logic class for <see cref="Window.Admin.UserControls.Photo.Single"/>
    /// </summary>
    public class SingleViewModel : SingleItemViewModelBase
    {
        // FIELDS
        string[] likedUserName;
        string[] disLikedUserName;
        
        ICommand deleteCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="SingleViewModel"/>
        /// </summary>
        /// <param name="photo">
        /// An instance of <see cref="DataAccess.Entities.Photo"/> to show
        /// </param>
        /// <param name="isEditingEnabled">
        /// Determines if entities can be edited
        /// </param>
        public SingleViewModel(DataAccess.Entities.Photo photo, bool isEditingEnabled)
            : base(shownEntity: photo, isWritingEnabled: isEditingEnabled)
        {
            ILookup<bool, string> groupLikes = photo.Likes.ToLookup(l => l.IsLiked, p => p.User.NickName);
            likedUserName = groupLikes[true].ToArray();
            disLikedUserName = groupLikes[false].ToArray();

            // commands
            this.deleteCommand = new Commands.MultipleCommand(new CommandBase[]
            {
                new Commands.Admin.DeleteCommand(),
                new Commands.Shared.DeletePhotoFromServerCommand(photo.Path)
            });

            Logger.LogAsync(Core.LogMode.Debug, $"Initializes {nameof(SingleViewModel)}");
        }

        // PROPERTIES
        /// <summary>
        /// Gets nicknames of user that liked photo
        /// </summary>
        public string[] LikedUserName
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(LikedUserName)} in amount of {likedUserName.Length}");

                return likedUserName;
            }
        }
        /// <summary>
        /// Gets nickname of user that dislike photo
        /// </summary>
        public string[] DisLikedUserName
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(DisLikedUserName)} in amount of {disLikedUserName.Length}");

                return disLikedUserName;
            }
        }
        /// <summary>
        /// Gets crud operation name
        /// </summary>
        public override string CrudOperationName
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(CrudOperationName)}. With value = {RemoveText}");

                return RemoveText;
            }
        }

        // COMMANDS
        /// <summary>
        /// Gets action to delete current entites
        /// </summary>
        public override ICommand CrudOperation 
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(CrudOperation)}");

                return deleteCommand;
            }
        }
    }
}
