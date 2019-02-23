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
    public class UserTest
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
        public void GetAllColumnsOfUser()
        {
            // Arrange
            User firstUser = dbContext.Users.First();
            User secondUser = dbContext.Users.ToArray()[1];
            Photo photo = dbContext.Photos.First();
            User expectedUser = new User
            {
                MainPhotoPath = "Test main photo path.jpg",
                NickName = "Test nickname",
                Password = "1111",
                IsAdmin = false,
                Photos = new List<Photo>
                {
                    new Photo
                    {
                        Path = "Some test path.jpg",
                        Likes = new List<PhotoLike>
                        {
                            new PhotoLike{ IsLiked = true, User = firstUser },
                            new PhotoLike{ IsLiked = false, User = secondUser }
                        },
                        Comments = new List<Comment>
                        {
                            new Comment
                            {
                                Date = DateTime.Now,
                                Text = "Some test text",
                                User = firstUser,
                                Likes = new List<CommentLike>
                                {
                                    new CommentLike{ IsLiked = true, User = firstUser },
                                    new CommentLike{ IsLiked = false, User = secondUser }
                                }
                            }
                        }
                    }
                },
                Followers = new List<User> { firstUser, secondUser },
                Following = new List<User> { firstUser, secondUser },
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Date = DateTime.Now,
                        Text = "Some test text",
                        Photo = photo,
                        Likes = new List<CommentLike>
                        {
                            new CommentLike{ IsLiked = true, User = firstUser },
                            new CommentLike{ IsLiked = false, User = secondUser }
                        }
                    }
                },
                PhotoLikes = new List<PhotoLike>
                {
                    new PhotoLike
                    {
                        IsLiked = true,
                        Photo = photo
                    }
                },
                CommentLikes = new List<CommentLike>
                {
                    new CommentLike
                    {
                        IsLiked = true,
                        Comment = dbContext.Comments.First()
                    }
                },
                Messages = new List<Message>
                {
                    new Message
                    {
                        Date = DateTime.Now,
                        Text = "Some test text",
                        Subject = dbContext.Subjects.First()
                    }
                }
            };

            // Act
            dbContext.Users.Add(expectedUser);
            dbContext.SaveChanges();
            User actualUser = dbContext.Users.Find(expectedUser.Id);

            // Assert
            Assert.AreEqual(expectedUser.MainPhotoPath, actualUser.MainPhotoPath);
            Assert.AreEqual(expectedUser.NickName, actualUser.NickName);
            Assert.AreEqual(expectedUser.Password, actualUser.Password);
            Assert.AreEqual(expectedUser.IsAdmin, actualUser.IsAdmin);
            CollectionAssert.AreEqual(expectedUser.Photos.ToArray(), actualUser.Photos.ToArray());
            CollectionAssert.AreEqual(expectedUser.Followers.ToArray(), actualUser.Followers.ToArray());
            CollectionAssert.AreEqual(expectedUser.Following.ToArray(), actualUser.Following.ToArray());
            CollectionAssert.AreEqual(expectedUser.Comments.ToArray(), actualUser.Comments.ToArray());
            CollectionAssert.AreEqual(expectedUser.PhotoLikes.ToArray(), actualUser.PhotoLikes.ToArray());
            CollectionAssert.AreEqual(expectedUser.CommentLikes.ToArray(), actualUser.CommentLikes.ToArray());
            CollectionAssert.AreEqual(expectedUser.Messages.ToArray(), actualUser.Messages.ToArray());
        }


        // EQUAL
        #region equal
        [TestMethod]
        public void Equals_NullValue_Exception()
        {
            // Arrange
            User user1 = new User();
            User user2 = null;

            // Act
            // Assert
            Assert.ThrowsException<System.ArgumentNullException>(() => user1.Equals(user2));
        }
        [TestMethod]
        public void Equals_DifferentType_False()
        {
            // Arrange
            User user = new User();
            PhotoLike photoLike = new PhotoLike();

            // Act
            // Assert
            Assert.IsFalse(user.Equals(photoLike));
            Assert.AreNotEqual(user, photoLike);
            Assert.AreNotSame(user,photoLike);
        }
        [TestMethod]
        public void Equals_TheSameInstance_True()
        {
            // Arrange
            User user = new User();

            // Act
            // Assert
            Assert.IsTrue(user.Equals(user));
            Assert.AreEqual(user, user);
            Assert.AreSame(user, user);
        }
        [TestMethod]
        public void Equals_TheSameReference_True()
        {
            // Arrange
            User user1 = new User();
            User user2 = user1;

            // Act
            // Assert
            Assert.IsTrue(user1.Equals(user2));
            Assert.AreEqual(user1, user2);
            Assert.AreSame(user1, user2);
        }
        [TestMethod]
        public void Equals_TheSameValue_True()
        {
            // Arrange
            User user1 = new User { NickName = "User" };
            User user2 = new User { NickName = "User" };

            // Act
            // Assert
            Assert.IsTrue(user1.Equals(user2));
            Assert.AreEqual(user1, user2);
            Assert.AreNotSame(user1, user2);
        }
        [TestMethod]
        public void Equals_DifferentValue_False()
        {
            // Arrange
            User user1 = new User { NickName = "User 1" };
            User user2 = new User { NickName = "User 2" };

            // Act
            // Assert
            Assert.IsFalse(user1.Equals(user2));
            Assert.AreNotEqual(user1, user2);
            Assert.AreNotSame(user1, user2);
        }
        #endregion
    }
}
