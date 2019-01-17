using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess.Entities;
using DataAccess.Repositories;
using DA = DataAccess.Context;
using System.Data.Entity.Infrastructure;

namespace UnitTest.DataAccess.Repositories
{
    [TestClass]
    public class MessageRepositoryTest
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
            dbFiller = new Resources.Classes.DbFiller();
            dbContext = new DA.AppContext(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Largo\Source\Repos\Coursework\Project\UnitTest\Resources\DataAccess\TestDB.mdf; Integrated Security=True;Initial Catalog=TestDB");
        }
        [ClassCleanup]
        public static void Finalizer()
        {
            dbContext.Dispose();
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
        // COUNT
        #region COUNT
        [TestMethod]
        public void Count()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            int expectedMessageInDb = 15;

            // Act
            int actualMessageInDb = messageRepository.Count();

            // Assert
            Assert.AreEqual(expectedMessageInDb, actualMessageInDb);
        }
        [TestMethod]
        public void CountIfMessageWithoutSubject()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            int expectedMessageWithoutSubjectInDb = 2;

            // Act
            // probably fail because lazy loading or because check data in DB
            int actualMessageWithoutSubjectInDb = messageRepository.Count(message => message.Subject == null);

            // Assert
            Assert.AreEqual(expectedMessageWithoutSubjectInDb, actualMessageWithoutSubjectInDb);
        }
        [TestMethod]
        public void CountIfDayIs15()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            int expectedMessageOn15Day = 6;

            // Act
            int actualMessageOn15Day = messageRepository.Count(message => message.Date.Day == 15);

            // Assert
            Assert.AreEqual(expectedMessageOn15Day, actualMessageOn15Day);
        }
        #endregion
        // GET
        #region GET
        [TestMethod]
        public void GetAll()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            int expectedMessageInDb = 15;

            // Act
            IEnumerable<Message> messageFromDB = messageRepository.Get();
            int actualMessageInDB = messageFromDB.Count();

            // Assert
            Assert.AreEqual(expectedMessageInDb, actualMessageInDB);
            CollectionAssert.AreEquivalent(dbContext.Messages.ToArray(), messageFromDB.ToArray());
        }
        [TestMethod]
        public void GetFilterByYear()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            int expectedUserInDb = 4;

            // Act
            IEnumerable<Message> messageFromDB = messageRepository.Get(filter: message => message.Date.Year > 2010);
            int actualUserInDb = messageFromDB.Count();

            // Assert
            Assert.AreEqual(expectedUserInDb, actualUserInDb);
            CollectionAssert.IsSubsetOf(messageFromDB.ToArray(), dbContext.Messages.ToArray());
        }
        [TestMethod]
        public void GetOrderByDay()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            int expectedMessage = 15;

            // Act
            IEnumerable<Message> messageFromDB = messageRepository.Get(orderBy: message => message.OrderBy(m => m.Date.Day));
            int actualUserInDb = messageFromDB.Count();

            // Assert
            Assert.AreEqual(expectedMessage, actualUserInDb);
            CollectionAssert.AreEqual(dbContext.Messages.OrderBy(m => m.Date.Day).ToArray(), messageFromDB.ToArray());
        }
        [TestMethod]
        public void GetFilterAndOrder()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            int expectedUserInDb = 2;

            // Act
            IEnumerable<Message> messageFromDB = messageRepository.Get(filter: m => m.Date.Month == 1, orderBy: o => o.OrderByDescending(m => m.Date.Year));
            int actualMessageInDb = messageFromDB.Count();

            // Assert
            Assert.AreEqual(expectedUserInDb, actualMessageInDb);
            CollectionAssert.AreEqual(dbContext.Messages.Where(m => m.Date.Month == 1).OrderByDescending(m => m.Date.Year).ToArray(), messageFromDB.ToArray());
        }
        #endregion
        // GET BY ID
        #region GET BY ID
        [TestMethod]
        public void GetById()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            Message expectedMessage = dbContext.Messages.First();
            int idToSearch = expectedMessage.Id;

            // Act
            Message messageFromDB = messageRepository.Get(idToSearch);

            // Assert
            Assert.AreEqual(expectedMessage, messageFromDB);
        }
        [TestMethod]
        public void GetByWrongId_Null()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            int wrongId = int.MaxValue;
            Message expectedMessageFromDb = null;

            // Act
            Message actualMessageFromDb = messageRepository.Get(wrongId);

            // Assert
            Assert.AreEqual(expectedMessageFromDb, actualMessageFromDb);
        }
        #endregion
        // INSERT
        #region INSERT
        [TestMethod]
        public void AddMessage()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            Message message = new Message
            {
                Date = System.DateTime.Now,
                User = dbContext.Users.First(),
                Subject = dbContext.Subjects.First(),
                Text = "Test test test test test test test"
            };

            // Act
            messageRepository.Insert(message);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Messages.ToList(), message);
        }
        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"..\..\Resources\DataAccess\Repositories\WrongMessage.xml",
            tableName: "Message",
            dataAccessMethod: DataAccessMethod.Random)]
        public void AddWrongMessage_Exception()
        {
            Assert.Fail("throws exception, but not the one that was expected");
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            bool hasUser = Convert.ToBoolean(TestContext.DataRow["HasUser"]);
            bool hasSubject = Convert.ToBoolean(TestContext.DataRow["HasSubject"]);
            Message message = new Message
            {
                Date = new DateTime(year: Convert.ToInt32(TestContext.DataRow["Year"]), 
                                    month: Convert.ToInt32(TestContext.DataRow["Month"]), 
                                    day: Convert.ToInt32(TestContext.DataRow["Day"])),
                User = hasUser ? dbContext.Users.First() : null,
                Subject = hasSubject ? dbContext.Subjects.First() : null,
                Text = Convert.ToString(TestContext.DataRow["Text"])
            };

            // Act
            messageRepository.Insert(message);

            // Assert
            Assert.ThrowsException<System.Data.Entity.Validation.DbEntityValidationException>(() => dbContext.SaveChanges());
            // undo adding
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(message);
        }

        #endregion
        // DELETE BY KEY
        #region DELETE BY KEY
        [TestMethod]
        public void DeleteByKey()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            Message expectedMessageToDelete = dbContext.Messages.First();
            int idToDelete = expectedMessageToDelete.Id;

            // Act
            messageRepository.Delete(idToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Messages.ToArray(), expectedMessageToDelete);
        }
        [TestMethod]
        public void DeleteByWrongKey_Exception()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            int wrongId = 200;

            // Act
            // Assert
            Assert.ThrowsException<InvalidOperationException>(() => messageRepository.Delete(wrongId));
        }
        [TestMethod]
        public void DeleteByNullKey_Exception()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            object wrongId = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => messageRepository.Delete(wrongId));
        }
        #endregion
        // DELETE BY VALUE
        #region DELETE BY VALUE
        [TestMethod]
        public void DeleteByValue()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            Message messageToDelete = dbContext.Messages.First();

            // Act
            messageRepository.Delete(messageToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Messages.ToArray(), messageToDelete);
        }
        [TestMethod]
        public void DeleteByNullValue()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            Message entityToDelete = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => messageRepository.Delete(entityToDelete: entityToDelete));
        }
        [TestMethod]
        public void DeleteByChangedValue()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            Message changedMessageToDelete = dbContext.Messages.First();
            changedMessageToDelete.Text += "Chnaged it";
            changedMessageToDelete.Subject = dbContext.Subjects.First();

            // Act
            messageRepository.Delete(entityToDelete: changedMessageToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Messages.ToArray(), changedMessageToDelete);
        }
        #endregion
        // UPDATE
        #region UPDATE
        [TestMethod]
        public void Update()
        {
            // Arrange
            MessageRepository messageRepository = new MessageRepository(dbContext);
            Message messageToUpdate = dbContext.Messages.First();
            string newText = "Sets here new text for message";

            // Act
            messageToUpdate.Text = newText;
            messageRepository.Update(messageToUpdate);

            // Assert
            Assert.AreEqual(dbContext.Messages.Find(messageToUpdate.Id).Text, newText);
        }
        #endregion
    }
}
