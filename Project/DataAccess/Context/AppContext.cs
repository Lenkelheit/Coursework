using System.Data.Entity;
using System.Linq;

using DataAccess.Entities;
using DataAccess.Configuration;

namespace DataAccess.Context
{
    /// <summary>
    /// Contains DbSets
    /// </summary>
    public sealed class AppContext : DbContext
    {
        // PROPERTIES
        /// <summary>
        /// A user set
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// A photo set
        /// </summary>
        public DbSet<Photo> Photos { get; set; }
        /// <summary>
        /// A photo likes set
        /// </summary>
        public DbSet<PhotoLike> PhotoLike { get; set; }
        /// <summary>
        /// A comments set
        /// </summary>
        public DbSet<Comment> Comments { get; set; }
        /// <summary>
        /// A comment likes set
        /// </summary>
        public DbSet<CommentLike> CommentLike { get; set; }
        /// <summary>
        /// A messages set
        /// </summary>
        public DbSet<Message> Messages { get; set; }
        /// <summary>
        /// A subject set
        /// </summary>
        public DbSet<Subject> Subjects { get; set; }

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="AppContext"/>
        /// </summary>
        public AppContext()
            : base() { }
        /// <summary>
        /// Initialize a new instance of <see cref="AppContext"/>
        /// </summary>
        /// <param name="connectionString">
        /// A connection string
        /// </param>
        public AppContext(string connectionString)
            : base(connectionString) {  }

        static AppContext()
        {
            Database.SetInitializer(new Initializers.AppContextInitializer());
        }

        // METHODS
        /// <summary>
        /// Build a database table by current model configuration
        /// </summary>
        /// <param name="modelBuilder">
        /// Configurate model 
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SubjectConfiguration());
            modelBuilder.Configurations.Add(new CommentLikeConfiguration());
            modelBuilder.Configurations.Add(new PhotoLikeConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new MessageConfiguration());
            modelBuilder.Configurations.Add(new PhotoConfiguration());
            modelBuilder.Configurations.Add(new UserConfigurataion());

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// Amount of transaction that has been confirmed.
        /// </returns>
        public override int SaveChanges()
        {
            PhotoLike.Local.Where(ph => ph.Photo == null || ph.User == null).ToList().ForEach(ph => PhotoLike.Remove(ph));

            Photos.Local.Where(p => p.User == null).ToList().ForEach(p => Photos.Remove(p));

            CommentLike.Local.Where(cl => cl.Comment == null || cl.User == null).ToList().ForEach(cl => CommentLike.Remove(cl));

            Comments.Local.Where(c => c.Photo == null || c.User == null).ToList().ForEach(c => Comments.Remove(c));

            return base.SaveChanges();
        }
    }
}
