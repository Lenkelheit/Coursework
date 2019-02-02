using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess.Structs;
using DataAccess.Entities;
using DataAccess.Repositories;
using DA = DataAccess.Context;
using System.Data.Entity.Infrastructure;

namespace UnitTest.DataAccess.Repositories
{   
    [TestClass]
    public class PhotoRepositoryTest
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
        // COUNT
        #region COUNT
        [TestMethod]
        public void Count()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            int expectedPhotosInDb = 13;

            // Act
            int actualPhotosInDb = photoRepository.Count();

            // Assert
            Assert.AreEqual(expectedPhotosInDb, actualPhotosInDb);
        }
        [TestMethod]
        public void CountIfUserJohn()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            int expectedPhotosUserJohn = 3;

            // Act
            int actualPhotosUserJohn = photoRepository.Count(photo => photo.User.NickName == "John");

            // Assert
            Assert.AreEqual(expectedPhotosUserJohn, actualPhotosUserJohn);
        }
        #endregion
        // GET
        #region GET
        [TestMethod]
        public void GetAll()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            int expectedPhotoInDb = 13;

            // Act
            IEnumerable<Photo> photosFromDb = photoRepository.Get();
            int actualPhotoInDb = photosFromDb.Count();

            // Assert
            Assert.AreEqual(expectedPhotoInDb, actualPhotoInDb);
            CollectionAssert.AreEquivalent(dbContext.Photos.ToArray(), photosFromDb.ToArray());
        }
        [TestMethod]
        public void GetFilterByUserNickName()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            int expectedPhotoInDb = 3;

            // Act
            IEnumerable<Photo> photosFromDb = photoRepository.Get(filter: photo => photo.User.NickName == "John");
            int actualPhotoInDb = photosFromDb.Count();

            // Assert
            Assert.AreEqual(expectedPhotoInDb, actualPhotoInDb);
            CollectionAssert.IsSubsetOf(photosFromDb.ToArray(), dbContext.Photos.ToArray());
        }
        [TestMethod]
        public void GetOrder()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            int expectedPhotoInDb = 13;

            // Act
            IEnumerable<Photo> photosFromDb = photoRepository.Get(orderBy: photo => photo.OrderBy(p => p.Path));
            int actualPhotoInDb = photosFromDb.Count();

            // Assert
            Assert.AreEqual(expectedPhotoInDb, actualPhotoInDb);
            CollectionAssert.AreEqual(dbContext.Photos.OrderBy(p => p.Path).ToArray(), photosFromDb.ToArray());
        }
        [TestMethod]
        public void GetFilterAndOrder()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            int expectedPhotoInDb = 3;

            // Act
            IEnumerable<Photo> photosFromDb = photoRepository.
                Get(filter: p => p.User.NickName == "John", orderBy: o => o.OrderByDescending(p => p.Path));
            int actualPhotoInDb = photosFromDb.Count();

            // Assert
            Assert.AreEqual(expectedPhotoInDb, actualPhotoInDb);
            CollectionAssert.AreEqual(dbContext.Photos.Where(p => p.User.NickName == "John")
                .OrderByDescending(p => p.Path).ToArray(), photosFromDb.ToArray());
        }
        #endregion
        // GET BY ID
        #region GET BY ID
        [TestMethod]
        public void GetById()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            int idToSearch = 4;
            Photo expectedPhoto = dbContext.Photos.Find(idToSearch);

            // Act
            Photo photosFromDb = photoRepository.Get(idToSearch);

            // Assert
            Assert.AreEqual(expectedPhoto, photosFromDb);
        }
        [TestMethod]
        public void GetByWrongId_Null()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            int wrongId = int.MaxValue;
            Photo expectedPhotoFromDb = null;

            // Act
            Photo actualPhotoFromDb = photoRepository.Get(wrongId);

            // Assert
            Assert.AreEqual(expectedPhotoFromDb, actualPhotoFromDb);
        }
        #endregion
        // INSERT
        #region INSERT
        [TestMethod]
        public void AddPhoto()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            Photo photo = new Photo
            {
                User = dbContext.Users.First(),
                Path = "1/4.jpg"
            };

            // Act
            photoRepository.Insert(photo);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Photos.ToList(), photo);
        }
        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"..\..\Resources\DataAccess\Repositories\WrongPhotoPath.xml",
            tableName: "Photo",
            dataAccessMethod: DataAccessMethod.Random)]
        public void AddWrongPhotoPath_Exception()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            Photo photo = new Photo()
            {
                User = dbContext.Users.First(),
                Path = Convert.ToString(TestContext.DataRow["Path"])
            };

            // Act
            photoRepository.Insert(photo);

            // Assert
            Assert.ThrowsException<System.Data.Entity.Validation.DbEntityValidationException>(() => dbContext.SaveChanges());
            // undo adding
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(photo);
        }
        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"..\..\Resources\DataAccess\Repositories\WrongPhotoExtension.xml",
            tableName: "Photo",
            dataAccessMethod: DataAccessMethod.Random)]
        public void AddWrongPhotoExtension_Exception()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            Photo photo = new Photo()
            {
                User = dbContext.Users.First(),
                Path = Convert.ToString(TestContext.DataRow["Path"])
            };

            // Act
            photoRepository.Insert(photo);

            // Assert
            Assert.ThrowsException<System.Data.Entity.Validation.DbEntityValidationException>(() => dbContext.SaveChanges());
            // undo adding
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(photo);
        }
        #endregion
        // DELETE BY KEY
        #region DELETE BY KEY
        [TestMethod]
        public void DeleteByKey()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            Photo expectedDeletedPhoto = dbContext.Photos.First();
            int idToDelete = expectedDeletedPhoto.Id;

            // Act
            List<CommentLike> deletedCommentLikes = dbContext.CommentLike
                .AsEnumerable().Where(cl => expectedDeletedPhoto.Comments.Contains(cl.Comment)).ToList();
            photoRepository.Delete(idToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Photos.ToArray(), expectedDeletedPhoto);
            // Checks if all photo's likes are deleted.
            Assert.IsFalse(dbContext.PhotoLike.AsEnumerable().Any(pl => pl.Photo == null || pl.Photo.Id == expectedDeletedPhoto.Id));
            // Checks if all photo's comments are deleted.
            Assert.IsFalse(dbContext.Comments.AsEnumerable().Any(c => c.Photo == null || c.Photo.Id == expectedDeletedPhoto.Id));
            // Checks if all photo's comments' likes are deleted.
            CollectionAssert.IsNotSubsetOf(deletedCommentLikes, dbContext.CommentLike.ToList());
        }
        [TestMethod]
        public void DeleteByWrongKey_Exception()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            int wrongId = int.MaxValue;

            // Act
            // Assert
            Assert.ThrowsException<InvalidOperationException>(() => photoRepository.Delete(wrongId));
        }
        [TestMethod]
        public void DeleteByNullKey_Exception()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            object wrongId = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => photoRepository.Delete(wrongId));
        }
        #endregion
        // DELETE BY VALUE
        #region DELETE BY VALUE
        [TestMethod]
        public void DeleteByValue()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            Photo photoToDelete = dbContext.Photos.First();

            // Act
            List<CommentLike> deletedCommentLikes = dbContext.CommentLike
                .AsEnumerable().Where(cl => photoToDelete.Comments.Contains(cl.Comment)).ToList();
            photoRepository.Delete(photoToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Photos.ToArray(), photoToDelete);
            // Checks if all photo's likes are deleted.
            Assert.IsFalse(dbContext.PhotoLike.AsEnumerable().Any(pl => pl.Photo == null || pl.Photo.Id == photoToDelete.Id));
            // Checks if all photo's comments are deleted.
            Assert.IsFalse(dbContext.Comments.AsEnumerable().Any(c => c.Photo == null || c.Photo.Id == photoToDelete.Id));
            // Checks if all photo's comments' likes are deleted.
            CollectionAssert.IsNotSubsetOf(deletedCommentLikes, dbContext.CommentLike.ToList());
        }
        [TestMethod]
        public void DeleteByNullValue()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => photoRepository.Delete(entityToDelete: null));
        }
        [TestMethod]
        public void DeleteByChangedValue()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            Photo changedPhotoToDelete = dbContext.Photos.First();
            changedPhotoToDelete.Path = "Changed it.jpg";

            // Act
            List<CommentLike> deletedCommentLikes = dbContext.CommentLike
                .AsEnumerable().Where(cl => changedPhotoToDelete.Comments.Contains(cl.Comment)).ToList();
            photoRepository.Delete(entityToDelete: changedPhotoToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Photos.ToArray(), changedPhotoToDelete);
            // Checks if all photo's likes are deleted.
            Assert.IsFalse(dbContext.PhotoLike.AsEnumerable().Any(pl => pl.Photo == null || pl.Photo.Id == changedPhotoToDelete.Id));
            // Checks if all photo's comments are deleted.
            Assert.IsFalse(dbContext.Comments.AsEnumerable().Any(c => c.Photo == null || c.Photo.Id == changedPhotoToDelete.Id));
            // Checks if all photo's comments' likes are deleted.
            CollectionAssert.IsNotSubsetOf(deletedCommentLikes, dbContext.CommentLike.ToList());
        }
        #endregion
        // UPDATE
        #region UPDATE
        [TestMethod]
        public void Update()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            Photo photoToUpdate = dbContext.Photos.First();
            string newPath = "new path.jpg";

            // Act
            photoToUpdate.Path = newPath;
            photoRepository.Update(photoToUpdate);

            // Assert
            Assert.AreEqual(dbContext.Photos.Find(photoToUpdate.Id).Path, newPath);
        }
        #endregion
        // GET LIKE DISLIKE AMOUNT
        #region GET LIKE DISLIKE AMOUNT
        [TestMethod]
        public void GetLikeDislikeAmount()
        {
            // Arrange
            PhotoRepository photoRepository = new PhotoRepository(dbContext);
            Photo photo = dbContext.Photos.First();
            int likesCount = photo.Likes.Count(l => l.IsLiked);
            LikeDislikeAmount expectedLikeDislikeAmountToPhotoInDb = new LikeDislikeAmount
            {
                LikesAmount = likesCount,
                DisLikesAmount = photo.Likes.Count - likesCount
            };

            // Act
            LikeDislikeAmount actualLikeDislikeAmountToPhotoInDb = photoRepository.GetLikeDislikeAmount(photo);

            // Assert
            Assert.AreEqual(expectedLikeDislikeAmountToPhotoInDb.LikesAmount, actualLikeDislikeAmountToPhotoInDb.LikesAmount);
            Assert.AreEqual(expectedLikeDislikeAmountToPhotoInDb.DisLikesAmount, actualLikeDislikeAmountToPhotoInDb.DisLikesAmount);
        }
        #endregion
    }
}
