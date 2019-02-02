using System;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Proxy data access and view model in non generic way
    /// </summary>
    public class NonGenericRepository : Interfaces.IGenericRepository<object>
    {
#warning NOT IMPLEMENTED

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
        public int Count()
        {
            throw new NotImplementedException();
        }

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

        public IEnumerable<object> Get(Expression<Func<object, bool>> filter = null, 
                                       Func<IQueryable<object>, IOrderedQueryable<object>> orderBy = null, 
                                       string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public object Get(object id)
        {
            throw new NotImplementedException();
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
