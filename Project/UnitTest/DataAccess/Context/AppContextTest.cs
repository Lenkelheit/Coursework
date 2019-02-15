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
        [TestCleanup]
        public void Cleaner()
        {
            dbFiller.Purge(dbContext);
        }


        // TEST
        // USER FOLLOWERS
        #region USER FOLLOWER
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
            connectionString: @"..\..\Resources\DataAccess\Repositories\WrongLengthNameOrPassword.xml",
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
            connectionString: @"..\..\Resources\DataAccess\Repositories\AvatarFormat.xml",
            tableName: "User",
            dataAccessMethod: DataAccessMethod.Random)]
        public void AvatarFormatTest_AddRegularUserWithAvatar()
        {
            // Arrange
            User user = new User()
            {
                MainPhotoName = Convert.ToString(TestContext.DataRow["Avatar"]),
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
        public void AddUserAndFollower()
        {
            // Arrange
            User user1 = new User() { NickName = "Jordan", Password = "1111" };
            User user2 = new User() { NickName = "Braxton", Password = "1111" };
            user1.Followers.Add(user2);

            // Act
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.SaveChanges();
            User userFromDb1 = dbContext.Users.First(u => u.NickName == user1.NickName);
            User userFromDb2 = dbContext.Users.First(u => u.NickName == user2.NickName);

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user1);
            CollectionAssert.Contains(dbContext.Users.ToArray(), user2);
            CollectionAssert.Contains(userFromDb1.Followers.ToArray(), user2);
            CollectionAssert.Contains(userFromDb2.Following.ToArray(), user1);
        }
        [TestMethod]
        public void DeleteUser_AndFollower_Cascade()
        {
            // Arrange
            User user1 = new User() { NickName = "Jordan", Password = "1111" };
            User user2 = new User() { NickName = "Braxton", Password = "1111" };
            user1.Followers.Add(user2);

            // Act
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.Users.Remove(user2);
            dbContext.SaveChanges();

            User userFromDb1 = dbContext.Users.First(u => u.NickName == user1.NickName);

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user1);
            CollectionAssert.DoesNotContain(dbContext.Users.ToArray(), user2);
            CollectionAssert.DoesNotContain(userFromDb1.Followers.ToArray(), user2);
        }
        #endregion
        // PHOTO
        #region PHOTO
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
                    new Photo() { Name = "23.jpg" },
                    new Photo() { Name = "24.jpg" },
                    new Photo() { Name = "25.jpg" }
                }
            };
            string expectedUserNickName = user.NickName;
            int expectedUserNickNameAmount = 1;

            // Act
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            IQueryable<Photo> photosFromDb = dbContext.Photos.Where(photo => photo.Name == "23.jpg" || photo.Name == "24.jpg" || photo.Name == "25.jpg");

            int actualUserNickNameAmount = photosFromDb.Select(photo => photo.User.NickName).Distinct().Count();
            string actualUserNickName = photosFromDb.Select(photo => photo.User.NickName).Distinct().First();

            // Assert                                                            
            CollectionAssert.Contains(dbContext.Users.ToArray(), user);
            CollectionAssert.IsSubsetOf(user.Photos.ToList(), dbContext.Photos.ToArray());
            Assert.AreEqual(expectedUserNickName, actualUserNickName, "User nicknames are not the same");            
            Assert.AreEqual(expectedUserNickNameAmount, actualUserNickNameAmount, "The photo has reference to more users that was added");
        }
        [TestMethod]
        public void AddPhotoWithoutUser_Exception()
        {
            Assert.Fail();

            // Arrange
            Photo photo = new Photo() { Name = "23.jpg" };

            // Act
            dbContext.Photos.Add(photo);

            // Assert
            Assert.ThrowsException<DbUpdateException>(() => dbContext.SaveChanges());
            // undo adding
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(photo);
        }
        [TestMethod]
        public void AddUserPhotoLikes()
        {
            // Arrange
            Photo photo = new Photo() { Name = "23.jpg" };
            User user = new User() { NickName = "John", Password = "1111" };
            PhotoLike photoLike = new PhotoLike() { IsLiked = true, Photo = photo };
            user.Photos.Add(photo);
            user.PhotoLikes.Add(photoLike);

            // Act
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user);
            CollectionAssert.Contains(dbContext.Photos.ToArray(), photo);
            CollectionAssert.Contains(dbContext.PhotoLike.ToArray(), photoLike);

            CollectionAssert.Contains(dbContext.Users.First(u => u.NickName == user.NickName).PhotoLikes.ToArray(), photoLike);
            CollectionAssert.Contains(dbContext.Photos.First(p => p.Name == photo.Name).Likes.ToArray(), photoLike);
        }
        [TestMethod]
        public void AddPhotoLikeWithoutPhoto_Exception()
        {
            Assert.Fail();

            // Arrange
            User user = new User() { NickName = "John", Password = "1111" };
            PhotoLike photoLike = new PhotoLike() { IsLiked = true, User = user };

            // Act
            dbContext.Users.Add(user);
            dbContext.PhotoLike.Add(photoLike);

            // Assert
            Assert.ThrowsException<DbUpdateException>(() => dbContext.SaveChanges());
            // undo adding
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(user);
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(photoLike);
        }
        [TestMethod]
        public void AddPhotoLikeWithoutUser_Exception()
        {
            Assert.Fail();

            // Arrange
            Photo photo = new Photo() { Name = "23.jpg" };
            PhotoLike photoLike = new PhotoLike() { IsLiked = true, Photo = photo };

            // Act
            dbContext.Photos.Add(photo);
            dbContext.PhotoLike.Add(photoLike);

            // Assert
            Assert.ThrowsException<DbUpdateException>(() => dbContext.SaveChanges());
            // undo adding
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(photo);
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(photoLike);
        }
        
        [TestMethod]
        public void DeletePhoto()
        {
            // Arrange
            Photo photo = new Photo() { Name = "23.jpg" };
            User user = new User() { NickName = "John", Password = "1111" };
            user.Photos.Add(photo);

            // Act
            dbContext.Users.Add(user);
            dbContext.Photos.Remove(photo);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user);
            CollectionAssert.DoesNotContain(dbContext.Users.First(u => u.NickName == user.NickName).Photos.ToArray(), photo);
            CollectionAssert.DoesNotContain(dbContext.Photos.ToArray(), photo);
        }
        [TestMethod]
        public void DeleteUser_AndPhoto_Cascade()
        {
            Assert.Fail();

            // Arrange
            Photo photo = new Photo() { Name = "23.jpg" };
            User user = new User() { NickName = "John", Password = "1111" };
            user.Photos.Add(photo);

            // Act
            dbContext.Users.Add(user);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Users.ToArray(), user);
            CollectionAssert.DoesNotContain(dbContext.Photos.ToArray(), photo);
        }

        [TestMethod]
        public void DeletePhotoLike()
        {
            // Arrange
            Photo photo = new Photo() { Name = "23.jpg" };
            User user = new User() { NickName = "John", Password = "1111" };
            PhotoLike photoLike = new PhotoLike() { IsLiked = true, Photo = photo };
            user.Photos.Add(photo);
            user.PhotoLikes.Add(photoLike);

            // Act
            dbContext.Users.Add(user);
            dbContext.PhotoLike.Remove(photoLike);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user);
            CollectionAssert.Contains(dbContext.Photos.ToArray(), photo);
            CollectionAssert.DoesNotContain(dbContext.PhotoLike.ToArray(), photoLike);

            CollectionAssert.DoesNotContain(dbContext.Users.First(u => u.NickName == user.NickName).PhotoLikes.ToArray(), photoLike);
            CollectionAssert.DoesNotContain(dbContext.Photos.First(p => p.Name == photo.Name).Likes.ToArray(), photoLike);
        }
        
        [TestMethod]
        public void DeletePhoto_AndPhotoLike_Cascade()
        {
            // Arrange
            Photo photo = new Photo() { Name = "23.jpg" };
            User user = new User() { NickName = "John", Password = "1111" };
            PhotoLike photoLike = new PhotoLike() { IsLiked = true, Photo = photo };
            user.Photos.Add(photo);
            user.PhotoLikes.Add(photoLike);

            // Act
            dbContext.Users.Add(user);
            dbContext.Photos.Remove(photo);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user);
            CollectionAssert.DoesNotContain(dbContext.Photos.ToArray(), photo);
            CollectionAssert.DoesNotContain(dbContext.PhotoLike.ToArray(), photoLike);

            CollectionAssert.DoesNotContain(dbContext.Users.First(u => u.NickName == user.NickName).Photos.ToArray(), photoLike);
            CollectionAssert.DoesNotContain(dbContext.Users.First(u => u.NickName == user.NickName).PhotoLikes.ToArray(), photoLike);
        }
        [TestMethod]
        public void DeleteUser_AndPhotoLike_Cascade()
        {
            Assert.Fail();

            // Arrange
            Photo photo = new Photo() { Name = "23.jpg" };
            User user1 = new User() { NickName = "John", Password = "1111" };
            User user2 = new User() { NickName = "Adam", Password = "1111" };
            PhotoLike photoLike = new PhotoLike() { IsLiked = true, Photo = photo, User = user1 };
            user2.Photos.Add(photo);
            user2.PhotoLikes.Add(photoLike);

            // Act
            dbContext.Users.Add(user2);
            dbContext.Users.Add(user1);
            dbContext.Users.Remove(user1);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Users.ToArray(), user1);
            CollectionAssert.Contains(dbContext.Users.ToArray(), user2);

            CollectionAssert.Contains(dbContext.Photos.ToArray(), photo);
            CollectionAssert.Contains(dbContext.Users.First(u => u.NickName == user2.NickName).Photos.ToArray(), photo);

            CollectionAssert.DoesNotContain(dbContext.PhotoLike.ToArray(), photoLike);
            CollectionAssert.DoesNotContain(dbContext.Photos.Find(photo).Likes.ToArray(), photoLike);
        }

        [TestMethod]
        public void DeleteUser_AndPhotoAndPhotoLike_Cascade()
        {
            Assert.Fail();
            
            // Arrange
            Photo photo = new Photo() { Name = "23.jpg" };
            User user = new User() { NickName = "Saimon", Password = "1111" };
            PhotoLike photoLike = new PhotoLike() { IsLiked = true, Photo = photo };
            user.Photos.Add(photo);
            user.PhotoLikes.Add(photoLike);            

            // Act
            dbContext.Users.Add(user);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Users.ToArray(), user);
            CollectionAssert.DoesNotContain(dbContext.Photos.ToArray(), photo);
            CollectionAssert.DoesNotContain(dbContext.PhotoLike.ToArray(), photoLike);              
        }
        #endregion
        // COMMENTS
        #region COMMENTS
        [TestMethod]
        public void AddUserWithPhotoAndUserWithComment()
        {
            // Arrange
            Photo photo1 = new Photo() { Name = "23.jpg" };
            Photo photo2 = new Photo() { Name = "24.jpg" };

            User user1 = new User()
            {
                NickName = "John",
                Password = "1111",
                Photos = new List<Photo>()
                {
                    photo1, photo2
                }
            };
            User user2 = new User()
            {
                NickName = "John",
                Password = "1111",
                Comments = new List<Comment>()
                {
                    new Comment() { Text = "Comment text", Date = DateTime.Now, Photo = photo1 },
                    new Comment() { Text = "Comment text", Date = DateTime.Now, Photo = photo2 },
                    new Comment() { Text = "Comment text", Date = DateTime.Now, Photo = photo1 }
                }
            };
            int uniqueCommentUserNickNameAmount = 1;
            int uniqueCommmentPhotoPathAmount = 2;

            // Act
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.SaveChanges();
            int actualCommentUserNickNameAmount = dbContext.Comments.Select(x => x.User.NickName).Distinct().Count();
            int actualCommentPhotoPathAmount = dbContext.Comments.Select(x => x.Photo.Name).Distinct().Count();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user1);
            CollectionAssert.Contains(dbContext.Users.ToArray(), user2);
            CollectionAssert.Contains(dbContext.Photos.ToArray(), photo1);
            CollectionAssert.Contains(dbContext.Photos.ToArray(), photo2);
            CollectionAssert.IsSubsetOf(user1.Photos.ToList(), dbContext.Photos.ToArray());
            CollectionAssert.IsSubsetOf(user2.Comments.ToList(), dbContext.Comments.ToArray());
            Assert.AreEqual(uniqueCommentUserNickNameAmount, actualCommentUserNickNameAmount, "User nicknames amount are not the same");
            Assert.AreEqual(uniqueCommmentPhotoPathAmount, actualCommentPhotoPathAmount, "Photo pathes are not the same");
        }
        [TestMethod]
        public void AddUserCommentWithoutPhoto_Exception()
        {
            Assert.Fail();

            // Arrange
            Comment comment1 = new Comment() { Text = "Comment text", Date = DateTime.Now };
            Comment comment2 = new Comment() { Text = "Comment text", Date = DateTime.Now };
            Comment comment3 = new Comment() { Text = "Comment text", Date = DateTime.Now };
            User user = new User()
            {
                NickName = "John",
                Password = "1111",
                Comments = new List<Comment>()
                {
                    comment1,
                    comment2,
                    comment3
                }
            };

            // Act
            dbContext.Users.Add(user);
            
            // Assert
            Assert.ThrowsException<DbUpdateException>(() => dbContext.SaveChanges());
            // undo adding
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(user);
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(comment1);
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(comment2);
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(comment3);
        }
        [TestMethod]
        public void AddPhotoCommentWithoutPhoto_Exception()
        {
            Assert.Fail();
            // Arrange
            Comment comment1 = new Comment() { Text = "Comment text", Date = DateTime.Now };
            Comment comment2 = new Comment() { Text = "Comment text", Date = DateTime.Now };
            Comment comment3 = new Comment() { Text = "Comment text", Date = DateTime.Now };
            User user = new User()
            {
                NickName = "John",
                Password = "1111",
                Comments = new List<Comment>()
                {
                    comment1,
                    comment2,
                    comment3
                }
            };

            // Act
            dbContext.Users.Add(user);

            // Assert
            Assert.ThrowsException<DbUpdateException>(() => dbContext.SaveChanges());
            // undo adding
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(user);
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(comment1);
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(comment2);
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(comment3);
        }
        [TestMethod]
        public void AddCommentLike()
        {
            // Arrange
            Photo photo1 = new Photo() { Name = "23.jpg" };
            Photo photo2 = new Photo() { Name = "24.jpg" };

            User user1 = new User()
            {
                NickName = "John",
                Password = "1111",
                Photos = new List<Photo> { photo1, photo2 }
            };
            Comment comment = new Comment()
            {
                Text = "Comment text",
                Date = DateTime.Now,
                Photo = photo1,
                User = user1
            };
            CommentLike commentLike = new CommentLike() { IsLiked = true, Comment = comment };
            User user2 = new User() { NickName = "Adam", Password = "1111", CommentLikes = new List<CommentLike> { commentLike } };

            // Act
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user1);
            CollectionAssert.Contains(dbContext.Users.ToArray(), user2);
            CollectionAssert.Contains(dbContext.Comments.ToArray(), comment);
            CollectionAssert.Contains(dbContext.CommentLike.ToArray(), commentLike);

            CollectionAssert.Contains(dbContext.Users.First(u => u.NickName == user2.NickName).CommentLikes.ToArray(), commentLike);
            CollectionAssert.Contains(dbContext.Comments.First(c => c.Text == comment.Text).Likes.ToArray(), commentLike);
        }
        [TestMethod]
        public void DeleteComment()
        {
            // Arrange
            Photo photo1 = new Photo() { Name = "23.jpg" };
            Photo photo2 = new Photo() { Name = "24.jpg" };

            User user1 = new User()
            {
                NickName = "John",
                Password = "1111",
                Photos = new List<Photo> { photo1, photo2 }
            };
            User user2 = new User() { NickName = "Adam", Password = "1111" };
            Comment comment = new Comment()
            {
                Text = "Comment text",
                Date = DateTime.Now,
                Photo = photo1,
                User = user2
            };

            // Act
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.Photos.Add(photo1);
            dbContext.Photos.Add(photo2);
            dbContext.Comments.Add(comment);
            dbContext.Comments.Remove(comment);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user1);
            CollectionAssert.Contains(dbContext.Users.ToArray(), user2);

            CollectionAssert.DoesNotContain(dbContext.Users.First(u => u.NickName == user1.NickName).Comments.ToArray(), comment);
            CollectionAssert.DoesNotContain(dbContext.Comments.ToArray(), comment);

            CollectionAssert.Contains(dbContext.Photos.ToArray(), photo1);
            CollectionAssert.Contains(dbContext.Photos.ToArray(), photo2);
        }
        [TestMethod]
        public void DeleteCommentLike()
        {
            // Arrange
            Photo photo1 = new Photo() { Name = "23.jpg" };
            Photo photo2 = new Photo() { Name = "24.jpg" };

            User user1 = new User()
            {
                NickName = "John",
                Password = "1111",
                Photos = new List<Photo> { photo1, photo2 }
            };
            Comment comment = new Comment()
            {
                Text = "Comment text",
                Date = DateTime.Now,
                Photo = photo1,
                User = user1
            };
            CommentLike commentLike = new CommentLike() { IsLiked = true, Comment = comment };
            User user2 = new User() { NickName = "Adam", Password = "1111", CommentLikes = new List<CommentLike> { commentLike } };

            // Act
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.CommentLike.Remove(commentLike);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user1);
            CollectionAssert.Contains(dbContext.Users.ToArray(), user2);
            CollectionAssert.Contains(dbContext.Comments.ToArray(), comment);
            CollectionAssert.Contains(dbContext.Photos.ToArray(), photo1);
            CollectionAssert.Contains(dbContext.Photos.ToArray(), photo2);
            CollectionAssert.DoesNotContain(dbContext.CommentLike.ToArray(), commentLike);

            CollectionAssert.DoesNotContain(dbContext.Users.First(u => u.NickName == user2.NickName).CommentLikes.ToArray(), commentLike);
            CollectionAssert.DoesNotContain(dbContext.Comments.First(c => c.Text == comment.Text).Likes.ToArray(), commentLike);
        }
        [TestMethod]
        public void DeleteUser_AndComment_Cascade()
        {
            // Arrange
            Photo photo1 = new Photo() { Name = "23.jpg" };
            Photo photo2 = new Photo() { Name = "24.jpg" };

            User user1 = new User()
            {
                NickName = "John",
                Password = "1111",
                Photos = new List<Photo> { photo1, photo2 }
            };
            User user2 = new User() { NickName = "Adam", Password = "1111" };
            Comment comment = new Comment()
            {
                Text = "Comment text",
                Date = DateTime.Now,
                Photo = photo1,
                User = user2
            };
            
            // Act
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.Users.Remove(user2);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user1);
            CollectionAssert.DoesNotContain(dbContext.Users.ToArray(), user2);

            CollectionAssert.DoesNotContain(dbContext.Users.First(u => u.NickName == user1.NickName).Comments.ToArray(), comment);
            CollectionAssert.DoesNotContain(dbContext.Comments.ToArray(), comment);

            CollectionAssert.Contains(dbContext.Photos.ToArray(), photo1);
            CollectionAssert.Contains(dbContext.Photos.ToArray(), photo2);     
        }
        [TestMethod]
        public void DeleteUser_AndCommentLike_Cascade()
        {
            // Arrange
            Photo photo1 = new Photo() { Name = "23.jpg" };
            Photo photo2 = new Photo() { Name = "24.jpg" };

            User user1 = new User()
            {
                NickName = "John",
                Password = "1111",
                Photos = new List<Photo> { photo1, photo2 }
            };
            Comment comment = new Comment()
            {
                Text = "Comment text",
                Date = DateTime.Now,
                Photo = photo1,
                User = user1                
            };
            photo1.Comments.Add(comment);
            User user2 = new User() { NickName = "Adam", Password = "1111" };
            CommentLike commentLike = new CommentLike() { IsLiked = true, User = user2 };
            comment.Likes.Add(commentLike);

            // Act
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.Users.Remove(user2);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user1, "There is no user1 in DB");
            CollectionAssert.DoesNotContain(dbContext.Users.ToArray(), user2, "User2 is still in DB");

            CollectionAssert.Contains(dbContext.Comments.ToArray(), comment, "There is no such comment in DB");
            CollectionAssert.Contains(dbContext.Users.First(u => u.NickName == user1.NickName).Comments.ToArray(), comment, "User 1 does not have current comment");


            CollectionAssert.Contains(dbContext.Photos.ToArray(), photo1);
            CollectionAssert.Contains(dbContext.Photos.ToArray(), photo2);
            CollectionAssert.Contains(dbContext.Users.First(u => u.NickName == user1.NickName).Photos.ToArray(), photo1);
            CollectionAssert.Contains(dbContext.Users.First(u => u.NickName == user1.NickName).Photos.ToArray(), photo2);

            CollectionAssert.DoesNotContain(dbContext.CommentLike.ToArray(), commentLike, "CommentLike is still in DB");
            CollectionAssert.DoesNotContain(dbContext.Comments.First(c => c.Text == comment.Text).Likes.ToArray(), commentLike, "Comment still has like");
        }
        [TestMethod]
        public void DeleteComment_AndCommentLike_Cascade()
        {
            // Arrange
            Photo photo1 = new Photo() { Name = "23.jpg" };
            Photo photo2 = new Photo() { Name = "24.jpg" };

            User user1 = new User()
            {
                NickName = "John",
                Password = "1111",
                Photos = new List<Photo> { photo1, photo2 }
            };
            Comment comment = new Comment()
            {
                Text = "Comment text",
                Date = DateTime.Now,
                Photo = photo1,
                User = user1
            };
            CommentLike commentLike = new CommentLike() { IsLiked = true, Comment = comment };
            User user2 = new User() { NickName = "Adam", Password = "1111", CommentLikes = new List<CommentLike> { commentLike } };

            // Act
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.Comments.Remove(comment);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user1);
            CollectionAssert.Contains(dbContext.Users.ToArray(), user2);
            CollectionAssert.DoesNotContain(dbContext.Comments.ToArray(), comment);
            CollectionAssert.DoesNotContain(dbContext.CommentLike.ToArray(), commentLike);

            CollectionAssert.DoesNotContain(dbContext.Users.First(u => u.NickName == user2.NickName).CommentLikes.ToArray(), commentLike);
        }
        [TestMethod]
        public void DeletePhoto_AndComment_Cascade()
        {
            // Arrange
            Photo photo1 = new Photo() { Name = "23.jpg" };
            Photo photo2 = new Photo() { Name = "24.jpg" };

            User user1 = new User()
            {
                NickName = "John",
                Password = "1111",
                Photos = new List<Photo> { photo1, photo2 }
            };
            Comment comment = new Comment()
            {
                Text = "Comment text",
                Date = DateTime.Now,
                Photo = photo1,
                User = user1
            };
            User user2 = new User() { NickName = "Adam", Password = "1111" };

            // Act
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.Photos.Remove(photo1);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user1);
            CollectionAssert.Contains(dbContext.Users.ToArray(), user2);
            CollectionAssert.Contains(dbContext.Photos.ToArray(), photo2);
            CollectionAssert.DoesNotContain(dbContext.Photos.ToArray(), photo1);
            CollectionAssert.DoesNotContain(dbContext.Comments.ToArray(), comment);
        }
        [TestMethod]
        public void DeletePhoto_AndCommentAndCommentLike_Cascade()
        {
            Assert.Fail();

            // Arrange
            Photo photo1 = new Photo() { Name = "23.jpg" };
            Photo photo2 = new Photo() { Name = "24.jpg" };

            User user1 = new User()
            {
                NickName = "John",
                Password = "1111",
                Photos = new List<Photo> { photo1, photo2 }
            };
            Comment comment = new Comment()
            {
                Text = "Comment text",
                Date = DateTime.Now,
                Photo = photo1,
                User = user1
            };
            CommentLike commentLike = new CommentLike() { IsLiked = true, Comment = comment };
            User user2 = new User() { NickName = "Adam", Password = "1111", CommentLikes = new List<CommentLike> { commentLike } };

            // Act
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.Photos.Remove(photo1);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user1);
            CollectionAssert.Contains(dbContext.Users.ToArray(), user2);
            CollectionAssert.Contains(dbContext.Photos.ToArray(), photo2);
            CollectionAssert.DoesNotContain(dbContext.Photos.ToArray(), photo1);
            CollectionAssert.DoesNotContain(dbContext.Comments.ToArray(), comment);
            CollectionAssert.DoesNotContain(dbContext.CommentLike.ToArray(), commentLike);

            CollectionAssert.DoesNotContain(dbContext.Users.First(u => u.NickName == user2.NickName).CommentLikes.ToArray(), commentLike);
        }
        [TestMethod]
        public void DeleteUser_AndPhotoAndCommentAndCommentLike_Cascade()
        {
            Assert.Fail();

            // Arrange
            Photo photo1 = new Photo() { Name = "23.jpg" };
            Photo photo2 = new Photo() { Name = "24.jpg" };

            User user1 = new User()
            {
                NickName = "John",
                Password = "1111",
                Photos = new List<Photo> { photo1, photo2 }
            };
            Comment comment = new Comment()
            {
                Text = "Comment text",
                Date = DateTime.Now,
                Photo = photo1,
                User = user1
            };
            CommentLike commentLike = new CommentLike() { IsLiked = true, Comment = comment };
            User user2 = new User() { NickName = "Adam", Password = "1111", CommentLikes = new List<CommentLike> { commentLike } };

            // Act
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.Users.Remove(user1);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user2);
            CollectionAssert.DoesNotContain(dbContext.Users.ToArray(), user1);
            CollectionAssert.DoesNotContain(dbContext.Photos.ToArray(), photo2);
            CollectionAssert.DoesNotContain(dbContext.Photos.ToArray(), photo1);
            CollectionAssert.DoesNotContain(dbContext.Comments.ToArray(), comment);
            CollectionAssert.DoesNotContain(dbContext.CommentLike.ToArray(), commentLike);

            CollectionAssert.DoesNotContain(dbContext.Users.First(u => u.NickName == user2.NickName).CommentLikes.ToArray(), commentLike);
        }
        [TestMethod]
        public void DeleteUser_AndPhotoAndPhotoLikeAndCommentAndCommentLike_Cascade()
        {
            Assert.Fail();

            // Arrange
            Photo photo1 = new Photo() { Name = "23.jpg" };
            Photo photo2 = new Photo() { Name = "24.jpg" };

            User user1 = new User()
            {
                NickName = "John",
                Password = "1111",
                Photos = new List<Photo> { photo1, photo2 }
            };
            Comment comment = new Comment()
            {
                Text = "Comment text",
                Date = DateTime.Now,
                Photo = photo1,
                User = user1
            };
            CommentLike commentLike = new CommentLike() { IsLiked = true, Comment = comment };
            PhotoLike photoLike = new PhotoLike() { IsLiked = true, Photo = photo1 };
            User user2 = new User() { NickName = "Adam", Password = "1111", CommentLikes = new List<CommentLike> { commentLike } };

            // Act
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.Users.Remove(user1);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user2);
            CollectionAssert.DoesNotContain(dbContext.Users.ToArray(), user1);
            CollectionAssert.DoesNotContain(dbContext.Photos.ToArray(), photo2);
            CollectionAssert.DoesNotContain(dbContext.Photos.ToArray(), photo1);
            CollectionAssert.DoesNotContain(dbContext.PhotoLike.ToArray(), photoLike);
            CollectionAssert.DoesNotContain(dbContext.Comments.ToArray(), comment);
            CollectionAssert.DoesNotContain(dbContext.CommentLike.ToArray(), commentLike);

            CollectionAssert.DoesNotContain(dbContext.Users.First(u => u.NickName == user2.NickName).CommentLikes.ToArray(), commentLike);
        }
        [TestMethod]
        public void DeleteUser_AndMesaageAndPhotoAndPhotoLikeAndCommentAndCommentLike_Cascade()
        {
            Assert.Fail();

            // Arrange
            Photo photo1 = new Photo() { Name = "23.jpg" };
            Photo photo2 = new Photo() { Name = "24.jpg" };

            User user1 = new User()
            {
                NickName = "John",
                Password = "1111",
                Photos = new List<Photo> { photo1, photo2 }
            };
            Comment comment = new Comment()
            {
                Text = "Comment text",
                Date = DateTime.Now,
                Photo = photo1,
                User = user1
            };
            CommentLike commentLike = new CommentLike() { IsLiked = true, Comment = comment };
            PhotoLike photoLike = new PhotoLike() { IsLiked = true, Photo = photo1 };
            User user2 = new User() { NickName = "Adam", Password = "1111", CommentLikes = new List<CommentLike> { commentLike } };
            Subject subject = new Subject() { Name = "Subject Name" };
            Message message = new Message()
            {
                Text = "This is message text",
                Date = DateTime.Now,
                Subject = subject,
                User = user1
            };

            // Act
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.Users.Remove(user1);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Users.ToArray(), user2);
            CollectionAssert.Contains(dbContext.Subjects.ToArray(), subject);
            CollectionAssert.DoesNotContain(dbContext.Messages.ToArray(), message);
            CollectionAssert.DoesNotContain(dbContext.Users.ToArray(), user1);
            CollectionAssert.DoesNotContain(dbContext.Photos.ToArray(), photo2);
            CollectionAssert.DoesNotContain(dbContext.Photos.ToArray(), photo1);
            CollectionAssert.DoesNotContain(dbContext.PhotoLike.ToArray(), photoLike);
            CollectionAssert.DoesNotContain(dbContext.Comments.ToArray(), comment);
            CollectionAssert.DoesNotContain(dbContext.CommentLike.ToArray(), commentLike);

            CollectionAssert.DoesNotContain(dbContext.Users.First(u => u.NickName == user2.NickName).CommentLikes.ToArray(), commentLike);
        }
        #endregion
        // SUBJECT AND MESSAGES 
        #region SUBJECT AND MESSAGES
        [TestMethod]
        public void AddSubject()
        {
            // Arrange
            Subject subject = new Subject() { Name = "Subject Name" };

            // Act
            dbContext.Subjects.Add(subject);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Subjects.ToArray(), subject);
        }
        [TestMethod]
        public void AddUserMessageWithSubject()
        {
            // Arrange
            Subject subject = new Subject() { Name = "Subject Name" };
            Message message = new Message()
            {
                Text = "This is message text",
                Date = DateTime.Now,
                Subject = subject,
                User = new User() { NickName = "John", Password = "1111" }
            };

            // Act
            dbContext.Messages.Add(message);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Subjects.ToArray(), subject);
            CollectionAssert.Contains(dbContext.Messages.ToArray(), message);
            Assert.AreEqual(dbContext.Messages.First(m => m.Text == message.Text).Subject, subject);
        }
        [TestMethod]
        public void AddUserMessageWithoutSubject()
        {
            // Arrange
            Message message = new Message()
            {
                Text = "This is message text",
                Date = DateTime.Now,
                User = new User() { NickName = "John", Password = "1111" }
            };

            // Act
            dbContext.Messages.Add(message);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Messages.ToArray(), message);
        }
        [TestMethod]
        public void AddMessagetSubjectWithoutUser_Exception()
        {
            // Arrange
            Message message = new Message()
            {
                Text = "This is message text",
                Subject = new Subject() { Name = "Subject Name" },
                Date = DateTime.Now,
            };

            // Act
            dbContext.Messages.Add(message);

            // Assert
            Assert.ThrowsException<DbUpdateException>(() => dbContext.SaveChanges());
            // undo adding
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(message);
        }
        [TestMethod]
        public void AddMessageWithoutSubjectWithoutUser_Exception()
        {
            // Arrange
            Message message = new Message()
            {
                Text = "This is message text",
                Date = DateTime.Now,
            };

            // Act
            dbContext.Messages.Add(message);

            // Assert
            Assert.ThrowsException<DbUpdateException>(() => dbContext.SaveChanges());
            // undo adding
            ((IObjectContextAdapter)dbContext).ObjectContext.Detach(message);
        }
        [TestMethod]
        public void DeleteSubject_Null()
        {
            // Arrange
            Subject subject = new Subject() { Name = "Subject Name" };
            Message message = new Message()
            {
                Text = "This is message text",
                Date = DateTime.Now,
                Subject = subject,
                User = new User() { NickName = "John", Password = "1111" }
            };

            // Act
            dbContext.Messages.Add(message);
            dbContext.Subjects.Remove(subject);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.Contains(dbContext.Messages.ToArray(), message);
            CollectionAssert.DoesNotContain(dbContext.Subjects.ToArray(), subject);
            Assert.IsNull(dbContext.Messages.First(m => m.Text == message.Text).Subject);
        }
        [TestMethod]
        public void DeleteUser_AndMessages_Cascade()
        {
            // Arrange
            User user = new User() { NickName = "John", Password = "1111" };
            Subject subject = new Subject() { Name = "Subject Name" };
            Message message = new Message()
            {
                Text = "This is message text",
                Date = DateTime.Now,
                Subject = subject,
                User = user
            };

            // Act
            dbContext.Users.Add(user);
            dbContext.Messages.Add(message);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();

            // Assert
            CollectionAssert.DoesNotContain(dbContext.Users.ToArray(), user, "User");
            CollectionAssert.DoesNotContain(dbContext.Messages.ToArray(), message, "Message");
            CollectionAssert.Contains(dbContext.Subjects.ToArray(), subject, "Subject");
        }
        #endregion
    }
}
