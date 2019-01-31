using System.Linq;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Defines algorithms to work with Data Table with <see cref="Entities.User"/>
    /// </summary>
    public class UserRepository : GenericRepository<Entities.User>
    {
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="UserRepository"/>
        /// </summary>
        /// <param name="context">Data context</param>
        public UserRepository(Context.AppContext context)
            : base(context) { }

        // METHODS
        /// <summary>
        /// Checks if current nickname is free
        /// </summary>
        /// <param name="nickName">
        /// Nickname that should be checked
        /// </param>
        /// <returns>
        /// True if nickname is free, otherwise false
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when passed <paramref name="nickName"/> is null
        /// </exception>
        public bool IsNicknameFree(string nickName)
        {
            if (string.IsNullOrWhiteSpace(nickName)) throw new System.ArgumentNullException(nameof(nickName));
            // return true with First occurrence
            return !dbSet.AsNoTracking().AsEnumerable().Any(user => user.NickName == nickName);
        }
        /// <summary>
        /// Checks if current nickname and password are valid
        /// </summary>
        /// <param name="nickName">
        /// The nickname that should be checked
        /// </param>
        /// <param name="password">
        /// The password that should be checked
        /// </param>
        /// <returns>
        /// Return a structure thet define if Nickname and Password is valid
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when passed <paramref name="nickName"/> is null
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when passed <paramref name="password"/> is null
        /// </exception>
        public Structs.ValidNameAndPassword IsDataValid(string nickName, string password)
        {
            // checking
            if (string.IsNullOrWhiteSpace(nickName)) throw new System.ArgumentNullException(nameof(nickName));
            if (string.IsNullOrWhiteSpace(password)) throw new System.ArgumentNullException(nameof(password));

            // get first user with such nickname
            Entities.User foundedAccount = dbSet.AsNoTracking().AsEnumerable().FirstOrDefault(user => user.NickName == nickName);


            // if no user has been found return both value as null
            if (foundedAccount == null) return new Structs.ValidNameAndPassword() { IsNameValid = false, IsPasswordValid = false };

            // returns nickname as True and Password if the same
            return new Structs.ValidNameAndPassword()
                                {
                                    IsNameValid = true,
                                    IsPasswordValid = foundedAccount.Password == password
                                };
        }
        /// <summary>
        /// Gets user by nickname
        /// </summary>
        /// <param name="nickname">
        /// The user nickname by which user should be found.
        /// </param>
        /// <returns>
        /// Returns <see cref="Entities.User"/> if user has been found, otherwise — null.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when passed <paramref name="nickname"/> is null
        /// </exception>
        public Entities.User Get(string nickname)
        {
            // checking
            if (string.IsNullOrWhiteSpace(nickname)) throw new System.ArgumentNullException(nameof(nickname));
            // return user or null
            return dbSet.FirstOrDefault(user => user.NickName == nickname);
        }
        /// <summary>
        /// Deletes preset user.
        /// </summary>
        /// <param name="entityToDelete">User to delete.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when passed <paramref name="entityToDelete"/> is null.
        /// </exception>
        public override void Delete(Entities.User entityToDelete)
        {
            if (entityToDelete == null) throw new System.ArgumentNullException(nameof(entityToDelete));

            dbSet.Attach(entityToDelete);
            entityToDelete.PhotoLikes.ToList().ForEach(l => l.User = null);
            entityToDelete.Comments.ToList().ForEach(l => l.User = null);
            entityToDelete.CommentLikes.ToList().ForEach(l => l.User = null);
            context.Entry(entityToDelete).State = System.Data.Entity.EntityState.Modified;

            base.Delete(entityToDelete);
        } 
    }
}
