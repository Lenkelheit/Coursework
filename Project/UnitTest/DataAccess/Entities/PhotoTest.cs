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
    public class PhotoTest
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
        public void GetAllColumnsOfPhoto()
        {
            // Arrange
            User user = dbContext.Users.First();
            Photo expectedPhoto = new Photo
            {
                Path = "Some test path.jpg",
                User = user,
                Likes = new List<PhotoLike>
                {
                    new PhotoLike{ IsLiked = true, User = user },
                    new PhotoLike{ IsLiked = false, User = dbContext.Users.ToArray()[1] }
                },
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Date = DateTime.Now,
                        Text = "Some test text",
                        User = user,
                        Likes = new List<CommentLike>
                        {
                            new CommentLike{ IsLiked = true, User = user },
                            new CommentLike{ IsLiked = false, User = dbContext.Users.ToArray()[1] }
                        }
                    }
                }
            };

            // Act
            dbContext.Photos.Add(expectedPhoto);
            dbContext.SaveChanges();
            Photo actualPhoto = dbContext.Photos.Find(expectedPhoto.Id);

            // Assert
            Assert.AreEqual(expectedPhoto.Path, actualPhoto.Path);
            Assert.AreEqual(expectedPhoto.User, actualPhoto.User);
            CollectionAssert.AreEqual(expectedPhoto.Likes.ToArray(), actualPhoto.Likes.ToArray());
            CollectionAssert.AreEqual(expectedPhoto.Comments.ToArray(), actualPhoto.Comments.ToArray());
        }


        // EQUAL
        #region equal
        [TestMethod]
        public void Equals_NullValue_Exception()
        {
            // Arrange
            Photo photo1 = new Photo();
            Photo photo2 = null;

            // Act
            // Assert
            Assert.ThrowsException<System.ArgumentNullException>(() => photo1.Equals(photo2));
        }
        [TestMethod]
        public void Equals_DifferentType_False()
        {
            // Arrange
            Photo photo = new Photo();
            PhotoLike photoLike = new PhotoLike();

            // Act
            // Assert
            Assert.IsFalse(photo.Equals(photoLike));
            Assert.AreNotEqual(photo, photoLike);
            Assert.AreNotSame(photo, photoLike);
        }
        [TestMethod]
        public void Equals_TheSameInstance_True()
        {
            // Arrange
            Photo photo = new Photo();
            
            // Act
            // Assert
            Assert.IsTrue(photo.Equals(photo));
            Assert.AreEqual(photo, photo);
            Assert.AreSame(photo, photo);
        }
        [TestMethod]
        public void Equals_TheSameReference_True()
        {
            // Arrange
            Photo photo1 = new Photo();
            Photo photo2 = photo1;
                
            // Act
            // Assert
            Assert.IsTrue(photo1.Equals(photo2));
            Assert.AreEqual(photo1, photo2);
            Assert.AreSame(photo1, photo2);
        }
        [TestMethod]
        public void Equals_TheSameValue_True()
        {
            // Arrange
            Photo photo1 = new Photo { Path = "photo name" };
            Photo photo2 = new Photo { Path = "photo name" };

            // Act
            // Assert
            Assert.IsTrue(photo1.Equals(photo2));
            Assert.AreEqual(photo1, photo2);
            Assert.AreNotSame(photo1, photo2);
        }
        [TestMethod]
        public void Equals_DifferentValue_False()
        {
            // Arrange
            Photo photo1 = new Photo { Path = "photo name 1" };
            Photo photo2 = new Photo { Path = "photo name 2" };

            // Act
            // Assert
            Assert.IsFalse(photo1.Equals(photo2));
            Assert.AreNotEqual(photo1, photo2);
            Assert.AreNotSame(photo1, photo2);
        }
        [TestMethod]
        public void Equals_SameUser_True()
        {
            // Arrange
            User user1 = new User { NickName = "User" };
            User user2 = new User { NickName = "User" };

            Photo photo1 = new Photo { Path = "photo name", User = user1 };
            Photo photo2 = new Photo { Path = "photo name", User = user2 };

            // Act
            // Assert
            Assert.IsTrue(photo1.Equals(photo2));
            Assert.AreEqual(photo1, photo2);
            Assert.AreNotSame(photo1, photo2);
        }
        [TestMethod]
        public void Equals_DifferentUser_False()
        {
            // Arrange
            User user1 = new User { NickName = "User 1" };
            User user2 = new User { NickName = "User 2" };

            Photo photo1 = new Photo { Path = "photo name", User = user1 };
            Photo photo2 = new Photo { Path = "photo name", User = user2 };

            // Act
            // Assert
            Assert.IsFalse(photo1.Equals(photo2));
            Assert.AreNotEqual(photo1, photo2);
            Assert.AreNotSame(photo1, photo2);
        }
        [TestMethod]
        public void Equals_SameLikes_True()
        {
            // Arrange
            PhotoLike[] photoLikes1 = new PhotoLike[]
            {
                new PhotoLike { IsLiked = true },
                new PhotoLike { IsLiked = true },
                new PhotoLike { IsLiked = true },
                new PhotoLike { IsLiked = true }
            };
            PhotoLike[] photoLikes2 = new PhotoLike[]
            {
                new PhotoLike { IsLiked = true },
                new PhotoLike { IsLiked = true },
                new PhotoLike { IsLiked = true },
                new PhotoLike { IsLiked = true }
            };

            Photo photo1 = new Photo { Path = "photo name", Likes = photoLikes1 };
            Photo photo2 = new Photo { Path = "photo name", Likes = photoLikes2 };

            // Act
            // Assert
            Assert.IsTrue(photo1.Equals(photo2));
            Assert.AreEqual(photo1, photo2);
            Assert.AreNotSame(photo1, photo2);
        }
        [TestMethod]
        public void Equals_DifferentLikes_False()
        {
            // Arrange
            PhotoLike[] photoLikes1 = new PhotoLike[]
            {
                new PhotoLike { IsLiked = true },
                new PhotoLike { IsLiked = true },
                new PhotoLike { IsLiked = true },
                new PhotoLike { IsLiked = true }
            };
            PhotoLike[] photoLikes2 = new PhotoLike[]
            {
                new PhotoLike { IsLiked = true },
                new PhotoLike { IsLiked = false },
                new PhotoLike { IsLiked = false },
                new PhotoLike { IsLiked = true }
            };

            Photo photo1 = new Photo { Path = "photo name", Likes = photoLikes1 };
            Photo photo2 = new Photo { Path = "photo name", Likes = photoLikes2 };

            // Act
            // Assert
            Assert.IsFalse(photo1.Equals(photo2));
            Assert.AreNotEqual(photo1, photo2);
            Assert.AreNotSame(photo1, photo2);
        }
        [TestMethod]
        public void Equals_SecondCommentsNull_False()
        {
            // Arrange
            Comment[] comments1 = new Comment[]
            {
                new Comment { Text = "Comment" },
                new Comment { Text = "Comment" },
                new Comment { Text = "Comment" }
            };
            Comment[] comments2 = null;

            Photo photo1 = new Photo { Path = "photo name", Comments = comments1 };
            Photo photo2 = new Photo { Path = "photo name", Comments = comments2 };

            // Act
            // Assert
            Assert.IsFalse(photo1.Equals(photo2));
            Assert.AreNotEqual(photo1, photo2);
            Assert.AreNotSame(photo1, photo2);
        }
        [TestMethod]
        public void Equals_FirstCommentsNull_False()
        {
            // Arrange
            Comment[] comments1 = null;
            Comment[] comments2 = new Comment[]
            {
                new Comment { Text = "Comment" },
                new Comment { Text = "Comment" },
                new Comment { Text = "Comment" }
            };

            Photo photo1 = new Photo { Path = "photo name", Comments = comments1 };
            Photo photo2 = new Photo { Path = "photo name", Comments = comments2 };

            // Act
            // Assert
            Assert.IsFalse(photo1.Equals(photo2));
            Assert.AreNotEqual(photo1, photo2);
            Assert.AreNotSame(photo1, photo2);
        }
        #endregion
    }
}
