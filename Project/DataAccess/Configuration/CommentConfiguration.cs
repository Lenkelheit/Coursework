using DataAccess.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Configuration
{
    internal class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            Property(m => m.Text).IsRequired();

            HasOptional(c => c.Photo).WithMany(p => p.Comments).Map(m => m.MapKey("PhotoId")).WillCascadeOnDelete(false);
            HasOptional(c => c.User).WithMany(u => u.Comments).Map(m => m.MapKey("UserId")).WillCascadeOnDelete(false);
        }
    }
}
