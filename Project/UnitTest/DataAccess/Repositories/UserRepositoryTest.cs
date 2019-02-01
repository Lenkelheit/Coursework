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
    public class UserRepositoryTest
    {
        // FIELDS
        static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Integrated Security=True; Initial Catalog=TestDB";
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
            UserRepository userRepository = new UserRepository(dbContext);
            int expectedUserInDb = 5;

            // Act
            int actualUserInDb = userRepository.Count();

            // Assert
            Assert.AreEqual(expectedUserInDb, actualUserInDb);
        }
        [TestMethod]
        public void CountIfNullAvatar()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            int expectedUserWithoutAvatarInDb = 2;

            // Act
            int actualUserWithoutAvatarDb = userRepository.Count(user => string.IsNullOrWhiteSpace(user.MainPhotoPath));

            // Assert
            Assert.AreEqual(expectedUserWithoutAvatarInDb, actualUserWithoutAvatarDb);
        }
        [TestMethod]
        public void CountIfIsAdmin()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            int expectedAdminAmount = 1;

            // Act
            int actualAdminAmount = userRepository.Count(user => user.IsAdmin);

            // Assert
            Assert.AreEqual(expectedAdminAmount, actualAdminAmount);
        }
        #endregion
        // GET
        #region GET
        [TestMethod]
        public void GetAll()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            int expectedUserInDb = 5;

            // Act
            IEnumerable<User> usersFromDb = userRepository.Get();
            int actualUserInDb = usersFromDb.Count();

            // Assert
            Assert.AreEqual(expectedUserInDb, actualUserInDb);
            CollectionAssert.AreEquivalent(dbContext.Users.ToArray(), usersFromDb.ToArray());
        }
        [TestMethod]
        public void GetFilterByPhotoAmount()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            int expectedUserInDb = 2;

            // Act
            IEnumerable<User> usersFromDb = userRepository.Get(filter: user => user.Photos.Count > 2);
            int actualUserInDb = usersFromDb.Count();

            // Assert
            Assert.AreEqual(expectedUserInDb, actualUserInDb);
            CollectionAssert.IsSubsetOf(usersFromDb.ToArray(), dbContext.Users.ToArray());
        }
        [TestMethod]
        public void GetOrder()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            int expectedUserInDb = 5;

            // Act
            IEnumerable<User> usersFromDb = userRepository.Get(orderBy: user => user.OrderBy(u => u.NickName));
            int actualUserInDb = usersFromDb.Count();

            // Assert
            Assert.AreEqual(expectedUserInDb, actualUserInDb);
            CollectionAssert.AreEqual(dbContext.Users.OrderBy(u => u.NickName).ToArray(), usersFromDb.ToArray());
        }
        [TestMethod]
        public void GetFilterAndOrder()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            int expectedUserInDb = 5;

            // Act
            IEnumerable<User> usersFromDb = userRepository.Get(filter: user => user.Messages.Count > 0, orderBy: o => o.OrderByDescending(user => user.NickName));
            int actualUserInDb = usersFromDb.Count();

            // Assert
            Assert.AreEqual(expectedUserInDb, actualUserInDb);
            CollectionAssert.AreEqual(dbContext.Users.Where(u => u.Messages.Count > 0).OrderByDescending(u => u.NickName).ToArray(), usersFromDb.ToArray());
        }
        #endregion
        // GET BY ID
        #region GET BY ID
        [TestMethod]
        public void GetById()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            int idToSearch = 4;
            User expectedUser = dbContext.Users.Find(idToSearch);

            // Act
            User usersFromDb = userRepository.Get(idToSearch);

            // Assert
            Assert.AreEqual(expectedUser, usersFromDb);
        }
        [TestMethod]
        public void GetByWrongId_Null()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            int wrongId = int.MaxValue;
            User expectedUserFromDb = null;

            // Act
            User actualUserFromDb = userRepository.Get(wrongId);

            // Assert
            Assert.AreEqual(expectedUserFromDb, actualUserFromDb);
        }
        #endregion
        // GET BY NICKNAME
        #region GET BY NICKNAME
        [TestMethod]
        public void GetByNickname()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            User expectedUser = dbContext.Users.ToArray()[2];
            string nameToSearch = expectedUser.NickName;

            // Act
            User usersFromDb = userRepository.Get(nameToSearch);

            // Assert
            Assert.AreEqual(expectedUser, usersFromDb);
        }
        [TestMethod]
        public void GetByNickname_HasAllColumn()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            string nameToSearch = "Harold";

            string expectedNickname = "Harold";
            string expectedPassword = "1111";
            string expectedAvatarPath = "1223/466/64.jpg";
            bool expectedIsAdminValue = true;
            int expectedFollowersAmount = 0;
            int expectedFollowingAmount = 2;
            int expectedPhotoAmount = 2;
            int expectedCommentAmount = 0;
            int expectedMessagesAmount = 2;
            int expectedCommentLikeAmount = 3;
            int expectedPhotoLikeAmount = 4;

            // Act
            User usersFromDb = userRepository.Get(nameToSearch);
            string actualNickname = usersFromDb.NickName;
            string actualPassword = usersFromDb.Password;
            string actualAvatarPath = usersFromDb.MainPhotoPath;
            bool actualIsAdminValue = usersFromDb.IsAdmin;
            int actualFollowersAmount = usersFromDb.Followers.Count;
            int actualFollowingAmount = usersFromDb.Following.Count;
            int actualPhotoAmount = usersFromDb.Photos.Count;
            int actualCommentAmount = usersFromDb.Comments.Count;
            int actualMessagesAmount = usersFromDb.Messages.Count;
            int actualCommentLikeAmount = usersFromDb.CommentLikes.Count;
            int actualPhotoLikeAmount = usersFromDb.PhotoLikes.Count;

            // Assert
            Assert.AreEqual(expectedNickname, actualNickname, "Nicknames are not the same");
            Assert.AreEqual(expectedPassword, actualPassword, "Passwords are not the same");
            Assert.AreEqual(expectedAvatarPath, actualAvatarPath, "Avatar pathes are not the same");
            Assert.AreEqual(expectedIsAdminValue, actualIsAdminValue, "Is Admin values are not the same");
            Assert.AreEqual(expectedFollowersAmount, actualFollowersAmount, "Followers amount are not the same");
            Assert.AreEqual(expectedFollowingAmount, actualFollowingAmount, "Following amount are not the same");
            Assert.AreEqual(expectedPhotoAmount, actualPhotoAmount, "Photos amount are not the same");
            Assert.AreEqual(expectedCommentAmount, actualCommentAmount, "Comments amount are not the same");
            Assert.AreEqual(expectedMessagesAmount, actualMessagesAmount, "Messages amount are not the same");
            Assert.AreEqual(expectedCommentLikeAmount, actualCommentLikeAmount, "Comment likes amount are not the same");
            Assert.AreEqual(expectedPhotoLikeAmount, actualPhotoLikeAmount, "Photo likes amount are not the same");
        }
        [TestMethod]
        public void GetByWrongNickname_Null()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            User expectedUser = null;
            string nameToSearch = "The name is wrong";

            // Act
            User usersFromDb = userRepository.Get(nameToSearch);

            // Assert
            Assert.AreEqual(expectedUser, usersFromDb);
        }
        [TestMethod]
        public void GetByNullNickname_Exception()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.Get(nickname: null));
        }
        #endregion
        // INSERT
        #region INSERT
        [TestMethod]
        public void AddRegularUserWithoutAvatar()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            User user = new User() { NickName = "John", Password = "1111" };

            // Act
            userRepository.Insert(user);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToList(), user);
        }
        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"..\..\Resources\DataAccess\Repositories\WrongLengthNameOrPassword.xml",
            tableName: "User",
            dataAccessMethod: DataAccessMethod.Random)]
        public void AddUsersWithWrongLengthNameOrPassword()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            User user = new User()
            {
                NickName = Convert.ToString(TestContext.DataRow["NickName"]),
                Password = Convert.ToString(TestContext.DataRow["Password"])
            };

            // Act
            userRepository.Insert(user);

            // Assert
            Assert.ThrowsException<System.Data.Entity.Validation.DbEntityValidationException>(() => dbContext.SaveChanges());
            // undo adding
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(user);
        }
        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"..\..\Resources\DataAccess\Repositories\AvatarFormat.xml",
            tableName: "User",
            dataAccessMethod: DataAccessMethod.Random)]
        public void AvatarFormatTest_AddRegularUserWithAvatar()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            User user = new User()
            {
                MainPhotoPath = Convert.ToString(TestContext.DataRow["Avatar"]),
                NickName = Convert.ToString(TestContext.DataRow["NickName"]),
                Password = Convert.ToString(TestContext.DataRow["Password"])
            };

            // Act
            userRepository.Insert(user);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user);
        }        
        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"..\..\Resources\DataAccess\Repositories\WrongAvatarPathOrExtension.xml",
            tableName: "User",
            dataAccessMethod: DataAccessMethod.Random)]
        public void AddWrongAvatarPathOrExtension_Exception()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            User user = new User()
            {
                MainPhotoPath = Convert.ToString(TestContext.DataRow["Avatar"]),
                NickName = Convert.ToString(TestContext.DataRow["NickName"]),
                Password = Convert.ToString(TestContext.DataRow["Password"])
            };

            // Act
            userRepository.Insert(user);

            // Assert
            Assert.ThrowsException<System.Data.Entity.Validation.DbEntityValidationException>(() => dbContext.SaveChanges());
            // undo adding
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(user);
        }
        [TestMethod]
        public void AddUserAndFollower()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            User user1 = new User() { NickName = "Jordan", Password = "1111" };
            User user2 = new User() { NickName = "Braxton", Password = "1111" };
            user1.Followers.Add(user2);

            // Act
            userRepository.Insert(user1);
            userRepository.Insert(user2);
            dbContext.SaveChanges();
            User userFromDb1 = dbContext.Users.First(u => u.NickName == "Jordan");
            User userFromDb2 = dbContext.Users.First(u => u.NickName == "Braxton");

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user1);
            CollectionAssert.Contains(dbContext.Users.ToArray(), user2);
            CollectionAssert.Contains(userFromDb1.Followers.ToArray(), user2);
            CollectionAssert.Contains(userFromDb2.Following.ToArray(), user1);
        }        
        [TestMethod]
        public void AddUserWithPhoto()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            User user = new User()
            {
                NickName = "John",
                Password = "1111",
                Photos = new List<Photo>()
                {
                    new Photo() { Path = "4/54/23.jpg" },
                    new Photo() { Path = "5/54/24.jpg" },
                    new Photo() { Path = "6/54/25.jpg" }
                }
            };
            int expectedForeignKeyAmount = 1;

            // Act
            userRepository.Insert(user);
            dbContext.SaveChanges();
            IQueryable<Photo> photosFromDb = dbContext.Photos.Where(photo => photo.Path == "4/54/23.jpg" || photo.Path == "5/54/24.jpg" || photo.Path == "6/54/25.jpg");

            int actualForeignKeyAmount = photosFromDb.Select(photo => photo.User.Id).Distinct().Count();
            int actualForeignKey = photosFromDb.Select(photo => photo.User.Id).Distinct().First();

            // Assert    
            Assert.AreEqual(expectedForeignKeyAmount, actualForeignKeyAmount);
            Assert.AreEqual(user.Id, actualForeignKey);
            CollectionAssert.Contains(dbContext.Users.ToArray(), user);
            CollectionAssert.IsSubsetOf(user.Photos.ToArray(), dbContext.Photos.ToArray());
        }
        #endregion
        // DELETE BY KEY
        #region DELETE BY KEY
        [TestMethod]
        public void DeleteByKey()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            User expectedDeletedUser = dbContext.Users.First();
            int idToDelete = expectedDeletedUser.Id;

            // Act
            List<PhotoLike> deletedPhotoLikesOfPhotos = dbContext.PhotoLike
                .AsEnumerable().Where(pl => expectedDeletedUser.Photos.Contains(pl.Photo)).ToList();

            List<Comment> deletedCommentsOfPhotos = dbContext.Comments
                .AsEnumerable().Where(c => expectedDeletedUser.Photos.Contains(c.Photo)).ToList();

            List<CommentLike> deletedCommentLikesOfPhotosComments = dbContext.CommentLike
                .AsEnumerable().Where(cl => deletedCommentsOfPhotos.Contains(cl.Comment)).ToList();

            userRepository.Delete(idToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Users.ToArray(), expectedDeletedUser);
            // Checks if all photolikes of user are deleted.
            Assert.IsFalse(dbContext.PhotoLike.AsEnumerable().Any(pl => pl.User == null || pl.User.Id == expectedDeletedUser.Id));
            // Checks if all user's comments are deleted.
            Assert.IsFalse(dbContext.Comments.AsEnumerable().Any(c => c.User == null || c.User.Id == expectedDeletedUser.Id));
            // Checks if all commentlikes of user are deleted.
            Assert.IsFalse(dbContext.CommentLike.AsEnumerable().Any(cl => cl.User == null || cl.User.Id == expectedDeletedUser.Id));
            // Checks if all photos of user are deleted.
            Assert.IsFalse(dbContext.Photos.AsEnumerable().Any(p => p.User == null || p.User.Id == expectedDeletedUser.Id));

            // Checks if all user's photos' photolikes are deleted.
            CollectionAssert.IsNotSubsetOf(deletedPhotoLikesOfPhotos, dbContext.PhotoLike.ToList());
            // Checks if all user's photos' comments are deleted.
            CollectionAssert.IsNotSubsetOf(deletedCommentsOfPhotos, dbContext.Comments.ToList());
            // Checks if all user's photos' comments' commentlikes are deleted.
            CollectionAssert.IsNotSubsetOf(deletedCommentLikesOfPhotosComments, dbContext.CommentLike.ToList());

            // Checks if all user's messages are deleted.
            Assert.IsFalse(dbContext.Messages.AsEnumerable().Any(m => m.User.Id == expectedDeletedUser.Id));

            // Checks if user isn't somebody's follower.
            Assert.IsFalse(dbContext.Users.AsEnumerable().Any(u => u.Followers.Contains(expectedDeletedUser)));
            // Checks if user isn't somebody's following.
            Assert.IsFalse(dbContext.Users.AsEnumerable().Any(u => u.Following.Contains(expectedDeletedUser)));
        }
        [TestMethod]
        public void DeleteByWrongKey_Exception()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            int wrongId = int.MaxValue;

            // Act
            // Assert
            Assert.ThrowsException<InvalidOperationException>(() => userRepository.Delete(wrongId));
        }
        [TestMethod]
        public void DeleteByNullKey_Exception()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            object wrongId = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.Delete(wrongId));
        }
        #endregion
        // DELETE BY VALUE
        #region DELETE BY VALUE
        [TestMethod]
        public void DeleteByValue()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            User userToDelete = dbContext.Users.First();

            // Act
            List<PhotoLike> deletedPhotoLikesOfPhotos = dbContext.PhotoLike
                .AsEnumerable().Where(pl => userToDelete.Photos.Contains(pl.Photo)).ToList();

            List<Comment> deletedCommentsOfPhotos = dbContext.Comments
                .AsEnumerable().Where(c => userToDelete.Photos.Contains(c.Photo)).ToList();

            List<CommentLike> deletedCommentLikesOfPhotosComments = dbContext.CommentLike
                .AsEnumerable().Where(cl => deletedCommentsOfPhotos.Contains(cl.Comment)).ToList();

            userRepository.Delete(userToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Users.ToArray(), userToDelete);
            // Checks if all photolikes of user are deleted.
            Assert.IsFalse(dbContext.PhotoLike.AsEnumerable().Any(pl => pl.User == null || pl.User.Id == userToDelete.Id));
            // Checks if all user's comments are deleted.
            Assert.IsFalse(dbContext.Comments.AsEnumerable().Any(c => c.User == null || c.User.Id == userToDelete.Id));
            // Checks if all commentlikes of user are deleted.
            Assert.IsFalse(dbContext.CommentLike.AsEnumerable().Any(cl => cl.User == null || cl.User.Id == userToDelete.Id));
            // Checks if all photos of user are deleted.
            Assert.IsFalse(dbContext.Photos.AsEnumerable().Any(p => p.User == null || p.User.Id == userToDelete.Id));

            // Checks if all user's photos' photolikes are deleted.
            CollectionAssert.IsNotSubsetOf(deletedPhotoLikesOfPhotos, dbContext.PhotoLike.ToList());
            // Checks if all user's photos' comments are deleted.
            CollectionAssert.IsNotSubsetOf(deletedCommentsOfPhotos, dbContext.Comments.ToList());
            // Checks if all user's photos' comments' commentlikes are deleted.
            CollectionAssert.IsNotSubsetOf(deletedCommentLikesOfPhotosComments, dbContext.CommentLike.ToList());

            // Checks if all user's messages are deleted.
            Assert.IsFalse(dbContext.Messages.AsEnumerable().Any(m => m.User.Id == userToDelete.Id));

            // Checks if user isn't somebody's follower.
            Assert.IsFalse(dbContext.Users.AsEnumerable().Any(u => u.Followers.Contains(userToDelete)));
            // Checks if user isn't somebody's following.
            Assert.IsFalse(dbContext.Users.AsEnumerable().Any(u => u.Following.Contains(userToDelete)));
        }
        [TestMethod]
        public void DeleteByNullValue()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.Delete(entityToDelete: null));
        }
        [TestMethod]
        public void DeleteByChangedValue()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            User changedUserToDelete = dbContext.Users.First(u => u.NickName == "John");
            changedUserToDelete.NickName += "Changed it";

            // Act
            List<PhotoLike> deletedPhotoLikesOfPhotos = dbContext.PhotoLike
                .AsEnumerable().Where(pl => changedUserToDelete.Photos.Contains(pl.Photo)).ToList();

            List<Comment> deletedCommentsOfPhotos = dbContext.Comments
                .AsEnumerable().Where(c => changedUserToDelete.Photos.Contains(c.Photo)).ToList();

            List<CommentLike> deletedCommentLikesOfPhotosComments = dbContext.CommentLike
                .AsEnumerable().Where(cl => deletedCommentsOfPhotos.Contains(cl.Comment)).ToList();

            userRepository.Delete(entityToDelete: changedUserToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Users.ToArray(), changedUserToDelete);
            // Checks if all photolikes of user are deleted.
            Assert.IsFalse(dbContext.PhotoLike.AsEnumerable().Any(pl => pl.User == null || pl.User.Id == changedUserToDelete.Id));
            // Checks if all user's comments are deleted.
            Assert.IsFalse(dbContext.Comments.AsEnumerable().Any(c => c.User == null || c.User.Id == changedUserToDelete.Id));
            // Checks if all commentlikes of user are deleted.
            Assert.IsFalse(dbContext.CommentLike.AsEnumerable().Any(cl => cl.User == null || cl.User.Id == changedUserToDelete.Id));
            // Checks if all photos of user are deleted.
            Assert.IsFalse(dbContext.Photos.AsEnumerable().Any(p => p.User == null || p.User.Id == changedUserToDelete.Id));

            // Checks if all user's photos' photolikes are deleted.
            CollectionAssert.IsNotSubsetOf(deletedPhotoLikesOfPhotos, dbContext.PhotoLike.ToList());
            // Checks if all user's photos' comments are deleted.
            CollectionAssert.IsNotSubsetOf(deletedCommentsOfPhotos, dbContext.Comments.ToList());
            // Checks if all user's photos' comments' commentlikes are deleted.
            CollectionAssert.IsNotSubsetOf(deletedCommentLikesOfPhotosComments, dbContext.CommentLike.ToList());

            // Checks if all user's messages are deleted.
            Assert.IsFalse(dbContext.Messages.AsEnumerable().Any(m => m.User.Id == changedUserToDelete.Id));

            // Checks if user isn't somebody's follower.
            Assert.IsFalse(dbContext.Users.AsEnumerable().Any(u => u.Followers.Contains(changedUserToDelete)));
            // Checks if user isn't somebody's following.
            Assert.IsFalse(dbContext.Users.AsEnumerable().Any(u => u.Following.Contains(changedUserToDelete)));
        }
        #endregion
        // UPDATE
        #region UPDATE
        [TestMethod]
        public void Update()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            User userToUpdate = dbContext.Users.First(u => u.NickName == "Beverley");
            string newNickName = "New Nick";

            // Act
            userToUpdate.NickName = newNickName;
            userRepository.Update(userToUpdate);

            // Assert
            Assert.AreEqual(dbContext.Users.Find(userToUpdate.Id).NickName, newNickName);
        }
        #endregion
        // IS NICKNAME FREE
        #region IS NICKNAME FREE
        [TestMethod]
        public void IsNicknameFree_True()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);

            // Act
            // Assert
            Assert.IsTrue(userRepository.IsNicknameFree("This nickname should be free"));
            Assert.IsTrue(userRepository.IsNicknameFree("john"));
        }
        [TestMethod]
        public void IsNicknameFree_False()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);

            // Act
            // Assert
            Assert.IsFalse(userRepository.IsNicknameFree("John"));
        }
        [TestMethod]
        public void IsNicknameFree_NullArgument_Exception()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsNicknameFree(null));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsNicknameFree(string.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsNicknameFree(""));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsNicknameFree("      "));
        }
        #endregion
        // IS DATA VALID
        #region IS DATA VALID            
        [TestMethod]
        public void IsDataValid_BothNull_Exception()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid(null, null));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid("", ""));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid(string.Empty, string.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid("    ", "    "));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid("", null));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid(string.Empty, null));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid("         ", null));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid(null, ""));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid(null, string.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid(null, "         "));
        }
        [TestMethod]
        public void IsDataValid_NicknameNull_Exception()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid(null, "1111"));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid("", "1111"));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid(string.Empty, "1111"));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid("        ", "1111"));
        }
        [TestMethod]
        public void IsDataValid_PasswordNull_Exception()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid("nickname", null));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid("nickname", ""));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid("nickname", string.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.IsDataValid("nickname", "          "));
        }
        [TestMethod]
        public void IsDataValid_WrongNickname()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);

            // Act
            ValidNameAndPassword actualResult = userRepository.IsDataValid("beverley", "1111");

            // Assert
            Assert.IsFalse(actualResult.IsNameValid);
            Assert.IsFalse(actualResult.IsPasswordValid);
        }
        [TestMethod]
        public void IsDataValid_WrongPassword()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);

            // Act
            ValidNameAndPassword actualResult = userRepository.IsDataValid("Beverley", "1234");

            // Assert
            Assert.IsTrue(actualResult.IsNameValid);
            Assert.IsFalse(actualResult.IsPasswordValid);
        }
        [TestMethod]
        public void IsDataValid_BothWrong()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);

            // Act
            ValidNameAndPassword actualResult = userRepository.IsDataValid("beverley", "1234");

            // Assert
            Assert.IsFalse(actualResult.IsNameValid);
            Assert.IsFalse(actualResult.IsPasswordValid);
        }
        [TestMethod]
        public void IsDataValid_BothValid()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);

            // Act
            ValidNameAndPassword actualResult = userRepository.IsDataValid("Beverley", "1111");

            // Assert
            Assert.IsTrue(actualResult.IsNameValid, "Nicknam is not valid");
            Assert.IsTrue(actualResult.IsPasswordValid, "Password is not valid");
        }
        #endregion
    }
}
