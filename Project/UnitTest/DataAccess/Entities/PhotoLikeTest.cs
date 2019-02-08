using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess.Entities;
using DA = DataAccess.Context;

namespace UnitTest.DataAccess.Entities
{
    [TestClass]
    public class PhotoLikeTest
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
        public void GetAllColumnsOfPhotoLike()
        {
            // Arrange
            PhotoLike expectedPhotoLike = new PhotoLike
            {
                IsLiked = true,
                User = dbContext.Users.First(),
                Photo = dbContext.Photos.First()
            };

            // Act
            dbContext.PhotoLike.Add(expectedPhotoLike);
            dbContext.SaveChanges();
            PhotoLike actualPhotoLike = dbContext.PhotoLike.Find(expectedPhotoLike.Id);

            // Assert
            Assert.AreEqual(expectedPhotoLike.IsLiked, actualPhotoLike.IsLiked);
            Assert.AreEqual(expectedPhotoLike.User, actualPhotoLike.User);
            Assert.AreEqual(expectedPhotoLike.Photo, actualPhotoLike.Photo);
        }
    }
}
