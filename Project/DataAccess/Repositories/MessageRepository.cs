namespace DataAccess.Repositories
{
    /// <summary>
    /// Defines algorithms to work with Data Table with <see cref="Entities.Message"/>
    /// </summary>
    public class MessageRepository : GenericRepository<Entities.Message>
    {
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="MessageRepository"/>
        /// </summary>
        /// <param name="context">Data context</param>
        public MessageRepository(Context.AppContext context)
            : base(context) { }
    }
}
