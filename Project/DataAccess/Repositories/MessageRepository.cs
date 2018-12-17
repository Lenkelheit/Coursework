namespace DataAccess.Repositories
{
    public class MessageRepository : GenericRepository<Entities.Message>
    {
        public MessageRepository(Context.AppContext context) : base(context)
        {
            throw new System.NotImplementedException();
        }
    }
}
