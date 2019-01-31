using DataAccess.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Configuration
{
    internal class PhotoLikeConfiguration : EntityTypeConfiguration<PhotoLike>
    {
        public PhotoLikeConfiguration()
        {
            Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("PhotoLikes");

            });

            HasOptional(l => l.Photo).WithMany(p => p.Likes).Map(m => m.MapKey("PhotoId")).WillCascadeOnDelete(false);
            HasOptional(l => l.User).WithMany(u => u.PhotoLikes).Map(m => m.MapKey("UserId")).WillCascadeOnDelete(false);
        }
    }
}
