using System.Linq;

namespace DataAccess.Wrappers
{
    /// <summary>
    /// Wraps <see cref="Entities.Comment"/>
    /// </summary>
    public class CommentWrapper : System.ComponentModel.INotifyPropertyChanged
    {
        // FIELDS
        readonly Entities.Comment comment;
        int likesAmount;
        int dislikeAmount;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="CommentWrapper"/>
        /// </summary>
        /// <param name="comment">
        /// An instance of <see cref="Entities.Comment"/> t
        /// </param>
        public CommentWrapper(Entities.Comment comment)
        {
            this.comment = comment;
            this.likesAmount = comment.Likes.Count(l => l.IsLiked);
            this.dislikeAmount = comment.Likes.Count - likesAmount;
        }

        // PROPERTIES
        /// <summary>
        /// Gets wrapped comment
        /// </summary>
        public Entities.Comment Comment => comment;
        /// <summary>
        /// Gets or sets likes amount
        /// </summary>
        public int LikesAmount
        {
            get
            {
                return likesAmount;
            }
            set
            {
                likesAmount = value;

                OnPropertyChanged(nameof(LikesAmount));
            }
        }
        /// <summary>
        /// Gets or sets dislikes amount
        /// </summary>
        public int DisLikesAmount
        {
            get
            {
                return dislikeAmount;
            }
            set
            {
                dislikeAmount = value;

                OnPropertyChanged(nameof(DisLikesAmount));
            }
        }

        // EVENTS
        /// <summary>
        /// Occurs when property changed
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        // METHODS
        /// <summary>
        /// Invoke <see cref="PropertyChanged"/>
        /// </summary>
        /// <param name="propertyName">
        /// Property name that has been updated
        /// </param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}
