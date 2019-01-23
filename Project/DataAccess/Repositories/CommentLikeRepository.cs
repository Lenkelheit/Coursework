using System.Linq;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Defines algorithms to work with Data Table with <see cref="Entities.CommentLike"/>
    /// </summary>
    public class CommentLikeRepository : GenericRepository<Entities.CommentLike>
    {
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="CommentLikeRepository"/>
        /// </summary>
        /// <param name="context">DataContext</param>
        public CommentLikeRepository(Context.AppContext context) 
            : base(context) { }

        // METHODS
        /// <summary>
        /// Gets user like if it exists
        /// </summary>
        /// <param name="comment">
        /// Rated comment 
        /// </param>
        /// <param name="user">
        /// User that probably sets the like
        /// </param>
        /// <returns>
        /// An instance of <see cref="Entities.CommentLike"/> if user rated comment, otherwise — null
        /// </returns>
        public Entities.CommentLike TryGetUserLike(Entities.Comment comment, Entities.User user)
        {
            return context.CommentLike.FirstOrDefault(l => l.Comment.Id == comment.Id && l.User.Id == user.Id);
        }
    }
}
