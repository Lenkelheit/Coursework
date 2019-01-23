namespace DataAccess.Repositories
{
    /// <summary>
    /// Defines algorithms to work with Data Table with <see cref="Entities.Comment"/>
    /// </summary>
    public class CommentRepository : GenericRepository<Entities.Comment>
    {
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="CommentRepository"/>
        /// </summary>
        /// <param name="context">Data context</param>
        public CommentRepository(Context.AppContext context) 
            : base(context) { }
    }
}
