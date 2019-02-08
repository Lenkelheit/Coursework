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
    }
}
