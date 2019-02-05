using DataAccess.Context;
using System.Data.Entity;

namespace DataAccess.Initializers
{
    internal class AppContextInitializer : DropCreateDatabaseIfModelChanges<AppContext>
    {
        protected override void Seed(AppContext context)
        {
            // add default admin
            context.Users.Add(new Entities.User() { NickName = "Admin", Password = "1111", IsAdmin = true });

            // add subject
            context.Subjects.AddRange(new Entities.Subject[]
            {
                new Entities.Subject() { Name = "Error" },
                new Entities.Subject() { Name = "Suggestion" },
                new Entities.Subject() { Name = "Question" },
                new Entities.Subject() { Name = "Respond" },
            });

            base.Seed(context);
        }
    }
}
