using DataAccess.Context;
using System.Data.Entity;

namespace DataAccess.Initializers
{
    internal class AppContextInitializer : DropCreateDatabaseAlways<AppContext>
    {
        protected override void Seed(AppContext context)
        {
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
