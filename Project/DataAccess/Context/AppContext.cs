using System.Data.Entity;

using DataAccess.Entities;
using DataAccess.Configuration;

namespace DataAccess.Context
{
    public sealed class AppContext : DbContext
    {
        // PROPERTIES
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PhotoLike> PhotoLike { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentLike> CommentLike { get; set; }

        // CONSTRUCTORS
        public AppContext(string connectionString)
            : base(connectionString) {  }

        static AppContext()
        {
            Database.SetInitializer(new Initializers.AppContextInitializer());
        }

        // METHODS
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new CommentLikeConfiguration());
            modelBuilder.Configurations.Add(new MessageConfiguration());
            modelBuilder.Configurations.Add(new PhotoConfiguration());
            modelBuilder.Configurations.Add(new PhotoLikeConfiguration());
            modelBuilder.Configurations.Add(new SubjectConfiguration());
            modelBuilder.Configurations.Add(new UserConfigurataion());

            base.OnModelCreating(modelBuilder);
        }
    }
}
