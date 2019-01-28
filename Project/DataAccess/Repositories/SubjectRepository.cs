namespace DataAccess.Repositories
{
    /// <summary>
    /// Defines algorithms to work with Data Table with <see cref="Entities.Subject"/>
    /// </summary>
    public class SubjectRepository : GenericRepository<Entities.Subject>
    {
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="SubjectRepository"/>
        /// </summary>
        /// <param name="context">Data context</param>
        public SubjectRepository(Context.AppContext context) 
            : base(context) { }
    }
}
