namespace DataAccess.Repositories
{
    public class PhotoRepository : GenericRepository<Entities.Photo>
    {
        public PhotoRepository(Context.AppContext context) : base(context)
        {
            throw new System.NotImplementedException();
        }
    }
}
