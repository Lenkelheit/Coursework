using DataAccess.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Configuration
{
    internal class PhotoConfiguration : EntityTypeConfiguration<Photo>
    {
        public PhotoConfiguration()
        {
            Property(p => p.Path).IsRequired();

            HasRequired(p => p.User).WithMany(u => u.Photos).Map(m => m.MapKey("UserId")).WillCascadeOnDelete(false);
        }
    }
}
