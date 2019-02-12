using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess.Entities;
using DA = DataAccess.Context;

namespace UnitTest.DataAccess.Entities
{
    [TestClass]
    public class MessageTest
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
        [TestMethod]
        public void GetAllColumnsOfMessage()
        {
            // Arrange
            Message expectedMessage = new Message
            {
                Date = DateTime.Now,
                Text = "Some test text",
                User = dbContext.Users.First(),
                Subject = dbContext.Subjects.First()
            };

            // Act
            dbContext.Messages.Add(expectedMessage);
            dbContext.SaveChanges();
            Message actualMessage = dbContext.Messages.Find(expectedMessage.Id);

            // Assert
            Assert.AreEqual(expectedMessage.Date, actualMessage.Date);
            Assert.AreEqual(expectedMessage.Text, actualMessage.Text);
            Assert.AreEqual(expectedMessage.User, actualMessage.User);
            Assert.AreEqual(expectedMessage.Subject, actualMessage.Subject);
        }

        // EQUAL
        #region equal
        [TestMethod]
        public void Equals_NullValue_Exception()
        {
            // Arrange
            Message message1 = new Message();
            Message message2 = null;

            // Act
            // Assert
            Assert.ThrowsException<System.ArgumentNullException>(() => message1.Equals(message2));
        }
        [TestMethod]
        public void Equals_DifferentType_False()
        {
            // Arrange
            Message message = new Message();
            Subject subject = new Subject();

            // Act
            // Assert
            Assert.IsFalse(message.Equals(subject));
            Assert.AreNotEqual(message, subject);
            Assert.AreNotSame(message, subject);
        }
        [TestMethod]
        public void Equals_TheSameInstance_True()
        {
            // Arrange
            Message message = new Message();

            // Act
            // Assert
            Assert.IsTrue(message.Equals(message));
            Assert.AreEqual(message, message);
            Assert.AreSame(message, message);
        }
        [TestMethod]
        public void Equals_TheSameReference_True()
        {
            // Arrange
            Message message1 = new Message();
            Message message2 = message1;

            // Act
            // Assert
            Assert.IsTrue(message1.Equals(message2));
            Assert.AreEqual(message1, message2);
            Assert.AreSame(message1, message2);
        }
        [TestMethod]
        public void Equals_TheSameValue_True()
        {
            // Arrange
            Message message1 = new Message() { Id = Guid.Empty, Text = "Message" };
            Message message2 = new Message() { Id = Guid.Empty, Text = "Message" };

            // Act
            // Assert
            Assert.IsTrue(message1.Equals(message2));
            Assert.AreEqual(message1, message2);
            Assert.AreNotSame(message1, message2);
        }
        [TestMethod]
        public void Equals_DifferentValue_False()
        {
            // Arrange
            Message message1 = new Message() { Id = Guid.Empty, Text = "Message 1" };
            Message message2 = new Message() { Id = Guid.Empty, Text = "Message 2" };

            // Act
            // Assert
            Assert.IsFalse(message1.Equals(message2));
            Assert.AreNotEqual(message1, message2);
            Assert.AreNotSame(message1, message2);
        }
        [TestMethod]
        public void Equals_SameSubject_True()
        {
            // Arrange
            Subject subject1 = new Subject { Id = Guid.Empty, Name = "Subject" };
            Subject subject2 = new Subject { Id = Guid.Empty, Name = "Subject" };

            Message message1 = new Message() { Id = Guid.Empty, Text = "Message", Subject = subject1 };
            Message message2 = new Message() { Id = Guid.Empty, Text = "Message", Subject = subject2 };

            // Act
            // Assert
            Assert.IsTrue(message1.Equals(message2));
            Assert.AreEqual(message1, message2);
            Assert.AreNotSame(message1, message2);
        }
        [TestMethod]
        public void Equals_DifferentSubject_False()
        {
            // Arrange
            Subject subject1 = new Subject { Name = "Subject 1" };
            Subject subject2 = new Subject { Name = "Subject 2" };

            Message message1 = new Message() { Id = Guid.Empty, Text = "Message", Subject = subject1 };
            Message message2 = new Message() { Id = Guid.Empty, Text = "Message", Subject = subject2 };

            // Act
            // Assert
            Assert.IsFalse(message1.Equals(message2));
            Assert.AreNotEqual(message1, message2);
            Assert.AreNotSame(message1, message2);
        }
        [TestMethod]
        public void Equals_SameUser_True()
        {
            // Arrange
            User user1 = new User { NickName = "User" };
            User user2 = new User { NickName = "User" };

            Message message1 = new Message() { Id = Guid.Empty, Text = "Message", User = user1 };
            Message message2 = new Message() { Id = Guid.Empty, Text = "Message", User = user2 };

            // Act
            // Assert
            Assert.IsTrue(message1.Equals(message2));
            Assert.AreEqual(message1, message2);
            Assert.AreNotSame(message1, message2);
        }
        [TestMethod]
        public void Equals_DifferentUser_False()
        {
            // Arrange
            User user1 = new User { NickName = "User 1" };
            User user2 = new User { NickName = "User 2" };

            Message message1 = new Message() { Id = Guid.Empty, Text = "Message", User = user1 };
            Message message2 = new Message() { Id = Guid.Empty, Text = "Message", User = user2 };

            // Act
            // Assert
            Assert.IsFalse(message1.Equals(message2));
            Assert.AreNotEqual(message1, message2);
            Assert.AreNotSame(message1, message2);
        }
        [TestMethod]
        public void Equals_FirstUserNull_False()
        {
            // Arrange
            User user1 = null;
            User user2 = new User { NickName = "User 2" };

            Message message1 = new Message() { Id = Guid.Empty, Text = "Message", User = user1 };
            Message message2 = new Message() { Id = Guid.Empty, Text = "Message", User = user2 };

            // Act
            // Assert
            Assert.IsFalse(message1.Equals(message2));
            Assert.AreNotEqual(message1, message2);
            Assert.AreNotSame(message1, message2);
        }
        [TestMethod]
        public void Equals_SecondUserNull_False()
        {
            // Arrange
            User user1 = new User { NickName = "User 1" };
            User user2 = null;

            Message message1 = new Message() { Id = Guid.Empty, Text = "Message", User = user1 };
            Message message2 = new Message() { Id = Guid.Empty, Text = "Message", User = user2 };

            // Act
            // Assert
            Assert.IsFalse(message1.Equals(message2));
            Assert.AreNotEqual(message1, message2);
            Assert.AreNotSame(message1, message2);
        }
        #endregion
    }
}
