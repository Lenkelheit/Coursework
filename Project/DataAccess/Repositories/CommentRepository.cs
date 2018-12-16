namespace DataAccess.Repositories
{
    public class CommentRepository : GenericRepository<Entities.Comment>
    {
        public CommentRepository(Context.AppContext context) : base(context)
        {
            throw new System.NotImplementedException();
        }
    }
}
