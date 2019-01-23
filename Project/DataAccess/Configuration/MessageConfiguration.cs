using DataAccess.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Configuration
{
    internal class MessageConfiguration : EntityTypeConfiguration<Message>
    {
        public MessageConfiguration()
        {
            Property(m => m.Text).IsRequired();

            HasOptional(m => m.Subject).WithMany(s => s.Messages).Map(m => m.MapKey("SubjectId"));
            HasRequired(m => m.User).WithMany(u => u.Messages).Map(m => m.MapKey("UserId"));
        }
    }
}
