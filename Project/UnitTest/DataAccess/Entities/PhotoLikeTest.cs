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

        // EQUAL
        #region equal
        [TestMethod]
        public void Equals_NullValue_Exception()
        {
            // Arrange
            PhotoLike photoLike1 = new PhotoLike();
            PhotoLike photoLike2 = null;

            // Act
            // Assert
            Assert.ThrowsException<System.ArgumentNullException>(() => photoLike1.Equals(photoLike2));
        }
        [TestMethod]
        public void Equals_DifferentType_False()
        {
            // Arrange
            PhotoLike photoLike = new PhotoLike();
            Subject subject = new Subject();

            // Act
            // Assert
            Assert.IsFalse(photoLike.Equals(subject));
            Assert.AreNotEqual(photoLike, subject);
            Assert.AreNotSame(photoLike, subject);
        }
        [TestMethod]
        public void Equals_TheSameInstance_True()
        {
            // Arrange
            PhotoLike photoLike = new PhotoLike();
            
            // Act
            // Assert
            Assert.IsTrue(photoLike.Equals(photoLike));
            Assert.AreEqual(photoLike, photoLike);
            Assert.AreSame(photoLike, photoLike);
        }
        [TestMethod]
        public void Equals_TheSameReference_True()
        {
            // Arrange
            PhotoLike photoLike1 = new PhotoLike();
            PhotoLike photoLike2 = photoLike1;

            // Act
            // Assert
            Assert.IsTrue(photoLike1.Equals(photoLike2));
            Assert.AreEqual(photoLike1, photoLike2);
            Assert.AreSame(photoLike1, photoLike2);
        }
        [TestMethod]
        public void Equals_TheSameValue_True()
        {
            // Arrange
            PhotoLike photoLike1 = new PhotoLike() { IsLiked = true };
            PhotoLike photoLike2 = new PhotoLike() { IsLiked = true };

            // Act
            // Assert
            Assert.IsTrue(photoLike1.Equals(photoLike2));
            Assert.AreEqual(photoLike1, photoLike2);
            Assert.AreNotSame(photoLike1, photoLike2);
        }
        [TestMethod]
        public void Equals_DifferentValue_False()
        {
            // Arrange
            PhotoLike photoLike1 = new PhotoLike() { IsLiked = true };
            PhotoLike photoLike2 = new PhotoLike() { IsLiked = false };

            // Act
            // Assert
            Assert.IsFalse(photoLike1.Equals(photoLike2));
            Assert.AreNotEqual(photoLike1, photoLike2);
            Assert.AreNotSame(photoLike1, photoLike2);
        }
        [TestMethod]
        public void Equals_SameUser_True()
        {
            // Arrange
            User user1 = new User { NickName = "User" };
            User user2 = new User { NickName = "User" };

            PhotoLike photoLike1 = new PhotoLike() { IsLiked = true, User = user1 };
            PhotoLike photoLike2 = new PhotoLike() { IsLiked = true, User = user2 };

            // Act
            // Assert
            Assert.IsTrue(photoLike1.Equals(photoLike2));
            Assert.AreEqual(photoLike1, photoLike2);
            Assert.AreNotSame(photoLike1, photoLike2);
        }
        [TestMethod]
        public void Equals_DifferentUser_False()
        {
            // Arrange
            User user1 = new User { NickName = "User 1" };
            User user2 = new User { NickName = "User 2" };

            PhotoLike photoLike1 = new PhotoLike() { IsLiked = true, User = user1 };
            PhotoLike photoLike2 = new PhotoLike() { IsLiked = true, User = user2 };

            // Act
            // Assert
            Assert.IsFalse(photoLike1.Equals(photoLike2));
            Assert.AreNotEqual(photoLike1, photoLike2);
            Assert.AreNotSame(photoLike1, photoLike2);
        }
        [TestMethod]
        public void Equals_SamePhoto_True()
        {
            // Arrange
            Photo photo1 = new Photo { Path = "photo name" };
            Photo photo2 = new Photo { Path = "photo name" };

            PhotoLike photoLike1 = new PhotoLike() { IsLiked = true, Photo = photo1 };
            PhotoLike photoLike2 = new PhotoLike() { IsLiked = true, Photo = photo2 };

            // Act
            // Assert
            Assert.IsTrue(photoLike1.Equals(photoLike2));
            Assert.AreEqual(photoLike1, photoLike2);
            Assert.AreNotSame(photoLike1, photoLike2);
        }
        [TestMethod]
        public void Equals_DifferentLikes_False()
        {
            // Arrange
            Photo photo1 = new Photo { Path = "photo name 1" };
            Photo photo2 = new Photo { Path = "photo name 2" };

            PhotoLike photoLike1 = new PhotoLike() { IsLiked = true, Photo = photo1 };
            PhotoLike photoLike2 = new PhotoLike() { IsLiked = true, Photo = photo2 };

            // Act
            // Assert
            Assert.IsFalse(photoLike1.Equals(photoLike2));
            Assert.AreNotEqual(photoLike1, photoLike2);
            Assert.AreNotSame(photoLike1, photoLike2);
        }
        [TestMethod]
        public void Equals_FirstPhotoNull_False()
        {
            // Arrange
            Photo photo1 = null;
            Photo photo2 = new Photo { Path = "photo name" };

            PhotoLike photoLike1 = new PhotoLike() { IsLiked = true, Photo = photo1 };
            PhotoLike photoLike2 = new PhotoLike() { IsLiked = true, Photo = photo2 };

            // Act
            // Assert
            Assert.IsFalse(photoLike1.Equals(photoLike2));
            Assert.AreNotEqual(photoLike1, photoLike2);
            Assert.AreNotSame(photoLike1, photoLike2);
        }
        [TestMethod]
        public void Equals_SecondPhotoNull_False()
        {
            // Arrange
            Photo photo1 = new Photo { Path = "photo name" };
            Photo photo2 = null;

            PhotoLike photoLike1 = new PhotoLike() { IsLiked = true, Photo = photo1 };
            PhotoLike photoLike2 = new PhotoLike() { IsLiked = true, Photo = photo2 };

            // Act
            // Assert
            Assert.IsFalse(photoLike1.Equals(photoLike2));
            Assert.AreNotEqual(photoLike1, photoLike2);
            Assert.AreNotSame(photoLike1, photoLike2);
        }
        #endregion
    }
}
