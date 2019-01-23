using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static Core.Configuration.DBConfig;

namespace DataAccess.Entities
{
    /// <summary>
    /// Maps to User table
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }
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
	    public virtual ICollection<User> Followers { get; set; } = new List<User>();
        /// <summary>
        /// A following collection
        /// </summary>
	    public virtual ICollection<User> Following { get; set; } = new List<User>();
        /// <summary>
        /// A comments collection
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        /// <summary>
        /// A likes to photo collection
        /// </summary>
        public virtual ICollection<PhotoLike> PhotoLikes { get; set; } = new List<PhotoLike>();
        /// <summary>
        /// A like to comments collection
        /// </summary>
        public virtual ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();
        /// <summary>
        /// A messages collection
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
        /// <summary>
        /// Defines is current user an admin
        /// </summary>
        public bool IsAdmin { get; set; }        
    }
}
