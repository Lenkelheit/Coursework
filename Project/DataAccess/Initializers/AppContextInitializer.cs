using DataAccess.Context;
using System.Data.Entity;

namespace DataAccess.Initializers
{
    internal class AppContextInitializer : DropCreateDatabaseAlways<AppContext>
    {
        protected override void Seed(AppContext context)
        {
            // subject initialization should be here
            throw new System.NotImplementedException();

            base.Seed(context);
        }
    }
}
