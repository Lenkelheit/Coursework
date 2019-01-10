namespace DataAccess.Repositories
{
    /// <summary>
    /// Defines algorithms to work with Data Table with <see cref="Entities.Photo"/>
    /// </summary>
    public class PhotoRepository : GenericRepository<Entities.Photo>
    {
        /// <summary>
        /// Initialize a new instance of <see cref="PhotoRepository"/>
        /// </summary>
        /// <param name="context">Data context</param>
        public PhotoRepository(Context.AppContext context) 
            : base(context) { }
    }
}
