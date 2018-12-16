using System.Data.Entity;

using DataAccess.Entities;

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

        // METHODS
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
