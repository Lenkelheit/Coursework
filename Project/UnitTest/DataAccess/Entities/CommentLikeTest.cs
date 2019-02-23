using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess.Entities;
using DA = DataAccess.Context;

namespace UnitTest.DataAccess.Entities
{
    [TestClass]
    [Ignore("Fail because cyclonic references")]
    public class CommentLikeTest
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
        public void GetAllColumnsOfCommentLike()
        {
            // Arrange
            CommentLike expectedCommentLike = new CommentLike
            {
                IsLiked = true,
                User = dbContext.Users.First(),
                Comment = dbContext.Comments.First()
            };

            // Act
            dbContext.CommentLike.Add(expectedCommentLike);
            dbContext.SaveChanges();
            CommentLike actualCommentLike = dbContext.CommentLike.Find(expectedCommentLike.Id);

            // Assert
            Assert.AreEqual(expectedCommentLike.IsLiked, actualCommentLike.IsLiked);
            Assert.AreEqual(expectedCommentLike.User, actualCommentLike.User);
            Assert.AreEqual(expectedCommentLike.Comment, actualCommentLike.Comment);
        }

        // EQUAL
        #region equal
        [TestMethod]
        public void Equals_NullValue_Exception()
        {
            // Arrange
            CommentLike commentLike1 = new CommentLike();
            CommentLike commentLike2 = null;

            // Act
            // Assert
            Assert.ThrowsException<System.ArgumentNullException>(() => commentLike1.Equals(commentLike2));
        }
        [TestMethod]
        public void Equals_DifferentType_False()
        {
            // Arrange
            CommentLike commentLike = new CommentLike();
            Subject subject = new Subject();

            // Act
            // Assert
            Assert.IsFalse(commentLike.Equals(subject));
            Assert.AreNotEqual(commentLike, subject);
            Assert.AreNotSame(commentLike, subject);
        }
        [TestMethod]
        public void Equals_TheSameInstance_True()
        {
            // Arrange
            CommentLike commentLike = new CommentLike();

            // Act
            // Assert
            Assert.IsTrue(commentLike.Equals(commentLike));
            Assert.AreEqual(commentLike, commentLike);
            Assert.AreSame(commentLike, commentLike);
        }
        [TestMethod]
        public void Equals_TheSameReference_True()
        {
            // Arrange
            CommentLike commentLike1 = new CommentLike();
            CommentLike commentLike2 = commentLike1;

            // Act
            // Assert
            Assert.IsTrue(commentLike1.Equals(commentLike2));
            Assert.AreEqual(commentLike1, commentLike2);
            Assert.AreSame(commentLike1, commentLike2);
        }
        [TestMethod]
        public void Equals_TheSameValue_True()
        {
            // Arrange
            CommentLike commentLike1 = new CommentLike { Id = System.Guid.Empty, IsLiked = true };
            CommentLike commentLike2 = new CommentLike { Id = System.Guid.Empty, IsLiked = true };

            // Act
            // Assert
            Assert.IsTrue(commentLike1.Equals(commentLike2));
            Assert.AreEqual(commentLike1, commentLike2);
            Assert.AreNotSame(commentLike1, commentLike2);
        }
        [TestMethod]
        public void Equals_DifferentValue_False()
        {
            // Arrange
            CommentLike commentLike1 = new CommentLike { Id = System.Guid.Empty, IsLiked = true };
            CommentLike commentLike2 = new CommentLike { Id = System.Guid.Empty, IsLiked = false };

            // Act
            // Assert
            Assert.IsFalse(commentLike1.Equals(commentLike2));
            Assert.AreNotSame(commentLike1, commentLike2);
        }
        [TestMethod]
        public void Equals_SameComment_True()
        {
            // Arrange
            Comment comment = new Comment() { Text = "Comment" };

            CommentLike commentLike1 = new CommentLike { Id = System.Guid.Empty, IsLiked = true, Comment = comment };
            CommentLike commentLike2 = new CommentLike { Id = System.Guid.Empty, IsLiked = true, Comment = comment };

            // Act
            // Assert
            Assert.IsTrue(commentLike1.Equals(commentLike2));
            Assert.AreEqual(commentLike1, commentLike2);
            Assert.AreNotSame(commentLike1, commentLike2);
        }
        [TestMethod]
        public void Equals_DifferentComment_False()
        {
            // Arrange
            Comment comment1 = new Comment() { Text = "Comment 1" };
            Comment comment2 = new Comment() { Text = "Comment 2" };

            CommentLike commentLike1 = new CommentLike { Id = System.Guid.Empty, IsLiked = true, Comment = comment1 };
            CommentLike commentLike2 = new CommentLike { Id = System.Guid.Empty, IsLiked = true, Comment = comment2 };

            // Act
            // Assert
            Assert.IsFalse(commentLike1.Equals(commentLike2));
            Assert.AreNotEqual(commentLike1, commentLike2);
            Assert.AreNotSame(commentLike1, commentLike2);
        }
        [TestMethod]
        public void Equals_SameUser_True()
        {
            // Arrange
            User user = new User { NickName = "User" };

            CommentLike commentLike1 = new CommentLike { Id = System.Guid.Empty, IsLiked = true, User = user };
            CommentLike commentLike2 = new CommentLike { Id = System.Guid.Empty, IsLiked = true, User = user };

            // Act
            // Assert
            Assert.IsTrue(commentLike1.Equals(commentLike2));
            Assert.AreEqual(commentLike1, commentLike2);
            Assert.AreNotSame(commentLike1, commentLike2);
        }
        [TestMethod]
        public void Equals_DifferentUser_False()
        {
            // Arrange
            User user1 = new User { NickName = "User 1" };
            User user2 = new User { NickName = "User 2" };

            CommentLike commentLike1 = new CommentLike { Id = System.Guid.Empty, IsLiked = true, User = user1 };
            CommentLike commentLike2 = new CommentLike { Id = System.Guid.Empty, IsLiked = true, User = user2 };

            // Act
            // Assert
            Assert.IsFalse(commentLike1.Equals(commentLike2));
            Assert.AreNotEqual(commentLike1, commentLike2);
            Assert.AreNotSame(commentLike1, commentLike2);
        }
        [TestMethod]
        public void Equals_FirstUserNull_False()
        {
            // Arrange
            User user1 = null;
            User user2 = new User { NickName = "User 2" };

            CommentLike commentLike1 = new CommentLike { Id = System.Guid.Empty, IsLiked = true, User = user1 };
            CommentLike commentLike2 = new CommentLike { Id = System.Guid.Empty, IsLiked = true, User = user2 };

            // Act
            // Assert
            Assert.IsFalse(commentLike1.Equals(commentLike2));
            Assert.AreNotEqual(commentLike1, commentLike2);
            Assert.AreNotSame(commentLike1, commentLike2);
        }
        [TestMethod]
        public void Equals_SecondUserNull_False()
        {
            // Arrange
            User user1 = new User { NickName = "User 1" };
            User user2 = null;

            CommentLike commentLike1 = new CommentLike { Id = System.Guid.Empty, IsLiked = true, User = user1 };
            CommentLike commentLike2 = new CommentLike { Id = System.Guid.Empty, IsLiked = true, User = user2 };

            // Act
            // Assert
            Assert.IsFalse(commentLike1.Equals(commentLike2));
            Assert.AreNotEqual(commentLike1, commentLike2);
            Assert.AreNotSame(commentLike1, commentLike2);
        }
        #endregion
    }
}
