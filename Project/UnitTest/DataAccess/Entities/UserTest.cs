using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess.Entities;
using DA = DataAccess.Context;

namespace UnitTest.DataAccess.Entities
{
    [TestClass]
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
            dbFiller = new Resources.Classes.DbFiller();
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
    }
}
