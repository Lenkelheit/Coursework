using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess.Entities;
using DA = DataAccess.Context;

namespace UnitTest.DataAccess.Entities
{
    [TestClass]
    public class CommentTest
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
        public void GetAllColumnsOfComment()
        {
            // Arrange
            User user = dbContext.Users.First();
            Comment expectedComment = new Comment
            {
                Date = DateTime.Now,
                Text = "Some test text",
                User = user,
                Photo = dbContext.Photos.First(),
                Likes = new List<CommentLike>
                {
                    new CommentLike{ IsLiked = true, User = user },
                    new CommentLike{ IsLiked = false, User = dbContext.Users.ToArray()[1] }
                }
            };

            // Act
            dbContext.Comments.Add(expectedComment);
            dbContext.SaveChanges();
            Comment actualComment = dbContext.Comments.Find(expectedComment.Id);

            // Assert
            Assert.AreEqual(expectedComment.Date, actualComment.Date);
            Assert.AreEqual(expectedComment.Text, actualComment.Text);
            Assert.AreEqual(expectedComment.User, actualComment.User);
            Assert.AreEqual(expectedComment.Photo, actualComment.Photo);
            CollectionAssert.AreEqual(expectedComment.Likes.ToArray(), actualComment.Likes.ToArray());
        }

        // EQUAL
        #region equal
        [TestMethod]
        public void Equals_NullValue_Exception()
        {
            // Arrange
            Comment comment1 = new Comment();
            Comment comment2 = null;

            // Act
            // Assert
            Assert.ThrowsException<System.ArgumentNullException>(() => comment1.Equals(comment2));
        }
        [TestMethod]
        public void Equals_DifferentType_False()
        {
            // Arrange
            Comment comment = new Comment();
            Subject subject = new Subject();

            // Act
            // Assert
            Assert.IsFalse(comment.Equals(subject));
            Assert.AreNotEqual(comment, subject);
            Assert.AreNotSame(comment, subject);
        }
        [TestMethod]
        public void Equals_TheSameInstance_True()
        {
            // Arrange
            Comment comment = new Comment();

            // Act
            // Assert
            Assert.IsTrue(comment.Equals(comment));
            Assert.AreSame(comment, comment);
        }
        [TestMethod]
        public void Equals_TheSameReference_True()
        {
            // Arrange
            Comment comment1 = new Comment();
            Comment comment2 = comment1;

            // Act
            // Assert
            Assert.IsTrue(comment1.Equals(comment2));
            Assert.AreEqual(comment1, comment2);
            Assert.AreSame(comment1, comment2);
        }
        [TestMethod]
        public void Equals_TheSameValue_True()
        {
            // Arrange
            Comment comment1 = new Comment { Text = "Comment" };
            Comment comment2 = new Comment { Text = "Comment" };

            // Act
            // Assert
            Assert.IsTrue(comment1.Equals(comment2));
            Assert.AreEqual(comment1, comment2);
            Assert.AreNotSame(comment1, comment2);
        }
        [TestMethod]
        public void Equals_DifferentValue_False()
        {
            // Arrange
            Comment comment1 = new Comment { Text = "Comment 1" };
            Comment comment2 = new Comment { Text = "Comment 2" };

            // Act
            // Assert
            Assert.IsFalse(comment1.Equals(comment2));
            Assert.AreNotEqual(comment1, comment2);
            Assert.AreNotSame(comment1, comment2);
        }
        [TestMethod]
        public void Equals_SameUser_True()
        {
            // Arrange
            User user1 = new User { NickName = "User" };
            User user2 = new User { NickName = "User" };

            Comment comment1 = new Comment { Text = "Comment", User = user1 };
            Comment comment2 = new Comment { Text = "Comment", User = user2 };

            // Act
            // Assert
            Assert.IsTrue(comment1.Equals(comment2));
            Assert.AreEqual(comment1, comment2);
            Assert.AreNotSame(comment1, comment2);
        }
        [TestMethod]
        public void Equals_DifferentUser_False()
        {
            // Arrange
            User user1 = new User { NickName = "User 1" };
            User user2 = new User { NickName = "User 2" };

            Comment comment1 = new Comment { Text = "Comment", User = user1 };
            Comment comment2 = new Comment { Text = "Comment", User = user2 };

            // Act
            // Assert
            Assert.IsFalse(comment1.Equals(comment2));
            Assert.AreNotEqual(comment1, comment2);
            Assert.AreNotSame(comment1, comment2);
        }
        [TestMethod]
        public void Equals_SameLikes_True()
        {
            // Arrange
            CommentLike[] commentLikes1 = new CommentLike[]
            {
                new CommentLike { IsLiked = true  },
                new CommentLike { IsLiked = true  },
                new CommentLike { IsLiked = true  },
                new CommentLike { IsLiked = false },
            };
            CommentLike[] commentLikes2 = new CommentLike[]
            {
                new CommentLike { IsLiked = true  },
                new CommentLike { IsLiked = true  },
                new CommentLike { IsLiked = true  },
                new CommentLike { IsLiked = false },
            };

            Comment comment1 = new Comment { Text = "Comment", Likes = commentLikes1 };
            Comment comment2 = new Comment { Text = "Comment", Likes = commentLikes2 };

            // Act
            // Assert
            Assert.IsTrue(comment1.Equals(comment2));
            Assert.AreEqual(comment1, comment2);
            Assert.AreNotSame(comment1, comment2);
        }
        [TestMethod]
        public void Equals_DifferentLikes_False()
        {
            // Arrange
            CommentLike[] commentLikes1 = new CommentLike[]
            {
                new CommentLike { IsLiked = true  },
                new CommentLike { IsLiked = true  },
                new CommentLike { IsLiked = true  },
                new CommentLike { IsLiked = false },
            };
            CommentLike[] commentLikes2 = new CommentLike[]
            {
                new CommentLike { IsLiked = true  },
                new CommentLike { IsLiked = false  },
                new CommentLike { IsLiked = false  },
                new CommentLike { IsLiked = false },
            };

            Comment comment1 = new Comment { Text = "Comment", Likes = commentLikes1 };
            Comment comment2 = new Comment { Text = "Comment", Likes = commentLikes2 };

            // Act
            // Assert
            Assert.IsFalse(comment1.Equals(comment2));
            Assert.AreNotEqual(comment1, comment2);
            Assert.AreNotSame(comment1, comment2);
        }
        [TestMethod]
        public void Equals_FirstPhotoNull_False()
        {
            // Arrange
            Photo photo1 = null;
            Photo photo2 = new Photo { Path = "photo name" };

            Comment comment1 = new Comment { Text = "Comment", Photo = photo1 };
            Comment comment2 = new Comment { Text = "Comment", Photo = photo2 };

            // Act
            // Assert
            Assert.IsFalse(comment1.Equals(comment2));
            Assert.AreNotEqual(comment1, comment2);
            Assert.AreNotSame(comment1, comment2);
        }
        [TestMethod]
        public void Equals_SecondPhotoNull_False()
        {
            // Arrange
            Photo photo1 = new Photo { Path = "photo name" };
            Photo photo2 = null;

            Comment comment1 = new Comment { Text = "Comment", Photo = photo1 };
            Comment comment2 = new Comment { Text = "Comment", Photo = photo2 };

            // Act
            // Assert
            Assert.IsFalse(comment1.Equals(comment2));
            Assert.AreNotEqual(comment1, comment2);
            Assert.AreNotSame(comment1, comment2);
        }
        #endregion
    }
}
