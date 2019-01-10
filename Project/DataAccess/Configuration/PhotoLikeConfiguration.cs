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

            HasRequired(l => l.Photo).WithMany(p => p.Likes).Map(m => m.MapKey("PhotoId"));
            HasOptional(l => l.User).WithMany(u => u.PhotoLikes).Map(m => m.MapKey("UserId"));
        }
    }
}
