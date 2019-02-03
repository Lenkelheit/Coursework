using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess.Entities;
using DA = DataAccess.Context;

namespace UnitTest.DataAccess.Entities
{
    [TestClass]
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
    }
}
