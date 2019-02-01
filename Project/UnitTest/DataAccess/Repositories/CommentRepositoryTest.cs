﻿using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess.Entities;
using DataAccess.Repositories;
using DA = DataAccess.Context;
using System.Data.Entity.Infrastructure;

namespace UnitTest.DataAccess.Repositories
{
    [TestClass]
    public class CommentRepositoryTest
    {
        // FIELDS
        static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Integrated Security=True; Initial Catalog=CommentRepositoryTestDB";
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
            CommentRepository commentRepository = new CommentRepository(dbContext);
            int expectedCommentsInDb = 30;

            // Act
            int actualCommentsInDb = commentRepository.Count();

            // Assert
            Assert.AreEqual(expectedCommentsInDb, actualCommentsInDb);
        }
        [TestMethod]
        public void CountIfMonthIs7()
        {
            // Arrange
            CommentRepository commentRepository = new CommentRepository(dbContext);
            int expectedCommentsOn7Month = 2;

            // Act
            int actualCommentsOn7Month = commentRepository.Count(comment => comment.Date.Month == 7);

            // Assert
            Assert.AreEqual(expectedCommentsOn7Month, actualCommentsOn7Month);
        }
        #endregion
        // GET
        #region GET
        [TestMethod]
        public void GetAll()
        {
            // Arrange
            CommentRepository commentRepository = new CommentRepository(dbContext);
            int expectedCommentInDb = 30;

            // Act
            IEnumerable<Comment> commentsFromDb = commentRepository.Get();
            int actualCommentInDb = commentsFromDb.Count();

            // Assert
            Assert.AreEqual(expectedCommentInDb, actualCommentInDb);
            CollectionAssert.AreEquivalent(dbContext.Comments.ToArray(), commentsFromDb.ToArray());
        }
        [TestMethod]
        public void GetFilterByYear()
        {
            // Arrange
            CommentRepository commentRepository = new CommentRepository(dbContext);
            int expectedCommentInDb = 6;

            // Act
            IEnumerable<Comment> commentsFromDb = commentRepository.Get(filter: comment => comment.Date.Month > 9);
            int actualCommentInDb = commentsFromDb.Count();

            // Assert
            Assert.AreEqual(expectedCommentInDb, actualCommentInDb);
            CollectionAssert.IsSubsetOf(commentsFromDb.ToArray(), dbContext.Comments.ToArray());
        }
        [TestMethod]
        public void GetOrder()
        {
            // Arrange
            CommentRepository commentRepository = new CommentRepository(dbContext);
            int expectedCommentInDb = 30;

            // Act
            IEnumerable<Comment> commentsFromDb = commentRepository.Get(orderBy: comment => comment.OrderBy(c => c.Date.Month));
            int actualCommentInDb = commentsFromDb.Count();

            // Assert
            Assert.AreEqual(expectedCommentInDb, actualCommentInDb);
            CollectionAssert.AreEqual(dbContext.Comments.OrderBy(c => c.Date.Month).ToArray(), commentsFromDb.ToArray());
        }
        [TestMethod]
        public void GetFilterAndOrder()
        {
            // Arrange
            CommentRepository commentRepository = new CommentRepository(dbContext);
            int expectedCommentInDb = 6;

            // Act
            IEnumerable<Comment> commentsFromDb = commentRepository.Get(filter: c => c.Date.Month > 9, orderBy: o => o.OrderByDescending(c => c.Date.Year));
            int actualCommentInDb = commentsFromDb.Count();

            // Assert
            Assert.AreEqual(expectedCommentInDb, actualCommentInDb);
            CollectionAssert.AreEqual(dbContext.Comments.Where(c => c.Date.Month > 9).OrderByDescending(c => c.Date.Year).ToArray(), commentsFromDb.ToArray());
        }
        #endregion
        // GET BY ID
        #region GET BY ID
        [TestMethod]
        public void GetById()
        {
            // Arrange
            CommentRepository commentRepository = new CommentRepository(dbContext);
            int idToSearch = 4;
            Comment expectedComment = dbContext.Comments.Find(idToSearch);

            // Act
            Comment commentsFromDb = commentRepository.Get(idToSearch);

            // Assert
            Assert.AreEqual(expectedComment, commentsFromDb);
        }
        [TestMethod]
        public void GetByWrongId_Null()
        {
            // Arrange
            CommentRepository commentRepository = new CommentRepository(dbContext);
            int wrongId = int.MaxValue;
            Comment expectedCommentFromDb = null;

            // Act
            Comment actualCommentFromDb = commentRepository.Get(wrongId);

            // Assert
            Assert.AreEqual(expectedCommentFromDb, actualCommentFromDb);
        }
        #endregion
        // INSERT
        #region INSERT
        [TestMethod]
        public void AddComment()
        {
            // Arrange
            CommentRepository commentRepository = new CommentRepository(dbContext);
            Comment comment = new Comment
            {
                Date = System.DateTime.Now,
                User = dbContext.Users.First(),
                Photo = dbContext.Photos.First(),
                Text = "Test test test test test test test"
            };

            // Act
            commentRepository.Insert(comment);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Comments.ToList(), comment);
        }
        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"..\..\Resources\DataAccess\Repositories\WrongComment.xml",
            tableName: "Comment",
            dataAccessMethod: DataAccessMethod.Random)]
        public void AddWrongComment_Exception()
        {
            // Arrange
            CommentRepository commentRepository = new CommentRepository(dbContext);
            Comment user = new Comment()
            {
                Date = new DateTime(year: Convert.ToInt32(TestContext.DataRow["Year"]),
                                    month: Convert.ToInt32(TestContext.DataRow["Month"]),
                                    day: Convert.ToInt32(TestContext.DataRow["Day"])),
                User = dbContext.Users.First(),
                Photo = dbContext.Photos.First(),
                Text = Convert.ToString(TestContext.DataRow["Text"]),
            };

            // Act
            commentRepository.Insert(user);

            // Assert
            Assert.ThrowsException<System.Data.Entity.Validation.DbEntityValidationException>(() => dbContext.SaveChanges());
            // undo adding
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(user);
        }
        #endregion
        // DELETE BY KEY
        #region DELETE BY KEY
        [TestMethod]
        public void DeleteByKey()
        {
            // Arrange
            CommentRepository commentRepository = new CommentRepository(dbContext);
            Comment expectedDeletedComment = dbContext.Comments.First();
            int idToDelete = expectedDeletedComment.Id;

            // Act
            commentRepository.Delete(idToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Comments.ToArray(), expectedDeletedComment);
            // Checks if all comment`s likes are deleted.
            Assert.IsFalse(dbContext.CommentLike.AsEnumerable().Any(cl => cl.Comment == null || cl.Comment.Id == expectedDeletedComment.Id));
        }
        [TestMethod]
        public void DeleteByWrongKey_Exception()
        {
            // Arrange
            CommentRepository commentRepository = new CommentRepository(dbContext);
            int wrongId = int.MaxValue;

            // Act
            // Assert
            Assert.ThrowsException<InvalidOperationException>(() => commentRepository.Delete(wrongId));
        }
        [TestMethod]
        public void DeleteByNullKey_Exception()
        {
            // Arrange
            CommentRepository commentRepository = new CommentRepository(dbContext);
            object wrongId = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => commentRepository.Delete(wrongId));
        }
        #endregion
        // DELETE BY VALUE
        #region DELETE BY VALUE
        [TestMethod]
        public void DeleteByValue()
        {
            // Arrange
            CommentRepository commentRepository = new CommentRepository(dbContext);
            Comment commentToDelete = dbContext.Comments.First();

            // Act
            commentRepository.Delete(commentToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Comments.ToArray(), commentToDelete);
            // Checks if all comment`s likes are deleted.
            Assert.IsFalse(dbContext.CommentLike.AsEnumerable().Any(cl => cl.Comment == null || cl.Comment.Id == commentToDelete.Id));
        }
        [TestMethod]
        public void DeleteByNullValue()
        {
            // Arrange
            CommentRepository commentRepository = new CommentRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => commentRepository.Delete(entityToDelete: null));
        }
        [TestMethod]
        public void DeleteByChangedValue()
        {
            // Arrange
            CommentRepository commentRepository = new CommentRepository(dbContext);
            Comment changedCommentToDelete = dbContext.Comments.First();
            changedCommentToDelete.Text += "Changed it";

            // Act
            commentRepository.Delete(entityToDelete: changedCommentToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Comments.ToArray(), changedCommentToDelete);
            // Checks if all comment`s likes are deleted.
            Assert.IsFalse(dbContext.CommentLike.AsEnumerable().Any(cl => cl.Comment == null || cl.Comment.Id == changedCommentToDelete.Id));
        }
        #endregion
        // UPDATE
        #region UPDATE
        [TestMethod]
        public void Update()
        {
            // Arrange
            CommentRepository commentRepository = new CommentRepository(dbContext);
            Comment commentToUpdate = dbContext.Comments.First();
            string newText = "Sets here new text for comment";

            // Act
            commentToUpdate.Text = newText;
            commentRepository.Update(commentToUpdate);

            // Assert
            Assert.AreEqual(dbContext.Comments.Find(commentToUpdate.Id).Text, newText);
        }
        #endregion
    }
}
