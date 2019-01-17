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
        static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Integrated Security=True; Initial Catalog=UserTestDB";
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
            int wrongId = 69;
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
            connectionString: @"..\..\Resources\DataAccess\Context\WrongLengthNameOrPassword.xml",
            tableName: "User",
            dataAccessMethod: DataAccessMethod.Random)]
        public void AddUsersWithWrongLengthNameOrPassword()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            User user = new User()
            {
                NickName = Convert.ToString(TestContext.DataRow["NickName"]),
                Password = Convert.ToString(TestContext.DataRow["Password"]),
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
            connectionString: @"..\..\Resources\DataAccess\Context\AvatarFormat.xml",
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
                Password = Convert.ToString(TestContext.DataRow["Password"]),
            };

            // Act
            userRepository.Insert(user);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user);
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

            // Act
            userRepository.Insert(user);
            dbContext.SaveChanges();
            IQueryable<Photo> photosFromDb = dbContext.Photos.Where(photo => photo.Path == "4/54/23.jpg" || photo.Path == "5/54/24.jpg" || photo.Path == "6/54/25.jpg");

            int actualForeignKeyAmount = photosFromDb.Select(photo => photo.User.Id).Distinct().Count();
            int actualForeignKey = photosFromDb.Select(photo => photo.User.Id).Distinct().First();

            // Assert                                                            
            CollectionAssert.Contains(dbContext.Users.ToArray(), user);
            CollectionAssert.IsSubsetOf(user.Photos.ToList(), dbContext.Photos.ToArray());
        }
        #endregion
        // DELETE BY KEY
        #region DELETE BY KEY
        [TestMethod]
        public void DeleteByKey()
        {
            Assert.Fail("Fail because regular deleting in DB does not work");
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            User expectedDeletedUser = dbContext.Users.First();
            int idToDelete = expectedDeletedUser.Id;

            // Act
            userRepository.Delete(idToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Users.ToArray(), expectedDeletedUser);
        }
        [TestMethod]
        public void DeleteByWrongKey_Exception()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            int wrongId = 200;

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
            Assert.Fail("Fail because regular deleting in DB does not work");
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            User userToDelete = dbContext.Users.First();

            // Act
            userRepository.Delete(userToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Users.ToArray(), userToDelete);
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
            Assert.Fail("Fail because regular deleting in DB does not work");
            // Arrange
            UserRepository userRepository = new UserRepository(dbContext);
            User changedUserToDelete = dbContext.Users.Where(u => u.NickName == "John").First();
            changedUserToDelete.NickName += "Chnaged it";

            // Act
            userRepository.Delete(entityToDelete: changedUserToDelete);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Users.ToArray(), changedUserToDelete);
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
