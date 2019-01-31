using System.Linq;

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

        // METHODS
        /// <summary>
        /// Deletes preset comment.
        /// </summary>
        /// <param name="entityToDelete">Comment to delete.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when passed <paramref name="entityToDelete"/> is null.
        /// </exception>
        public override void Delete(Entities.Comment entityToDelete)
        {
            if (entityToDelete == null) throw new System.ArgumentNullException(nameof(entityToDelete));

            dbSet.Attach(entityToDelete);
            entityToDelete.Likes.ToList().ForEach(l => l.Comment = null);
            context.Entry(entityToDelete).State = System.Data.Entity.EntityState.Modified;

            base.Delete(entityToDelete);
        }
    }
}
