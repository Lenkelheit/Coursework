using System.Linq;

namespace DataAccess.Wrappers
{
    /// <summary>
    /// Wraps <see cref="Entities.Photo"/>, to use it in algorithms
    /// </summary>
    public class PhotoWrapper
    {
        // FIELDS
        Entities.Photo photo;
        int likeAmount;
        int dislikeAmount;
        int commentAmount;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="PhotoWrapper"/>
        /// </summary>
        /// <param name="photo"></param>
        public PhotoWrapper(Entities.Photo photo)
        {
            this.photo = photo;
            this.likeAmount = photo.Likes.Count(l => l.IsLiked);
            this.dislikeAmount = photo.Likes.Count - likeAmount;
            this.commentAmount = photo.Comments.Count;
        }

        // PROPERTIES
        /// <summary>
        /// Gets wrapped photo
        /// </summary>
        public Entities.Photo Photo => photo;
        /// <summary>
        /// Gets like amount
        /// </summary>
        public int LikeAmount => likeAmount;
        /// <summary>
        /// Gets dislike amount
        /// </summary>
        public int DislikeAmount => dislikeAmount;
        /// <summary>
        /// Gets comment amount
        /// </summary>
        public int CommentAmount => commentAmount;
    }
}
