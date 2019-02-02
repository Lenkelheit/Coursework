using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess.Entities;
using DA = DataAccess.Context;

namespace UnitTest.DataAccess.Entities
{
    [TestClass]
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
            dbFiller = new Resources.Classes.DbFiller();
            dbContext = Resources.Initializers.DatabaseInitializer.dbContext;
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
    }
}
