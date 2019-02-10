using Microsoft.VisualStudio.TestTools.UnitTesting;

using DA = DataAccess.Context;

namespace UnitTest.Resources.Initializers
{
    [TestClass]
    public class DatabaseInitializer
    {
        // FIELDS
        private static DA.AppContext dbContext;
        private static string connectionString;

        // INITIALIZERS
        [AssemblyInitialize]
        public static void Constructor(TestContext context)
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TestDBConnection"].ConnectionString;
            dbContext = new DA.AppContext(connectionString);
            dbContext.Configuration.AutoDetectChangesEnabled = false;
        }
        [AssemblyCleanup]
        public static void Finalizer()
        {
            dbContext.Dispose();
            if (Core.Configuration.TestConfig.DO_DELETE_TEST_DATABASE) System.Data.Entity.Database.Delete(connectionString);
        }

        // PROPERTIES
        public static DA.AppContext DBContext => dbContext;
    }
}
