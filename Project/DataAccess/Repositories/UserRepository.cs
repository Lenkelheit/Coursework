namespace DataAccess.Repositories
{
    public class UserRepository : GenericRepository<Entities.User>
    {
        public UserRepository(Context.AppContext context) : base(context)
        {
            throw new System.NotImplementedException();
        }
    }
}
