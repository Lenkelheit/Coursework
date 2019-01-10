namespace DataAccess.Repositories
{
    /// <summary>
    /// Defines algorithms to work with Data Table with <see cref="Entities.CommentLike"/>
    /// </summary>
    public class CommentLikeRepository : GenericRepository<CommentLikeRepository>
    {
        /// <summary>
        /// Initialize a new instance of <see cref="CommentLikeRepository"/>
        /// </summary>
        /// <param name="context">DataContext</param>
        public CommentLikeRepository(Context.AppContext context) 
            : base(context) { }
    }
}
