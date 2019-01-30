using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess.Entities;
using DataAccess.Repositories;
using DA = DataAccess.Context;

namespace UnitTest.DataAccess.Repositories
{
    [TestClass]
    public class PhotoLikeRepositoryTest
    {
        // FIELDS
        static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Integrated Security=True; Initial Catalog=PhotoLikeRepositoryTestDB";
        static DA.AppContext dbContext;
        static Resources.Classes.DbFiller dbFiller;
        // PROPERTIES
        public TestContext TestContext { get; set; }
        // INITIALIZERS
        [ClassInitialize]
        public static void Constructor(TestContext context)
        {
            dbFiller = new Resources.Classes.DbFiller();
            dbContext = new DA.AppContext(connectionString);
        }
        [ClassCleanup]
        public static void Finalizer()
        {
            dbContext.Dispose();
            System.Data.Entity.Database.Delete(connectionString);
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
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            int expectedPhotoLikeInDb = 27;

            // Act
            int actualPhotoLikeInDb = photoLikeRepository.Count();

            // Assert
            Assert.AreEqual(expectedPhotoLikeInDb, actualPhotoLikeInDb);
        }
        [TestMethod]
        public void CountIfIsLikedTrueIs18()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            int expectedPhotoLikeWithIsLikedTrue = 18;

            // Act
            int actualPhotoLikeWithIsLikedTrue = photoLikeRepository.Count(photoLike => photoLike.IsLiked == true);

            // Assert
            Assert.AreEqual(expectedPhotoLikeWithIsLikedTrue, actualPhotoLikeWithIsLikedTrue);
        }
        #endregion
        // GET
        #region GET
        [TestMethod]
        public void GetAll()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            int expectedPhotoLikeInDb = 27;

            // Act
            IEnumerable<PhotoLike> photoLikeFromDb = photoLikeRepository.Get();
            int actualPhotoLikeInDb = photoLikeFromDb.Count();

            // Assert
            Assert.AreEqual(expectedPhotoLikeInDb, actualPhotoLikeInDb);
            CollectionAssert.AreEquivalent(dbContext.PhotoLike.ToArray(), photoLikeFromDb.ToArray());
        }
        [TestMethod]
        public void GetFilterByIsLiked()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            int expectedPhotoLikeInDb = 18;

            // Act
            IEnumerable<PhotoLike> photoLikeFromDb = photoLikeRepository.Get(filter: photoLike => photoLike.IsLiked == true);
            int actualPhotoLikeInDb = photoLikeFromDb.Count();

            // Assertz
            Assert.AreEqual(expectedPhotoLikeInDb, actualPhotoLikeInDb);
            CollectionAssert.IsSubsetOf(photoLikeFromDb.ToArray(), dbContext.PhotoLike.ToArray());
        }
        [TestMethod]
        public void GetOrder()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            int expectedPhotoLikeInDb = 27;

            // Act
            IEnumerable<PhotoLike> photoLikeFromDb = photoLikeRepository.Get(orderBy: photoLike => photoLike.OrderBy(pl => pl.IsLiked));
            int actualPhotoLikeInDb = photoLikeFromDb.Count();

            // Assert
            Assert.AreEqual(expectedPhotoLikeInDb, actualPhotoLikeInDb);
            CollectionAssert.AreEqual(dbContext.PhotoLike.OrderBy(pl => pl.IsLiked).ToArray(), photoLikeFromDb.ToArray());
        }
        [TestMethod]
        public void GetFilterAndOrder()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            int expectedPhotoLikeInDb = 18;

            // Act
            IEnumerable<PhotoLike> photoLikeFromDb = photoLikeRepository
                .Get(filter: pl => pl.IsLiked == true, orderBy: o => o.OrderByDescending(pl => pl.User.NickName));
            int actualPhotoLikeInDb = photoLikeFromDb.Count();

            // Assert
            Assert.AreEqual(expectedPhotoLikeInDb, actualPhotoLikeInDb);
            CollectionAssert.AreEqual(dbContext.PhotoLike.Where(pl => pl.IsLiked == true)
                .OrderByDescending(pl => pl.User.NickName).ToArray(), photoLikeFromDb.ToArray());
        }
        #endregion
        // GET BY ID
        #region GET BY ID
        [TestMethod]
        public void GetById()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            Guid idToSearch = dbContext.PhotoLike.First().Id;
            PhotoLike expectedPhotoLike = dbContext.PhotoLike.Find(idToSearch);

            // Act
            PhotoLike photoLikeFromDb = photoLikeRepository.Get(idToSearch);

            // Assert
            Assert.AreEqual(expectedPhotoLike, photoLikeFromDb);
        }
        [TestMethod]
        public void GetByWrongId_Null()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            Guid wrongId = default(Guid);
            PhotoLike expectedPhotoLikeFromDb = null;

            // Act
            PhotoLike actualPhotoLikeFromDb = photoLikeRepository.Get(wrongId);

            // Assert
            Assert.AreEqual(expectedPhotoLikeFromDb, actualPhotoLikeFromDb);
        }
        #endregion
        // INSERT
        #region INSERT
        [TestMethod]
        public void AddPhotoLike()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            PhotoLike photoLike = new PhotoLike
            {
                User = dbContext.Users.First(),
                Photo = dbContext.Photos.First(),
                IsLiked = true
            };

            // Act
            photoLikeRepository.Insert(photoLike);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.PhotoLike.ToList(), photoLike);
        }
        #endregion
        // DELETE BY KEY
        #region DELETE BY KEY
        [TestMethod]
        public void DeleteByKey()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            PhotoLike expectedDeletedPhotoLike = dbContext.PhotoLike.First();
            Guid idToDelete = expectedDeletedPhotoLike.Id;

            // Act
            photoLikeRepository.Delete(idToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.PhotoLike.ToArray(), expectedDeletedPhotoLike);
        }
        [TestMethod]
        public void DeleteByWrongKey_Exception()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            Guid wrongId = default(Guid);

            // Act
            // Assert
            Assert.ThrowsException<InvalidOperationException>(() => photoLikeRepository.Delete(wrongId));
        }
        [TestMethod]
        public void DeleteByNullKey_Exception()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            object wrongId = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => photoLikeRepository.Delete(wrongId));
        }
        #endregion
        // DELETE BY VALUE
        #region DELETE BY VALUE
        [TestMethod]
        public void DeleteByValue()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            PhotoLike photoLikeToDelete = dbContext.PhotoLike.First();

            // Act
            photoLikeRepository.Delete(photoLikeToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.PhotoLike.ToArray(), photoLikeToDelete);
        }
        [TestMethod]
        public void DeleteByNullValue()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => photoLikeRepository.Delete(entityToDelete: null));
        }
        [TestMethod]
        public void DeleteByChangedValue()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            PhotoLike changedPhotoLikeToDelete = dbContext.PhotoLike.First();
            changedPhotoLikeToDelete.IsLiked = false;

            // Act
            photoLikeRepository.Delete(entityToDelete: changedPhotoLikeToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.PhotoLike.ToArray(), changedPhotoLikeToDelete);
        }
        #endregion
        // UPDATE
        #region UPDATE
        [TestMethod]
        public void Update()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            PhotoLike photoLikeToUpdate = dbContext.PhotoLike.First();
            bool isLiked = false;

            // Act
            photoLikeToUpdate.IsLiked = isLiked;
            photoLikeRepository.Update(photoLikeToUpdate);

            // Assert
            Assert.AreEqual(dbContext.PhotoLike.Find(photoLikeToUpdate.Id).IsLiked, isLiked);
        }
        #endregion
        // TRY GET USER LIKE
        #region TRY GET USER LIKE
        [TestMethod]
        public void TryGetUserLike()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            PhotoLike expectedPhotoLikeFromDb = dbContext.PhotoLike.First();

            // Act
            PhotoLike actualPhotoLikeFromDb = photoLikeRepository.TryGetUserLike(expectedPhotoLikeFromDb.Photo, expectedPhotoLikeFromDb.User);

            // Assert
            Assert.AreEqual(expectedPhotoLikeFromDb, actualPhotoLikeFromDb);
        }
        [TestMethod]
        public void TryGetUserLike_Null()
        {
            // Arrange
            PhotoLikeRepository photoLikeRepository = new PhotoLikeRepository(dbContext);
            PhotoLike photoLikeFromDb = dbContext.PhotoLike.First();
            PhotoLike expectedPhotoLikeFromDb = null;
            User user = dbContext.Users.Where(u => u.NickName == "Harold").First();

            // Act
            PhotoLike actualPhotoLikeFromDb = photoLikeRepository.TryGetUserLike(photoLikeFromDb.Photo, user);

            // Assert
            Assert.AreEqual(expectedPhotoLikeFromDb, actualPhotoLikeFromDb);
        }
        #endregion
    }
}
