using System.Linq;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Defines algorithms to work with Data Table with <see cref="Entities.Photo"/>
    /// </summary>
    public class PhotoRepository : GenericRepository<Entities.Photo>
    {
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="PhotoRepository"/>
        /// </summary>
        /// <param name="context">Data context</param>
        public PhotoRepository(Context.AppContext context) 
            : base(context) { }

        // METHODS
        /// <summary>
        /// Gets amount of likes and dislikes to current photo
        /// </summary>
        /// <param name="photo">
        /// An instance of <see cref="Entities.Photo"/> in which likes should be counted
        /// </param>
        /// <returns>
        /// An amount of likes and dislikes to current photo
        /// </returns>
        public Structs.LikeDislikeAmount GetLikeDislikeAmount(Entities.Photo photo)
        {
            int likeCount = photo.Likes.Count(l => l.IsLiked);

            return new Structs.LikeDislikeAmount
            {
                LikesAmount = likeCount,
                DisLikesAmount = photo.Likes.Count - likeCount
            };
        }
    }
}
