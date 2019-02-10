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
    public class CommentLikeRepositoryTest
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
        // COUNT
        #region COUNT
        [TestMethod]
        public void Count()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            int expectedCommentLikesInDb = Resources.Classes.DbFiller.Instance.CommentLikeAmount;

            // Act
            int actualCommentLikesInDb = commentLikeRepository.Count();

            // Assert
            Assert.AreEqual(expectedCommentLikesInDb, actualCommentLikesInDb);
        }
        [TestMethod]
        public void CountIf_IsLikedTrue()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            int expectedCommentLikesWithIsLikedTrue = Resources.Classes.DbFiller.Instance.CommentLikeAmountWithLike;

            // Act
            int actualCommentLikesWithIsLikedTrue = commentLikeRepository.Count(commentLike => commentLike.IsLiked == true);

            // Assert
            Assert.AreEqual(expectedCommentLikesWithIsLikedTrue, actualCommentLikesWithIsLikedTrue);
        }
        #endregion
        // GET
        #region GET
        [TestMethod]
        public void GetAll()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            int expectedCommentLikeInDb = Resources.Classes.DbFiller.Instance.CommentLikeAmount;

            // Act
            CommentLike[] commentLikeFromDb = commentLikeRepository.Get().ToArray();
            int actualCommentLikeInDb = commentLikeFromDb.Length;

            // Assert
            Assert.AreEqual(expectedCommentLikeInDb, actualCommentLikeInDb);
            CollectionAssert.AreEquivalent(dbContext.CommentLike.ToArray(), commentLikeFromDb);
        }
        [TestMethod]
        public void GetFilterByIsLiked()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            int expectedCommentLikeWithLikeInDb = Resources.Classes.DbFiller.Instance.CommentLikeAmountWithLike;

            // Act
            CommentLike[] commentLikeFromDb = commentLikeRepository.Get(filter: commentLike => commentLike.IsLiked == true).ToArray();
            int actualCommentLikeWithLikeInDb = commentLikeFromDb.Length;

            // Assertz
            Assert.AreEqual(expectedCommentLikeWithLikeInDb, actualCommentLikeWithLikeInDb);
            CollectionAssert.IsSubsetOf(commentLikeFromDb, dbContext.CommentLike.ToArray());
        }
        [TestMethod]
        public void GetOrder()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            int expectedCommentLikeInDb = Resources.Classes.DbFiller.Instance.CommentLikeAmount;

            // Act
            CommentLike[] commentLikeFromDb = commentLikeRepository.Get(orderBy: commentLike => commentLike.OrderBy(cl => cl.IsLiked)).ToArray();
            int actualCommentLikeInDb = commentLikeFromDb.Length;

            // Assert
            Assert.AreEqual(expectedCommentLikeInDb, actualCommentLikeInDb);
            CollectionAssert.AreEqual(dbContext.CommentLike.OrderBy(cl => cl.IsLiked).ToArray(), commentLikeFromDb);
        }
        [TestMethod]
        public void GetFilterAndOrder()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            int expectedCommentLikeWithLikeInDb = Resources.Classes.DbFiller.Instance.CommentLikeAmountWithLike;
            CommentLike[] likesInDb = dbContext.CommentLike.Where(cl => cl.IsLiked == true).OrderByDescending(cl => cl.User.NickName).ToArray();

            // Act
            CommentLike[] commentLikeFromDb = commentLikeRepository
                .Get(filter: cl => cl.IsLiked == true, orderBy: o => o.OrderByDescending(cl => cl.User.NickName)).ToArray();
            int actualCommentLikeWithLikeInDb = commentLikeFromDb.Count();

            // Assert
            Assert.AreEqual(expectedCommentLikeWithLikeInDb, actualCommentLikeWithLikeInDb);
            CollectionAssert.AreEqual(likesInDb, commentLikeFromDb);
        }
        #endregion
        // GET BY ID
        #region GET BY ID
        [TestMethod]
        public void GetById()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            Guid idToSearch = dbContext.CommentLike.First().Id;
            CommentLike expectedCommentLike = dbContext.CommentLike.Find(idToSearch);

            // Act
            CommentLike commentLikeFromDb = commentLikeRepository.Get(idToSearch);

            // Assert
            Assert.AreEqual(expectedCommentLike, commentLikeFromDb);
        }
        [TestMethod]
        public void GetByWrongId_Null()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            Guid wrongId = default(Guid);
            CommentLike expectedCommentLikeFromDb = null;

            // Act
            CommentLike actualCommentLikeFromDb = commentLikeRepository.Get(wrongId);

            // Assert
            Assert.AreEqual(expectedCommentLikeFromDb, actualCommentLikeFromDb);
        }
        #endregion
        // INSERT
        #region INSERT
        [TestMethod]
        public void AddCommentLike()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            CommentLike commentLike = new CommentLike
            {
                User = dbContext.Users.First(),
                Comment = dbContext.Comments.First(),
                IsLiked = true
            };

            // Act
            commentLikeRepository.Insert(commentLike);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.CommentLike.ToList(), commentLike);
        }
        #endregion
        // DELETE BY KEY
        #region DELETE BY KEY
        [TestMethod]
        public void DeleteByKey()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            CommentLike expectedDeletedCommentLike = dbContext.CommentLike.First();
            Guid idToDelete = expectedDeletedCommentLike.Id;

            // Act
            commentLikeRepository.Delete(idToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.CommentLike.ToArray(), expectedDeletedCommentLike);
        }
        [TestMethod]
        public void DeleteByWrongKey_Exception()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            Guid wrongId = default(Guid);

            // Act
            // Assert
            Assert.ThrowsException<InvalidOperationException>(() => commentLikeRepository.Delete(wrongId));
        }
        [TestMethod]
        public void DeleteByNullKey_Exception()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            object wrongId = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => commentLikeRepository.Delete(wrongId));
        }
        #endregion
        // DELETE BY VALUE
        #region DELETE BY VALUE
        [TestMethod]
        public void DeleteByValue()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            CommentLike commentLikeToDelete = dbContext.CommentLike.First();

            // Act
            commentLikeRepository.Delete(commentLikeToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.CommentLike.ToArray(), commentLikeToDelete);
        }
        [TestMethod]
        public void DeleteByNullValue()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => commentLikeRepository.Delete(entityToDelete: null));
        }
        [TestMethod]
        public void DeleteByChangedValue()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            CommentLike changedCommentLikeToDelete = dbContext.CommentLike.First();
            changedCommentLikeToDelete.IsLiked = false;

            // Act
            commentLikeRepository.Delete(entityToDelete: changedCommentLikeToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.CommentLike.ToArray(), changedCommentLikeToDelete);
        }
        #endregion
        // UPDATE
        #region UPDATE
        [TestMethod]
        public void Update()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            CommentLike commentLikeToUpdate = dbContext.CommentLike.First();
            bool isLiked = false;

            // Act
            commentLikeToUpdate.IsLiked = isLiked;
            commentLikeRepository.Update(commentLikeToUpdate);
            dbContext.SaveChanges();

            // Assert
            Assert.AreEqual(dbContext.CommentLike.Find(commentLikeToUpdate.Id).IsLiked, isLiked);
        }
        #endregion
        // TRY GET USER LIKE
        #region TRY GET USER LIKE
        [TestMethod]
        public void TryGetUserLike()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            CommentLike expectedCommentLikeFromDb = dbContext.CommentLike.First();

            // Act
            CommentLike actualCommentLikeFromDb = commentLikeRepository.TryGetUserLike(expectedCommentLikeFromDb.Comment, expectedCommentLikeFromDb.User);

            // Assert
            Assert.AreEqual(expectedCommentLikeFromDb, actualCommentLikeFromDb);
        }
        [TestMethod]
        public void TryGetUserLike_Null()
        {
            // Arrange
            CommentLikeRepository commentLikeRepository = new CommentLikeRepository(dbContext);
            CommentLike commentLikeFromDb = dbContext.CommentLike.First();
            CommentLike expectedCommentLikeFromDb = null;
            string userNicknameWithouLike = Resources.Classes.DbFiller.Instance.UserWithoutCommentLike;
            User user = dbContext.Users.First(u => u.NickName == userNicknameWithouLike);

            // Act
            CommentLike actualCommentLikeFromDb = commentLikeRepository.TryGetUserLike(commentLikeFromDb.Comment, user);

            // Assert
            Assert.AreEqual(expectedCommentLikeFromDb, actualCommentLikeFromDb);
        }
        #endregion
    }
}
