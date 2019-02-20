using DataAccess.Comparers;
using DataAccess.Enums.Comparers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Core.Configuration.DBConfig;

namespace DataAccess.Entities
{
    /// <summary>
    /// Maps to User table
    /// </summary>
    public class User : EntityBase
    {
        // PROPERTIES
        /// <summary>
        /// Unique identifier
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override System.Guid Id { get; set; }
        /// <summary>
        /// A local path to avatar
        /// </summary>
        [MinLength(AVATAR_MIN_LENGTH)]
        [MaxLength(AVATAR_MAX_LENGTH)]
        [FileExtensions(Extensions = PHOTO_EXTENSION)]
        public string MainPhotoPath { get; set; }
        /// <summary>
        /// Nickname of the user
        /// </summary>
        [MinLength(NICKNAME_MIN_LENGTH)]
        [MaxLength(NICKNAME_MAX_LENGTH)]
        public string NickName { get; set; }
        /// <summary>
        /// Password of the user
        /// </summary>
        [MinLength(PASSWORD_MIN_LENGTH)]
        [MaxLength(PASSWORD_MAX_LENGTH)]
        public string Password { get; set; }
        /// <summary>
        /// A collection of photos posted by user
        /// </summary>
	    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
        /// <summary>
        /// A follower collection
        /// </summary>
	    public virtual ICollection<User> Followers { get; set; } = new SortedSet<User>(new UserComparer(UserCompareType.NickName));
        /// <summary>
        /// A following collection
        /// </summary>
	    public virtual ICollection<User> Following { get; set; } = new SortedSet<User>(new UserComparer(UserCompareType.NickName));
        /// <summary>
        /// A comments collection
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        /// <summary>
        /// A likes to photo collection
        /// </summary>
        public virtual ICollection<PhotoLike> PhotoLikes { get; set; } = new HashSet<PhotoLike>();
        /// <summary>
        /// A like to comments collection
        /// </summary>
        public virtual ICollection<CommentLike> CommentLikes { get; set; } = new HashSet<CommentLike>();
        /// <summary>
        /// A messages collection
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
        /// <summary>
        /// Defines is current user an admin
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// Defines is current user is blocked by admin
        /// </summary>
        public bool IsBlocked { get; set; }

        // METHODS
        #region  to string option
        /// <summary>
        /// Gets brief information about entity
        /// </summary>
        /// <returns>Brief information about entity</returns>
        protected override string GetBriefInfo()
        {
            return string.Concat(nameof(User), " with nickname : ", NickName);
        }
        /// <summary>
        /// Gets entity name
        /// </summary>
        /// <returns>Entity's name</returns>
        protected override string GetName()
        {
            return nameof(User);
        }
        #endregion
    }
}
