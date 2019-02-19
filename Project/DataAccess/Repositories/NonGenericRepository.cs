using System;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Proxy data access and view model in non generic way
    /// </summary>
    public class NonGenericRepository : Interfaces.IRepository<object>
    {
        // FIELDS
        internal Context.AppContext context;
        internal DbSet dbSet;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="NonGenericRepository"/>
        /// </summary>
        /// <param name="context">
        /// Data context
        /// </param>
        /// <param name="entityType">
        /// Entity type
        /// </param>
        public NonGenericRepository(Context.AppContext context, Type entityType)
        {
            this.context = context;
            this.dbSet = context.Set(entityType);
        }

        // METHODS
        /// <summary>
        /// Not implemented behaviour
        /// </summary>
        /// <returns>Count of entities</returns>
        [Obsolete(message: "Not implemented behaviour", error: true)]
        public int Count()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Counts records in data set which satisfy the condition
        /// </summary>
        /// <param name="predicate">The condition by which record should be count</param>
        /// <returns>Returns the amount of records in data set which satisfy the condition</returns>
        [Obsolete(message: "Not implemented behaviour", error: true)]
        public int Count(Func<object, bool> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes preset entity
        /// </summary>
        /// <param name="entityToDelete">Entity to delete</param>
        /// <exception cref="ArgumentNullException">
        /// Throws when passed <paramref name="entityToDelete"/> is null
        /// </exception>
        public void Delete(object entityToDelete)
        {
            if (entityToDelete == null) throw new ArgumentNullException(nameof(entityToDelete));

            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Gets data from data base
        /// </summary>
        /// <param name="filter">Filter for data</param>
        /// <param name="orderBy">The order of the received items</param>
        /// <param name="includeProperties">Included properties</param>
        /// <returns>Queried entities collection</returns>
        [Obsolete(message: "Not implemented behaviour", error: true)]
        public IQueryable<object> Get(Expression<Func<object, bool>> filter = null, 
                                       Func<IQueryable<object>, IOrderedQueryable<object>> orderBy = null, 
                                       string includeProperties = "")
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets entity by id
        /// </summary>
        /// <param name="id">Entities id</param>
        /// <returns>Finded entity</returns>
        public object Get(object id)
        {
            return dbSet.Find(id);
        }
        /// <summary>
        /// Inserts data in data base
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        public void Insert(object entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Updates entity in data base
        /// </summary>
        /// <param name="entityToUpdate">Entity to update</param>
        public void Update(object entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
