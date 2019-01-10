using System;
using System.Linq;
using System.Data.Entity.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DA = DataAccess.Context;
using DataAccess.Entities;
using System.Collections.Generic;

namespace UnitTest.DataAccess.Context
{
    [TestClass]
    public class AppContextTest
    {
        // FIELDS
        static DA.AppContext dbContext;
        // PROPERTIES
        public TestContext TestContext { get; set; }
        // INITIALIZERS
        [ClassInitialize]
        public static void Constructor(TestContext context)
        {
            dbContext = new DA.AppContext(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Largo\Source\Repos\Coursework\Project\UnitTest\Resources\DataAccess\TestDB.mdf; Integrated Security=True;Initial Catalog=TestDB");
        }
        [ClassCleanup]
        public static void Finalizer()
        {
            dbContext.Dispose();
        }
        // TEST
        [TestMethod]
        public void AddRegularUserWithoutAvatar()
        {
            // Arrange
            User user = new User() { NickName = "John", Password = "1111" };
            User[] users = new User[]
            {
                new User() { NickName = "Adam", Password = "1111"},
                new User() { NickName = "Braxton", Password = "1111"},
                new User() { NickName = "Boyle", Password = "1111"},
                new User() { NickName = "Jarred", Password = "1111"},
            };

            // Act
            Console.WriteLine(dbContext.Users.Count());
            dbContext.Users.Add(user);
            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToList(), user);
            CollectionAssert.IsSubsetOf(users, dbContext.Users.ToArray());
        }
        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"C:\Users\Largo\Source\Repos\Coursework\Project\UnitTest\Resources\DataAccess\Context\WrongLengthNameOrPassword.xml",
            tableName: "User",
            dataAccessMethod: DataAccessMethod.Random)]
        public void AddUsersWithWrongLengthNameOrPassword()
        {
            // Arrange
            User user = new User()
            {
                NickName = Convert.ToString(TestContext.DataRow["NickName"]),
                Password = Convert.ToString(TestContext.DataRow["Password"]),
            };

            // Act
            dbContext.Users.Add(user);

            // Assert
            Assert.ThrowsException<System.Data.Entity.Validation.DbEntityValidationException>(() => dbContext.SaveChanges());
            // undo adding
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(user);
        }

        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"C:\Users\Largo\Source\Repos\Coursework\Project\UnitTest\Resources\DataAccess\Context\AvatarFormat.xml",
            tableName: "User",
            dataAccessMethod: DataAccessMethod.Random)]
        public void AvatarFormatTest_AddRegularUserWithAvatar()
        {
            // Arrange
            User user = new User()
            {
                MainPhotoPath = Convert.ToString(TestContext.DataRow["Avatar"]),
                NickName = Convert.ToString(TestContext.DataRow["NickName"]),
                Password = Convert.ToString(TestContext.DataRow["Password"]),
            };

            // Act
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user);
        }
        [TestMethod]
        public void AddUserWithPhoto()
        {
            // Arrange
            User user = new User()
            {
                NickName = "John",
                Password = "1111",
                Photos = new List<Photo>()
                {
                    new Photo() { Path = "1/54/23.jpg" },
                    new Photo() { Path = "1/54/24.jpg" },
                    new Photo() { Path = "1/54/25.jpg" }
                }
            };
            int uniqueForeignKeyAmount = 1;

            // Act
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            int actualForeignKeyAmount = dbContext.Photos.Select(x => x.User.Id).Distinct().Count();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user);
            CollectionAssert.IsSubsetOf(user.Photos.ToList(), dbContext.Photos.ToArray());
            Assert.AreEqual(uniqueForeignKeyAmount, actualForeignKeyAmount, "Foreign key amount is not the same");            
        }
    }
}
