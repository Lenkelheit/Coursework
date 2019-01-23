using System;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Proxy data acsess and view model
    /// </summary>
    /// <typeparam name="TEntity">
    /// Data class work with
    /// </typeparam>
    public class GenericRepository<TEntity> : Interfaces.IGenericRepository<TEntity> where TEntity : class
    {
        // FIELDS
        internal Context.AppContext context;
        internal DbSet<TEntity> dbSet;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="GenericRepository{TEntity}"/>
        /// </summary>
        /// <param name="context">Data context</param>
        public GenericRepository(Context.AppContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }


        // METHODS

        /// <summary>
        /// Counts records in data set
        /// </summary>
        /// <returns>Count of entities</returns>
        public virtual int Count()
        {
            return dbSet.AsNoTracking().Count();
        }
        /// <summary>
        /// Counts records in data set which satisfy the condition
        /// </summary>
        /// <param name="predicate">The condition by which record should be count</param>
        /// <returns>Returns the amount of records in data set which satisfy the condition</returns> /
        /// // <exception cref="ArgumentNullException">
        /// Throws when passed <paramref name="predicate"/> is null
        /// </exception>
        public virtual int Count(Func<TEntity, bool> predicate)
        {
            return dbSet.Count(predicate);
        }
        /// <summary>
        /// Gets data from data base
        /// </summary>
        /// <param name="filter">Filter for data</param>
        /// <param name="orderBy">The order of the received items</param>
        /// <param name="includeProperties">Included properties</param>
        /// <returns>Queried entities collection</returns>
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, 
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
                                                string includeProperties = "")
        {
            // filter
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            // include property
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            // ordering
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        /// <summary>
        /// Gets entity by id
        /// </summary>
        /// <param name="id">Entities id</param>
        /// <returns>Finded entity</returns>
        public virtual TEntity Get(object id)
        {
            return dbSet.Find(id);
        }
        /// <summary>
        /// Inserts data in data base
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
        /// <summary>
        /// Deletes object by id
        /// </summary>
        /// <param name="id">Objects id</param>
        /// <exception cref="ArgumentNullException">
        /// Throws when passed <paramref name="id"/> is null
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Throws when there is no records with such id
        /// </exception>
        public virtual void Delete(object id)
        {
            // find
            if (id == null) throw new ArgumentNullException(nameof(id));
            TEntity entityToDelete = dbSet.Find(id);

            // delete finded
            if (entityToDelete == null) throw new InvalidOperationException(Core.Messages.Error.Repositories.THERE_IS_NO_RECORD_WITH_SUCH_ID);
            Delete(entityToDelete);
        }
        /// <summary>
        /// Deletes preset entity
        /// </summary>
        /// <param name="entityToDelete">Entity to delete</param>
        /// <exception cref="ArgumentNullException">
        /// Throws when passed <paramref name="entityToDelete"/> is null
        /// </exception>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (entityToDelete == null) throw new ArgumentNullException(nameof(entityToDelete));

            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        /// <summary>
        /// Updates data base
        /// </summary>
        /// <param name="entityToUpdate">Entity to update</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
