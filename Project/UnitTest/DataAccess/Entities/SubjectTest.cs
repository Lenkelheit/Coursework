using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess.Entities;
using DA = DataAccess.Context;

namespace UnitTest.DataAccess.Entities
{
    [TestClass]
    public class SubjectTest
    {
        // FIELDS
        static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Integrated Security=True; Initial Catalog=SubjectTestDB";
        static DA.AppContext dbContext;
        static Resources.Classes.DbFiller dbFiller;
        // PROPERTIES
        public TestContext TestContext { get; set; }
        // INITIALIZERS
        [ClassInitialize]
        public static void Constructor(TestContext context)
        {
            dbFiller = new Resources.Classes.DbFiller();
            dbContext = new DA.AppContext(connectionString);
        }
        [ClassCleanup]
        public static void Finalizer()
        {
            dbContext.Dispose();
            System.Data.Entity.Database.Delete(connectionString);
        }
        [TestInitialize]
        public void Filler()
        {
            dbFiller.Fill(dbContext);
        }
        [TestCleanup]
        public void Cleaner()
        {
            dbFiller.Purge(dbContext);
        }

        [TestMethod]
        public void GetAllColumnsOfSubject()
        {
            // Arrange
            Subject expectedSubject = new Subject
            {
                Name = "Some test name",
                Messages = new List<Message>
                {
                    new Message
                    {
                        Date = DateTime.Now,
                        Text = "Some test text",
                        User = dbContext.Users.First(),
                    },
                    new Message
                    {
                        Date = DateTime.Now,
                        Text = "Some other test text",
                        User = dbContext.Users.First()
                    }
                }
            };

            // Act
            dbContext.Subjects.Add(expectedSubject);
            dbContext.SaveChanges();
            Subject actualSubject = dbContext.Subjects.Find(expectedSubject.Id);

            // Assert
            Assert.AreEqual(expectedSubject.Name, actualSubject.Name);
            Assert.AreEqual(expectedSubject.Messages, actualSubject.Messages);
        }
    }
}
