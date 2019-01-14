﻿using System;
using System.Linq;
using System.Text;
using DataAccess.Entities;

namespace UnitTest.Resources.Classes
{
    internal class DbFiller
    {
        // FIELDS
        readonly Random random;
        readonly string[] names;
        readonly string[] words;
        // CONSTRUCTORS
        public DbFiller()
        {
            random = new Random();
            names = new string[50] { "Bennie", "Wan", "Neil", "Lynna", "Chrissy", "Vivienne", "Ambrose", "Salina", "Thelma", "Joellen", "Donovan", "Margarita", "Eliseo", "Lavada",
                                     "Letitia", "Kayleen", "Hermine", "Yvette", "Dino", "Tabitha", "Margareta", "Jordon", "Loree", "Crystle", "Darcey", "Tameika", "Josiah", "Kathie",
                                     "Galen", "Chauncey", "Jeannetta", "Sharonda", "Petra", "Victor", "Vida", "Corinna", "Dee", "Pia", "Carry", "Hipolito", "Colleen", "Katelynn",
                                     "Henry", "Argelia", "Rossie", "Lavonia", "Zena", "Ashleigh", "Annmarie", "Debbra" };
            words = string.Concat("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras accumsan mi a quam viverra luctus. ",
                                  "Suspendisse ut vulputate nisi, nec fermentum libero. ",
                                  "Morbi sollicitudin, orci sit amet congue cursus, nibh nunc lobortis orci, et hendrerit arcu nunc a lorem. Curabitur dignissim risus non diam ornare mattis ac vitae elit. ",
                                  "Suspendisse euismod gravida diam et varius. Sed quis commodo magna. Nunc a ex nec erat feugiat dapibus. Donec id nulla et dolor efficitur convallis. ",
                                  "Maecenas vel sem neque. Vivamus volutpat quam ac urna condimentum, vitae posuere neque scelerisque. Aenean et lacinia dolor.").ToLower().Split(new char[] { ',', '.'}, StringSplitOptions.RemoveEmptyEntries);

        }
        // DB METHODS
        public void Fill(global::DataAccess.Context.AppContext dbContext)
        {
            // USERS
            #region USERS
            User user1 = new User { NickName = "John",       Password = "1111", MainPhotoPath = "1223/466/64.jpg", IsAdmin = false };
            User user2 = new User { NickName = "Kayson",     Password = "1111", MainPhotoPath = null,              IsAdmin = false };
            User user3 = new User { NickName = "Clementine", Password = "1111", MainPhotoPath = "1223/466/64.jpg", IsAdmin = false };
            User user4 = new User { NickName = "Beverley",   Password = "1111", MainPhotoPath = null,              IsAdmin = false };
            User user5 = new User { NickName = "Harold",     Password = "1111", MainPhotoPath = "1223/466/64.jpg", IsAdmin = true };
            #endregion

            // FOLLOWERS
            #region FOLLOWERS
            user1.Followers.Add(user2);
            user1.Followers.Add(user3);
            user1.Followers.Add(user5);

            user2.Followers.Add(user1);
            user2.Followers.Add(user3);
            user2.Followers.Add(user4);

            user3.Followers.Add(user4);
            user3.Followers.Add(user5);

            user4.Followers.Add(user1);
            #endregion

            // PHOTOS
            #region PHOTOS
            // to user
            // 1
            Photo photo1 = new Photo { Path = "1/1.jpg", User = user1 };
            Photo photo2 = new Photo { Path = "1/2.jpg", User = user1 };
            Photo photo3 = new Photo { Path = "1/3.jpg", User = user1 };

            // 2
            Photo photo4 = new Photo { Path = "2/1.jpg", User = user2 };
            Photo photo5 = new Photo { Path = "2/2.jpg", User = user2 };
            Photo photo6 = new Photo { Path = "2/3.jpg", User = user2 };
            Photo photo7 = new Photo { Path = "2/4.jpg", User = user2 };
            Photo photo8 = new Photo { Path = "2/5.jpg", User = user2 };

            // 3
            Photo photo9 = new Photo { Path = "3/1.jpg", User = user3 };
            Photo photo10 = new Photo { Path = "3/2.jpg", User = user3 };

            // 4
            Photo photo11 = new Photo { Path = "4/1.jpg", User = user4 };

            // 5
            Photo photo12 = new Photo { Path = "5/1.jpg", User = user5 };
            Photo photo13 = new Photo { Path = "5/2.jpg", User = user5 };
            #endregion

            // PHOTO LIKES
            #region PHOTO LIKES
            // to photo
            // 1
            PhotoLike photoLike1 = new PhotoLike { IsLiked = true, Photo = photo1, User = user1 };
            PhotoLike photoLike2 = new PhotoLike { IsLiked = false, Photo = photo1, User = user2 };
            PhotoLike photoLike3 = new PhotoLike { IsLiked = true, Photo = photo1, User = user3 };
            // 2
            PhotoLike photoLike4 = new PhotoLike { IsLiked = true, Photo = photo2, User = user4 };
            PhotoLike photoLike5 = new PhotoLike { IsLiked = false, Photo = photo2, User = user1 };
            PhotoLike photoLike6 = new PhotoLike { IsLiked = false, Photo = photo2, User = user2 };
            PhotoLike photoLike7 = new PhotoLike { IsLiked = true, Photo = photo2, User = user3 };
            // 3
            PhotoLike photoLike8 = new PhotoLike { IsLiked = true, Photo = photo3, User = user2 };

            // 4
            PhotoLike photoLike9 = new PhotoLike { IsLiked = true, Photo = photo4, User = user4 };
            PhotoLike photoLike10 = new PhotoLike { IsLiked = false, Photo = photo4, User = user1 };
            PhotoLike photoLike11 = new PhotoLike { IsLiked = true, Photo = photo4, User = user2 };
            // 5
            PhotoLike photoLike12 = new PhotoLike { IsLiked = true, Photo = photo5, User = user3 };
            PhotoLike photoLike13 = new PhotoLike { IsLiked = true, Photo = photo5, User = user5 };
            // 6
            PhotoLike photoLike14 = new PhotoLike { IsLiked = false, Photo = photo6, User = user1 };
            PhotoLike photoLike15 = new PhotoLike { IsLiked = true, Photo = photo6, User = user2 };
            // 7
            PhotoLike photoLike16 = new PhotoLike { IsLiked = true, Photo = photo7, User = user5 };
            PhotoLike photoLike17 = new PhotoLike { IsLiked = true, Photo = photo7, User = user4 };
            // 8
            PhotoLike photoLike18 = new PhotoLike { IsLiked = false, Photo = photo8, User = user1 };

            // 9
            PhotoLike photoLike19 = new PhotoLike { IsLiked = true, Photo = photo9, User = user3 };
            // 10
            PhotoLike photoLike20 = new PhotoLike { IsLiked = true, Photo = photo10, User = user2 };

            // 11
            PhotoLike photoLike21 = new PhotoLike { IsLiked = true, Photo = photo11, User = user4 };
            PhotoLike photoLike22 = new PhotoLike { IsLiked = false, Photo = photo11, User = user1 };
            PhotoLike photoLike23 = new PhotoLike { IsLiked = false, Photo = photo11, User = user3 };
            PhotoLike photoLike24 = new PhotoLike { IsLiked = true, Photo = photo11, User = user5 };

            // 12
            PhotoLike photoLike25 = new PhotoLike { IsLiked = true, Photo = photo12, User = user4 };
            PhotoLike photoLike26 = new PhotoLike { IsLiked = false, Photo = photo12, User = user2 };
            PhotoLike photoLike27 = new PhotoLike { IsLiked = true, Photo = photo12, User = user5 };
            // 13
            // no likes
            #endregion

            // COMMENTS
            #region COMMENTS
            // to photo
            // 1
            Comment comment1 = new Comment { Date = new DateTime(year: 2018, month: 1, day: 15), Photo = photo1, User = user1, Text = "Here comes the rain again" };
            Comment comment2 = new Comment { Date = new DateTime(year: 2018, month: 2, day: 1), Photo = photo1, User = user2, Text = "I'm only sleeping" };
            Comment comment3 = new Comment { Date = new DateTime(year: 2018, month: 3, day: 4), Photo = photo1, User = user3, Text = "Another one bites the dust" };

            // 2
            Comment comment4 = new Comment { Date = new DateTime(year: 2017, month: 4, day: 5), Photo = photo2, User = user1, Text = "Rape me" };
            // 3
            Comment comment5 = new Comment { Date = new DateTime(year: 2016, month: 5, day: 6), Photo = photo3, User = user2, Text = "Falling away from me" };
            Comment comment6 = new Comment { Date = new DateTime(year: 2018, month: 6, day: 7), Photo = photo3, User = user3, Text = "But in the end it doesn't even matter" };

            // 4
            Comment comment7 = new Comment { Date = new DateTime(year: 2018, month: 7, day: 8), Photo = photo4, User = user1, Text = "Sweet dreams are made of this" };
            Comment comment8 = new Comment { Date = new DateTime(year: 2017, month: 8, day: 9), Photo = photo4, User = user4, Text = "My girl my girl don't lie to me tell me where did you sleep last night" };
            Comment comment9 = new Comment { Date = new DateTime(year: 2018, month: 9, day: 10), Photo = photo4, User = user1, Text = "Monkey see, monkey do I'd rather be deal than cool" };
            Comment comment10 = new Comment { Date = new DateTime(year: 2017, month: 10, day: 11), Photo = photo4, User = user4, Text = "I don't deserve this, darling, you look perfect tonight" };
            // 5
            Comment comment11 = new Comment { Date = new DateTime(year: 2018, month: 11, day: 12), Photo = photo5, User = user4, Text = "And if I only could, I'd make a deal with God, And I'd get him to swap our places" };
            Comment comment12 = new Comment { Date = new DateTime(year: 2012, month: 12, day: 13), Photo = photo5, User = user1, Text = "I love to see you run around And i can see you now" };
            Comment comment13 = new Comment { Date = new DateTime(year: 2018, month: 1, day: 14), Photo = photo5, User = user2, Text = "The Show Must Go On" };
            // 6
            Comment comment14 = new Comment { Date = new DateTime(year: 2000, month: 2, day: 15), Photo = photo6, User = user1, Text = "Goo goo g'joob" };
            // 7
            Comment comment15 = new Comment { Date = new DateTime(year: 2009, month: 3, day: 16), Photo = photo7, User = user3, Text = "And it's the missing that will kill you, knowing that you've missed your shot" };
            // 8
            Comment comment16 = new Comment { Date = new DateTime(year: 2018, month: 4, day: 17), Photo = photo8, User = user4, Text = "One is the loneliest number that you'll ever do" };
            Comment comment17 = new Comment { Date = new DateTime(year: 2004, month: 5, day: 18), Photo = photo8, User = user1, Text = "I'm just your problem" };
            Comment comment18 = new Comment { Date = new DateTime(year: 2018, month: 6, day: 19), Photo = photo8, User = user2, Text = "We didn't start the fire" };
            Comment comment19 = new Comment { Date = new DateTime(year: 2003, month: 7, day: 20), Photo = photo8, User = user4, Text = "God Is Dead?" };

            // 9
            Comment comment20 = new Comment { Date = new DateTime(year: 2018, month: 8, day: 21), Photo = photo9, User = user4, Text = "I don't wanna hear you say it" };
            Comment comment21 = new Comment { Date = new DateTime(year: 2015, month: 9, day: 22), Photo = photo9, User = user1, Text = "You're a dancer But you're dancing on air Just a matter of time till you fall" };
            Comment comment22 = new Comment { Date = new DateTime(year: 2016, month: 10, day: 23), Photo = photo9, User = user3, Text = "Wouldn't it be nice if we were older Then we wouldn't have to wait so long?" };
            // 10
            Comment comment23 = new Comment { Date = new DateTime(year: 2015, month: 11, day: 24), Photo = photo10, User = user4, Text = "I shot the sheriff, but I did not shoot the deputy" };
            Comment comment24 = new Comment { Date = new DateTime(year: 2010, month: 12, day: 25), Photo = photo10, User = user1, Text = "I hurt myself today To see if I still feel" };
            Comment comment25 = new Comment { Date = new DateTime(year: 2010, month: 1, day: 26), Photo = photo10, User = user1, Text = "I said if you're thinking of being my brother, It don't matter if you're Black or White" };
            Comment comment26 = new Comment { Date = new DateTime(year: 2014, month: 2, day: 27), Photo = photo10, User = user4, Text = "Well I've heard there was a secret chord" };
            Comment comment27 = new Comment { Date = new DateTime(year: 2019, month: 3, day: 28), Photo = photo10, User = user2, Text = "Gotta find that fool who did that to you" };

            // 11
            Comment comment28 = new Comment { Date = new DateTime(year: 2014, month: 4, day: 1), Photo = photo11, User = user4, Text = "All we are is dust in the wind" };

            // 12
            // no comments
            // 13
            Comment comment29 = new Comment { Date = new DateTime(year: 2012, month: 5, day: 2), Photo = photo13, User = user2, Text = "I want to get away, I want to fly away, Yeah yeah yeah" };
            Comment comment30 = new Comment { Date = new DateTime(year: 2014, month: 6, day: 3), Photo = photo13, User = user2, Text = "Where is my mind" };
            #endregion

            // COMMENTS LIKE
            #region COMMENTS LIKE
            // to comment
            // 1
            CommentLike commentLike1 = new CommentLike { IsLiked = true, Comment = comment1, User = user1 };
            CommentLike commentLike2 = new CommentLike { IsLiked = true, Comment = comment1, User = user3 };
            CommentLike commentLike3 = new CommentLike { IsLiked = false, Comment = comment1, User = user2 };
            // 2
            CommentLike commentLike4 = new CommentLike { IsLiked = false, Comment = comment2, User = user1 };
            CommentLike commentLike5 = new CommentLike { IsLiked = true, Comment = comment2, User = user3 };
            // 3
            CommentLike commentLike6 = new CommentLike { IsLiked = true, Comment = comment3, User = user1 };
            // 4
            // 5
            CommentLike commentLike7 = new CommentLike { IsLiked = false, Comment = comment5, User = user3 };
            CommentLike commentLike8 = new CommentLike { IsLiked = true, Comment = comment5, User = user5 };
            // 6
            // 7
            CommentLike commentLike9 = new CommentLike { IsLiked = true, Comment = comment7, User = user2 };
            // 8
            // 9
            // 10
            // 11
            CommentLike commentLike10 = new CommentLike { IsLiked = true, Comment = comment11, User = user1 };
            // 12
            // 13
            // 14
            CommentLike commentLike11 = new CommentLike { IsLiked = false, Comment = comment14, User = user3 };
            CommentLike commentLike12 = new CommentLike { IsLiked = true, Comment = comment14, User = user1 };
            // 15
            // 16
            CommentLike commentLike13 = new CommentLike { IsLiked = true, Comment = comment16, User = user5 };
            // 17
            // 18
            CommentLike commentLike14 = new CommentLike { IsLiked = true, Comment = comment18, User = user1 };
            CommentLike commentLike15 = new CommentLike { IsLiked = true, Comment = comment18, User = user2 };
            // 19
            // 20
            CommentLike commentLike16 = new CommentLike { IsLiked = false, Comment = comment20, User = user3 };
            // 21
            // 22
            CommentLike commentLike17 = new CommentLike { IsLiked = true, Comment = comment22, User = user2 };
            CommentLike commentLike18 = new CommentLike { IsLiked = true, Comment = comment22, User = user5 };
            // 23
            // 24
            // 25
            CommentLike commentLike19 = new CommentLike { IsLiked = true, Comment = comment25, User = user3 };
            CommentLike commentLike20 = new CommentLike { IsLiked = false, Comment = comment25, User = user1 };
            // 26
            // 27
            // 28
            CommentLike commentLike21 = new CommentLike { IsLiked = false, Comment = comment28, User = user2 };
            CommentLike commentLike22 = new CommentLike { IsLiked = false, Comment = comment28, User = user3 };
            // 29
            // 30

            #endregion

            // SUBJECTS
            #region SUBJECTS
            Subject subject1 = new Subject { Name = "Subject 1" };
            Subject subject2 = new Subject { Name = "Subject 2" };
            Subject subject3 = new Subject { Name = "Subject 3" };
            Subject subject4 = new Subject { Name = "Subject 4" };
            Subject subject5 = new Subject { Name = "Subject 5" };
            #endregion

            // MESSAGES
            #region MESSAGES
            // to subject
            // 1
            Message message1 = new Message { Date = new DateTime(year: 2000, month: 1, day: 15), Subject = subject1, User = user1, Text = "I've just seen a face" };
            Message message2 = new Message { Date = new DateTime(year: 2001, month: 1, day: 15), Subject = subject1, User = user2, Text = "So maybe tomorrow, I'll find my way home" };
            Message message3 = new Message { Date = new DateTime(year: 2002, month: 1, day: 15), Subject = subject1, User = user1, Text = "In a crooked little town, they were lost and never found" };
            // 2
            Message message4 = new Message { Date = new DateTime(year: 2003, month: 1, day: 15), Subject = subject2, User = user2, Text = "Wake me up when september ends" };
            Message message5 = new Message { Date = new DateTime(year: 2004, month: 1, day: 15), Subject = subject2, User = user1, Text = "What does the fox say?" };
            // 3
            Message message6 = new Message { Date = new DateTime(year: 2005, month: 1, day: 15), Subject = subject3, User = user3, Text = "Daydream, I fell asleep amid the flowers, For a couple of hours on a beautiful day" };
            Message message7 = new Message { Date = new DateTime(year: 2006, month: 1, day: 15), Subject = subject3, User = user1, Text = "We all live in a yellow submarine" };
            // 4
            Message message8 = new Message { Date = new DateTime(year: 2007, month: 1, day: 15), Subject = subject4, User = user4, Text = "Just tonight I will stay and well throw it all away" };
            Message message9 = new Message { Date = new DateTime(year: 2008, month: 1, day: 15), Subject = subject4, User = user1, Text = "Somebody mixed my medicine" };
            // 5
            Message message10 = new Message { Date = new DateTime(year: 2009, month: 1, day: 15), Subject = subject5, User = user5, Text = "A couple of Gs, an R and an E, an I and an N" };
            Message message11 = new Message { Date = new DateTime(year: 2010, month: 1, day: 15), Subject = subject5, User = user1, Text = "On candy stripe legs the Spiderman comes, Softly through the shadow of the evening sun" };
            Message message12 = new Message { Date = new DateTime(year: 2011, month: 1, day: 15), Subject = subject5, User = user5, Text = "Everybody wants to rule the world" };
            Message message13 = new Message { Date = new DateTime(year: 2012, month: 1, day: 15), Subject = subject5, User = user2, Text = "Don`t tell me I'm wrong, Don't tell that you knew all along" };
            // 6 deleted subject
            Message message14 = new Message { Date = new DateTime(year: 2013, month: 1, day: 15), Subject = null, User = user1, Text = "Cut my life into pieces" };
            Message message15 = new Message { Date = new DateTime(year: 2014, month: 1, day: 15), Subject = null, User = user2, Text = "Listen to the wind blow, watch the sun rise, Running in the shadows, damn your love, damn your lies" };
            #endregion

            #region Adding
            dbContext.Users.AddRange(new User[] { user1, user2, user3, user4, user5 });
            dbContext.Photos.AddRange(new Photo[] { photo1, photo2, photo3, photo4, photo5, photo6, photo7, photo8, photo9, photo10, photo11, photo12, photo13 });
            //dbContext.PhotoLike.AddRange(new PhotoLike[] { photoLike1, photoLike2, photoLike3, photoLike4, photoLike5, photoLike6, photoLike7, photoLike8, photoLike9,
            //                                               photoLike10, photoLike11, photoLike12, photoLike13, photoLike14, photoLike15, photoLike16, photoLike17, photoLike18, photoLike19,
            //                                               photoLike20, photoLike21, photoLike22, photoLike23, photoLike24, photoLike25, photoLike26, photoLike27 });
            dbContext.Comments.AddRange(new Comment[] { comment1, comment2, comment3, comment4, comment5, comment6, comment7, comment8, comment9,
                                                        comment10, comment11, comment12, comment13, comment14, comment15, comment16, comment17, comment18, comment19,
                                                        comment20, comment21, comment22, comment23, comment24, comment25, comment26, comment27, comment28, comment29, comment30});
            //dbContext.CommentLike.AddRange(new CommentLike[] { commentLike1, commentLike2, commentLike3, commentLike4, commentLike5, commentLike6, commentLike7, commentLike8, commentLike9,
            //                                                   commentLike11, commentLike12, commentLike13, commentLike14, commentLike15, commentLike16, commentLike17, commentLike18, commentLike19,
            //                                                   commentLike20, commentLike21, commentLike22 });
            dbContext.Subjects.AddRange(new Subject[] { subject1, subject2, subject3, subject4, subject5 });
            dbContext.Messages.AddRange(new Message[] { message1, message2, message3, message4, message5, message6, message7, message8, message9,
                                                        message10, message11, message12, message13, message14, message15 });
            dbContext.SaveChanges();
            #endregion
        }

        public void RandomFill(global::DataAccess.Context.AppContext dbContext, int userAmount = 1000, int photoAmount = 10000, int commentAmount = 2000, int likesAmount = 5000, int subjectAmount = 10, int messagesAmount = 1000)
        {
            throw new System.NotImplementedException("its broken, ian I dont know why");
            // USERS
            #region USERS
            User[] users = new User[userAmount];
            for (int i = 0; i < userAmount; ++i)
            {
                users[i] = new User
                {
                    NickName = names[random.Next(names.Length)],
                    Password = GeneratePassword(),
                    MainPhotoPath = GenerateImagePath(),
                    IsAdmin = random.Next(5) > 4 
                };
            }
            #endregion

            // FOLLOWERS
            #region FOLLOWERS
            for (int i = 0; i < userAmount; ++i)
            {
                int followersAmount = random.Next(25);
                for (int j = 0; j < followersAmount; ++j)
                {
                    int followerId = random.Next(users.Length);
                    if (followerId == i) continue; // do not follow on himself

                    users[i].Followers.Add(users[followerId]);
                }
            }
            #endregion

            // PHOTOS
            #region PHOTOS
            Photo[] photos = new Photo[photoAmount];
            for (int i = 0; i < photoAmount; ++i)
            {
                photos[i] = new Photo { Path = GenerateImagePath(), User = users[random.Next(users.Length)] };
            }
            #endregion

            // PHOTO LIKES
            #region PHOTO LIKES
            PhotoLike[] photoLikes = new PhotoLike[likesAmount];
            for (int i = 0; i < likesAmount; ++i)
            {
                photoLikes[i] = new PhotoLike
                {
                    IsLiked = random.Next(2) > 1,
                    Photo = photos[random.Next(photoAmount)],
                    User = users[random.Next(userAmount)]
                };
            }
            #endregion

            // COMMENTS
            #region COMMENTS
            Comment[] comments = new Comment[commentAmount];
            for (int i = 0; i < commentAmount; ++i)
            {
                comments[i] = new Comment
                {
                    Date = GenerateDate(),
                    Photo = photos[random.Next(photoAmount)],
                    User = users[random.Next(userAmount)],
                    Text = GenerateSentence()
                };
            }
            #endregion

            // COMMENTS LIKE
            #region COMMENTS LIKE
            CommentLike[] commentLikes = new CommentLike[likesAmount];
            for (int i = 0; i < likesAmount; ++i)
            {
                commentLikes[i] = new CommentLike
                {
                    IsLiked = random.Next(2) > 1,
                    Comment = comments[random.Next(commentAmount)],
                    User = users[random.Next(userAmount)]
                };
            }
            #endregion

            // SUBJECTS
            #region SUBJECTS
            Subject[] subjects = new Subject[subjectAmount];
            for (int i = 0; i < subjectAmount; ++i)
            {
                subjects[i] = new Subject { Name = $"Subject {i + 1}" };
            }
            #endregion

            // MESSAGES
            #region MESSAGES
            Message[] messages = new Message[messagesAmount];
            for (int i = 0; i < messagesAmount; ++i)
            {
                messages[i] = new Message
                {
                    Date = GenerateDate(),
                    Subject = subjects[random.Next(subjectAmount)],
                    User = users[random.Next(userAmount)],
                    Text = GenerateSentence()
                };
            }
            #endregion

            #region Adding
            dbContext.Users.AddRange(users);
            dbContext.Photos.AddRange(photos);
            //dbContext.PhotoLike.AddRange(photoLikes);
            dbContext.Comments.AddRange(comments);
            //dbContext.CommentLike.AddRange(commentLikes);
            dbContext.Subjects.AddRange(subjects);
            dbContext.Messages.AddRange(messages);
            dbContext.SaveChanges();
            #endregion
        }

        public void Purge(global::DataAccess.Context.AppContext dbContext)
        {
            #warning Probably very slow, could use SQl, but should write this in generic style
            dbContext.Users.RemoveRange(dbContext.Set<User>());
            dbContext.Photos.RemoveRange(dbContext.Set<Photo>());
            dbContext.PhotoLike.RemoveRange(dbContext.Set<PhotoLike>());
            dbContext.Comments.RemoveRange(dbContext.Set<Comment>());
            dbContext.CommentLike.RemoveRange(dbContext.Set<CommentLike>());
            dbContext.Messages.RemoveRange(dbContext.Set<Message>());
            dbContext.Subjects.RemoveRange(dbContext.Set<Subject>());
            dbContext.SaveChanges();
        }
        // PRIVATE METHODS
        private string GeneratePassword(int length = 4)
        {
            StringBuilder password = new StringBuilder(length);
            for (int i = 0; i < length; ++i)
            {
                password.Append(random.Next(10));
            }
            return password.ToString();
        }
        private string GenerateImagePath()
        {
            return $"{random.Next()}/{random.Next()}/{random.Next()}/.jpg";            
        }
        private DateTime GenerateDate()
        {
            return new DateTime(year: random.Next(2000, 2018), month: random.Next(1, 13), day: random.Next(1, 28));
        }
        private string GenerateSentence(int wordAmount = 10)
        {
            StringBuilder sentence = new StringBuilder();
            for (int i = 0; i < wordAmount; ++i)
            {
                sentence.Append(words[random.Next(words.Length)]);
                sentence.Append(' ');
            }
            return sentence.ToString();
        }
    }
}