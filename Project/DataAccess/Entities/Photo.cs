﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static Core.Configuration.DBConfig;

namespace DataAccess.Entities
{
    /// <summary>
    /// Maps to Photo table
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// Unique indetifier
        /// </summary>
		public int Id { get; set; }
        /// <summary>
        /// A server path to a photo
        /// </summary>
        [MinLength(PHOTO_PATH_MIN_LENGTH)]
        [MaxLength(PHOTO_PATH_MAX_LENGTH)]
        [FileExtensions(Extensions = PHOTO_EXTENSION)]
		public string Path { get; set; }
        /// <summary>
        /// A user that has been posted a photo
        /// </summary>
		public User User { get; set; }
        /// <summary>
        /// A collection of likes and dislikes to photo
        /// </summary>
		public ICollection<PhotoLike> Likes { get; set; } = new List<PhotoLike>();
        /// <summary>
        /// A comment collection relative to photo
        /// </summary>
		public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
