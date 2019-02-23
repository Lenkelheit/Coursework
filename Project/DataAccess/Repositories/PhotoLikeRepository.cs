using System.Linq;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Defines algorithms to work with Data Table with <see cref="Entities.PhotoLike"/>
    /// </summary>
    public class PhotoLikeRepository : GenericRepository<Entities.PhotoLike>
    {
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="PhotoLikeRepository"/>
        /// </summary>
        /// <param name="context">Data context</param>
        public PhotoLikeRepository(Context.AppContext context) 
            : base(context) { }

        // METHODS
        /// <summary>
        /// Gets user like if it exists
        /// </summary>
        /// <param name="photo">
        /// Rated photo 
        /// </param>
        /// <param name="user">
        /// User that probably sets the like
        /// </param>
        /// <returns>
        /// An instance of <see cref="Entities.PhotoLike"/> if user rated photo, otherwise — null
        /// </returns>
        public Entities.PhotoLike TryGetUserLike(Entities.Photo photo, Entities.User user)
        {
            return context.PhotoLike.FirstOrDefault(l => l.Photo.Id == photo.Id && l.User.Id == user.Id);
        }
        /// <summary>
        /// Determines if user liked current photo
        /// </summary>
        /// <param name="photo">
        /// Rated photo 
        /// </param>
        /// <param name="user">
        /// User that probably sets the like
        /// </param>
        /// <returns>
        /// True if user liked photo, false — if disliked, null — if user does not set like
        /// </returns>
        public bool? HasLiked(Entities.Photo photo, Entities.User user)
        {
            Entities.PhotoLike like = TryGetUserLike(photo, user);
            return like?.IsLiked;
        }
    }
}
