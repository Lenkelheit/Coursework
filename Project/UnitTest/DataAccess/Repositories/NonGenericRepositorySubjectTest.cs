using System;
using System.Linq;
using System.Data.Entity.Infrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess.Entities;
using DataAccess.Repositories;
using DA = DataAccess.Context;

namespace UnitTest.DataAccess.Repositories
{
    [TestClass]
    public class NonGenericRepositorySubjectTest
    {
        // FIELDS
        static DA.AppContext dbContext;
        static Resources.Classes.DbFiller dbFiller;
        // PROPERTIES
        public TestContext TestContext { get; set; }
        // INITIALIZERS
        [ClassInitialize]
        public static void Constructor(TestContext context)
        {
            dbFiller = Resources.Classes.DbFiller.Instance;

            dbContext = Resources.Initializers.DatabaseInitializer.DBContext;
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

        // TEST
        // GET BY ID
        #region GET BY ID
        [TestMethod]
        public void GetById()
        {
            // Arrange
            NonGenericRepository subjectRepository = new NonGenericRepository(dbContext, typeof(Subject));
            Guid idToSearch = dbContext.Subjects.First().Id;
            Subject expectedSubject = dbContext.Subjects.Find(idToSearch);

            // Act
            Subject subjectFromDB = subjectRepository.Get(idToSearch) as Subject;

            // Assert
            Assert.AreEqual(expectedSubject, subjectFromDB);
        }
        [TestMethod]
        public void GetByWrongId_Null()
        {
            // Arrange
            NonGenericRepository subjectRepository = new NonGenericRepository(dbContext, typeof(Subject));
            Guid wrongId = default(Guid);
            Subject expectedSubjectFromDb = null;

            // Act
            Subject actualSubjectFromDb = subjectRepository.Get(wrongId) as Subject;

            // Assert
            Assert.AreEqual(expectedSubjectFromDb, actualSubjectFromDb);
        }
        #endregion
        // INSERT
        #region INSERT
        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"..\..\Resources\DataAccess\Repositories\RegularSubject.xml",
            tableName: "Subject",
            dataAccessMethod: DataAccessMethod.Random)]
        public void AddRegularSubject()
        {
            // Arrange
            NonGenericRepository subjectRepository = new NonGenericRepository(dbContext, typeof(Subject));
            Subject subject = new Subject { Name = Convert.ToString(TestContext.DataRow["Name"]) };

            // Act
            subjectRepository.Insert(subject);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Subjects.ToList(), subject);
        }
        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"..\..\Resources\DataAccess\Repositories\WrongSubject.xml",
            tableName: "Subject",
            dataAccessMethod: DataAccessMethod.Random)]
        public void AddSubjectWithWrongNameLength()
        {
            // Arrange
            NonGenericRepository subjectRepository = new NonGenericRepository(dbContext, typeof(Subject));
            Subject subject = new Subject { Name = Convert.ToString(TestContext.DataRow["Name"]) };

            // Act
            subjectRepository.Insert(subject);

            // Assert
            Assert.ThrowsException<System.Data.Entity.Validation.DbEntityValidationException>(() => dbContext.SaveChanges());
            // undo adding
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(subject);
        }
        #endregion
        // DELETE BY VALUE
        #region DELETE BY VALUE
        [TestMethod]
        public void DeleteByValue()
        {
            // Arrange
            NonGenericRepository subjectRepository = new NonGenericRepository(dbContext, typeof(Subject));
            Subject subjectToDelete = dbContext.Subjects.First(s => s.Name == "Subject 1");

            // Act
            // This subject has messages that must have "subject: null" when one will be deleted.
            subjectRepository.Delete(subjectToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Subjects.ToArray(), subjectToDelete);
            // Checks if all subject's messages are null.
            Assert.IsTrue(dbContext.Messages.AsEnumerable().Any(m => m.Subject == null || m.Subject.Id != subjectToDelete.Id));
        }
        [TestMethod]
        public void DeleteByNullValue()
        {
            // Arrange
            NonGenericRepository subjectRepository = new NonGenericRepository(dbContext, typeof(Subject));

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => subjectRepository.Delete(entityToDelete: null));
        }
        [TestMethod]
        public void DeleteByChangedValue()
        {
            // Arrange
            NonGenericRepository subjectRepository = new NonGenericRepository(dbContext, typeof(Subject));
            Subject changedSubjectToDelete = dbContext.Subjects.First(s => s.Name == "Subject 1");
            changedSubjectToDelete.Name += "Changed it";

            // Act
            subjectRepository.Delete(entityToDelete: changedSubjectToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Subjects.ToArray(), changedSubjectToDelete);
            // Checks if all subject's messages are null.
            Assert.IsTrue(dbContext.Messages.AsEnumerable().Any(m => m.Subject == null || m.Subject.Id != changedSubjectToDelete.Id));
        }
        #endregion
        // UPDATE
        #region UPDATE
        [TestMethod]
        public void Update()
        {
            // Arrange
            NonGenericRepository subjectRepository = new NonGenericRepository(dbContext, typeof(Subject));
            Subject subjectToUpdate = dbContext.Subjects.First(s => s.Name == "Subject 1");
            string newSubjectName = "New Subject Name";

            // Act
            subjectToUpdate.Name = newSubjectName;
            subjectRepository.Update(subjectToUpdate);
            dbContext.SaveChanges();

            // Assert
            Assert.AreEqual(dbContext.Subjects.Find(subjectToUpdate.Id).Name, newSubjectName);
        }
        #endregion
    }
}
