using DataAccess.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Configuration
{
    internal class CommentLikeConfiguration : EntityTypeConfiguration<CommentLike>
    {
        public CommentLikeConfiguration()
        {
            Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("CommentLikes");
            });

            HasOptional(l => l.User).WithMany(u => u.CommentLikes).Map(m => m.MapKey("UserId"));
            HasRequired(l => l.Comment).WithMany(c => c.Likes).Map(m => m.MapKey("CommentId"));
        }
    }
}
