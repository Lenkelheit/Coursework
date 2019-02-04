using System.Linq;
using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin.Photo
{
    /// <summary>
    /// A logic class for <see cref="Window.Admin.UserControls.Photo.Single"/>
    /// </summary>
    public class SingleViewModel : ViewModelBase
    {
        // FIELDS
        DataAccess.Entities.Photo photo;
        bool isReadOnly;

        string[] likedUserName;
        string[] disLikedUserName;

        ICommand goBackCommand;
        ICommand deleteCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="SingleViewModel"/>
        /// </summary>
        /// <param name="photo">
        /// An instance of <see cref="DataAccess.Entities.Photo"/> to show
        /// </param>
        /// <param name="isReadOnly">
        /// Determines if entities can be edited
        /// </param>
        public SingleViewModel(DataAccess.Entities.Photo photo, bool isReadOnly)
        {
            if (photo == null) throw new System.ArgumentNullException(nameof(photo));

            this.photo = photo;
            this.isReadOnly = isReadOnly;

            ILookup<bool, string> groupLikes = photo.Likes.ToLookup(l => l.IsLiked, p => p.User.NickName);
            likedUserName = groupLikes[true].ToArray();
            disLikedUserName = groupLikes[false].ToArray();

            // commands
            this.goBackCommand = new Commands.Admin.GoBackCommand();
            this.deleteCommand = new Commands.Admin.DeleteCommand();

            Logger.LogAsync(Core.LogMode.Debug, $"Initializes {nameof(SingleViewModel)}");
        }

        // PROPERTIES
        /// <summary>
        /// Gets an instance of <see cref="DataAccess.Entities.Photo"/> to show
        /// </summary>
        public DataAccess.Entities.Photo Photo
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Photo)}");

                return photo;
            }
        }
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

        // COMMANDS
        /// <summary>
        /// Gets action to return to a previous content
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
        /// Gets action to delete current entites
        /// </summary>
        public ICommand DeleteCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(DeleteCommand)}");

                return deleteCommand;
            }
        }
    }
}
