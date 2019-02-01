using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess.Entities;
using DA = DataAccess.Context;

namespace UnitTest.GeneralDatabaseCreator
{
    [TestClass]
    public class DatabaseCreatorAndDeleter
    {
        // FIELDS
        static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Integrated Security=True; Initial Catalog=TestDB";
        static DA.AppContext dbContext;
        // PROPERTIES
        public TestContext TestContext { get; set; }
        // INITIALIZERS
        [AssemblyInitialize]
        public static void Constructor(TestContext context)
        {
            dbContext = new DA.AppContext(connectionString);
        }
        [AssemblyCleanup]
        public static void Finalizer()
        {
            dbContext.Dispose();
            System.Data.Entity.Database.Delete(connectionString);
        }
    }
}
