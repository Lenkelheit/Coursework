using Microsoft.VisualStudio.TestTools.UnitTesting;

using DA = DataAccess.Context;

namespace UnitTest.Resources.Initializers
{
    [TestClass]
    public class DatabaseInitializer
    {
        // FIELDS
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TestDBConnection"].ConnectionString;
        // PROPERTIES
        public static DA.AppContext DBContext { get; private set; }
        // INITIALIZERS
        [AssemblyInitialize]
        public static void Constructor(TestContext context)
        {
            DBContext = new DA.AppContext(connectionString);
        }
        [AssemblyCleanup]
        public static void Finalizer()
        {
            DBContext.Dispose();
            if (Core.Configuration.AppConfig.DO_DELETE_TEST_DB) System.Data.Entity.Database.Delete(connectionString);
        }
    }
}
