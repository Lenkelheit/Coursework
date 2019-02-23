using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess.Entities;
using DA = DataAccess.Context;

namespace UnitTest.DataAccess.Entities
{
    [TestClass]
    [Ignore("Fail because cyclonic references")]
    public class SubjectTest
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

        // EQUAL
        #region equal
        [TestMethod]
        public void Equals_NullValue_Exception()
        {
            // Arrange
            Subject subject1 = new Subject();
            Subject subject2 = null;

            // Act
            // Assert
            Assert.ThrowsException<System.ArgumentNullException>(() => subject1.Equals(subject2));
        }
        [TestMethod]
        public void Equals_DifferentType_False()
        {
            // Arrange
            Subject subject = new Subject();
            Message message = new Message();

            // Act
            // Assert
            Assert.IsFalse(subject.Equals(message));
            Assert.AreNotEqual(subject, message);
            Assert.AreNotSame(subject, message);
        }
        [TestMethod]
        public void Equals_TheSameInstance_True()
        {
            // Arrange
            Subject subject = new Subject();

            // Act
            // Assert
            Assert.IsTrue(subject.Equals(subject));
            Assert.AreEqual(subject, subject);
            Assert.AreSame(subject, subject);
        }
        [TestMethod]
        public void Equals_TheSameReference_True()
        {
            // Arrange
            Subject subject1 = new Subject();
            Subject subject2 = subject1;

            // Act
            // Assert
            Assert.IsTrue(subject1.Equals(subject2));
            Assert.AreEqual(subject1, subject2);
            Assert.AreSame(subject1, subject2);
        }
        [TestMethod]
        public void Equals_TheSameValue_True()
        {
            // Arrange
            Subject subject1 = new Subject() { Id = Guid.Empty, Name = "Subject", Messages = null };
            Subject subject2 = new Subject() { Id = Guid.Empty, Name = "Subject", Messages = null };

            // Act
            // Assert
            Assert.IsTrue(subject1.Equals(subject2));
            Assert.AreEqual(subject1, subject2);
            Assert.AreNotSame(subject1, subject2);
        }
        [TestMethod]
        public void Equals_DifferentValue_False()
        {
            // Arrange
            Subject subject1 = new Subject() { Id = Guid.Empty, Name = "Subject 1", Messages = null };
            Subject subject2 = new Subject() { Id = Guid.Empty, Name = "Subject 2", Messages = null };

            // Act
            // Assert
            Assert.IsFalse(subject1.Equals(subject2));
            Assert.AreNotEqual(subject1, subject2);
            Assert.AreNotSame(subject1, subject2);
        }
        [TestMethod]
        public void Equals_SameCollectionValue_True()
        {
            // Arrange
            Message[] messages = new Message[]
            {
                new Message { Text = "Text" },
                new Message { Text = "Text" },
                new Message { Text = "Text" }
            };

            Subject subject1 = new Subject() { Id = Guid.Empty, Name = "Subject", Messages = messages };
            Subject subject2 = new Subject() { Id = Guid.Empty, Name = "Subject", Messages = messages };

            // Act
            // Assert
            Assert.IsTrue(subject1.Equals(subject2));
            Assert.AreEqual(subject1, subject2);
            Assert.AreNotSame(subject1, subject2);
        }

        [TestMethod]
        public void Equals_SameCyclonic_True()
        {
            // Arrange
            const int MESSAGES_AMOUNT = 3;
            Message[] messages1 = new Message[MESSAGES_AMOUNT]
            {
                new Message { Text = "Text" },
                new Message { Text = "Text" },
                new Message { Text = "Text" }
            };

            Message[] messages2 = new Message[MESSAGES_AMOUNT]
            {
                new Message { Text = "Text" },
                new Message { Text = "Text" },
                new Message { Text = "Text" }
            };

            Subject subject1 = new Subject() { Id = Guid.Empty, Name = "Subject", Messages = messages1 };
            Subject subject2 = new Subject() { Id = Guid.Empty, Name = "Subject", Messages = messages2 };
            for (int i = 0; i < MESSAGES_AMOUNT; ++i)
            {
                messages1[i].Subject = subject1;
                messages2[i].Subject = subject2;
            }

            // Act
            // Assert
            Assert.IsTrue(subject1.Equals(subject2));
            Assert.AreEqual(subject1, subject2);
            Assert.AreNotSame(subject1, subject2);
        }
        [TestMethod]
        public void Equals_DifferentCollectionSize_False()
        {
            // Arrange
            Message[] messages1 = new Message[]
            {
                new Message { Id = Guid.Empty, Text = "Text" },
                new Message { Id = Guid.Empty, Text = "Text" },
                new Message { Id = Guid.Empty, Text = "Text" }
            };
            Message[] messages2 = new Message[]
            {
                new Message { Id = Guid.Empty, Text = "Text" },
                new Message { Id = Guid.Empty, Text = "Text" }
            };

            Subject subject1 = new Subject() { Id = Guid.Empty, Name = "Subject", Messages = messages1 };
            Subject subject2 = new Subject() { Id = Guid.Empty, Name = "Subject", Messages = messages2 };

            // Act
            // Assert
            Assert.IsFalse(subject1.Equals(subject2));
            Assert.AreNotEqual(subject1, subject2);
            Assert.AreNotSame(subject1, subject2);
        }
        [TestMethod]
        public void Equals_DifferentEnumerableSize_False()
        {
            // Arrange
            HashSet<Message> messages1 = new HashSet<Message>
            {
                new Message { Id = Guid.Empty, Text = "Text" },
                new Message { Id = Guid.Empty, Text = "Text" },
                new Message { Id = Guid.Empty, Text = "Text" }
            };
            
            HashSet<Message> messages2 = new HashSet<Message>
            {
                new Message { Id = Guid.Empty, Text = "Text" },
                new Message { Id = Guid.Empty, Text = "Text" }
            };

            Subject subject1 = new Subject() { Id = Guid.Empty, Name = "Subject", Messages = messages1 };
            Subject subject2 = new Subject() { Id = Guid.Empty, Name = "Subject", Messages = messages2 };

            // Act
            // Assert
            Assert.IsFalse(subject1.Equals(subject2));
            Assert.AreNotEqual(subject1, subject2);
            Assert.AreNotSame(subject1, subject2);
        }
        [TestMethod]
        public void Equals_DifferentCollectionValue_False()
        {
            // Arrange
            Message[] messages1 = new Message[]
            {
                new Message { Text = "Text 1" },
                new Message { Text = "Text 2" },
                new Message { Text = "Text 3" }
            };
            Message[] messages2 = new Message[]
            {
                new Message { Text = "Text 4" },
                new Message { Text = "Text 5" },
                new Message { Text = "Text 6" }
            };
            Subject subject1 = new Subject() { Id = Guid.Empty, Name = "Subject", Messages = messages1 };
            Subject subject2 = new Subject() { Id = Guid.Empty, Name = "Subject", Messages = messages2 };

            // Act
            // Assert
            Assert.IsFalse(subject1.Equals(subject2));
            Assert.AreNotEqual(subject1, subject2);
            Assert.AreNotSame(subject1, subject2);
        }
        [TestMethod]
        public void Equals_FirstCollectionNull_False()
        {
            // Arrange
            Message[] messages = new Message[]
            {
                new Message { Text = "Text" },
                new Message { Text = "Text" },
                new Message { Text = "Text" }
            };
            Subject subject1 = new Subject() { Id = Guid.Empty, Name = "Subject", Messages = null };
            Subject subject2 = new Subject() { Id = Guid.Empty, Name = "Subject", Messages = messages };

            // Act
            // Assert
            Assert.IsFalse(subject1.Equals(subject2));
            Assert.AreNotEqual(subject1, subject2);
            Assert.AreNotSame(subject1, subject2);
        }
        [TestMethod]
        public void Equals_SecondCollectionNull_False()
        {
            // Arrange
            Message[] messages = new Message[]
            {
                new Message { Text = "Text" },
                new Message { Text = "Text" },
                new Message { Text = "Text" }
            };
            Subject subject1 = new Subject() { Id = Guid.Empty, Name = "Subject", Messages = messages };
            Subject subject2 = new Subject() { Id = Guid.Empty, Name = "Subject", Messages = null };

            // Act
            // Assert
            Assert.IsFalse(subject1.Equals(subject2));
            Assert.AreNotEqual(subject1, subject2);
            Assert.AreNotSame(subject1, subject2);
        }
        #endregion
    }
}
