namespace DataAccess.Repositories
{
    public class SubjectRepository : GenericRepository<Entities.Subject>
    {
        public SubjectRepository(Context.AppContext context) : base(context)
        {
            throw new System.NotImplementedException();
        }
    }
}
