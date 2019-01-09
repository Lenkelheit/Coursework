namespace DataAccess.Repositories
{
    /// <summary>
    /// Defines algorithms to work with Data Table with <see cref="Entities.PhotoLike"/>
    /// </summary>
    public class PhotoLikeRepository : GenericRepository<Entities.PhotoLike>
    {
        /// <summary>
        /// Initialize a new instance of <see cref="PhotoLikeRepository"/>
        /// </summary>
        /// <param name="context">Daata context</param>
        public PhotoLikeRepository(Context.AppContext context) 
            : base(context) { }
    }
}
