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
        /// <summary>
        /// Deletes preset photo.
        /// </summary>
        /// <param name="entityToDelete">Photo to delete.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when passed <paramref name="entityToDelete"/> is null.
        /// </exception>
        public override void Delete(Entities.Photo entityToDelete)
        {
            if (entityToDelete == null) throw new System.ArgumentNullException(nameof(entityToDelete));

            dbSet.Attach(entityToDelete);

            // Photo's photolikes
            entityToDelete.Likes.ToList().ForEach(l => l.Photo = null);

            // Photo's comments
            entityToDelete.Comments.ToList().ForEach(c => 
            {
                // Photo's comment's commentlikes
                c.Likes.ToList().ForEach(cl => cl.Comment = null);

                // Photo's comment
                c.Photo = null;
            });

            context.Entry(entityToDelete).State = System.Data.Entity.EntityState.Modified;

            base.Delete(entityToDelete);
        }
    }
}
