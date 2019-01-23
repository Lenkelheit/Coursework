using DataAccess.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Configuration
{
    internal class UserConfigurataion : EntityTypeConfiguration<User>
    {
        public UserConfigurataion()
        {
            Property(u => u.MainPhotoPath).IsOptional();
            Property(u => u.NickName).IsRequired();
            Property(u => u.Password).IsRequired();            

            HasMany(u => u.Followers).WithMany(u => u.Following)
                .Map(f => f.ToTable("Followers")
                            .MapLeftKey("UserId")
                            .MapRightKey("FollowerId"));
        }
    }
}
