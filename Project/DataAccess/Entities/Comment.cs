using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static Core.Configuration.DBConfig;

namespace DataAccess.Entities
{
    /// <summary>
    /// Maps to Comments table
    /// </summary>
	public class Comment
	{
        /// <summary>
        /// Unique indetifier
        /// </summary>
		public int Id { get; set; }
        /// <summary>
        /// An user that wrote a comment
        /// </summary>
		public User User { get; set; }
        /// <summary>
        /// A photo to wich comment has been wriiten
        /// </summary>
        public Photo Photo { get; set; }
        /// <summary>
        /// A collection of likes to current comment
        /// </summary>
		public ICollection<CommentLike> Likes { get; set; } = new List<CommentLike>();
        /// <summary>
        /// Comment's text
        /// </summary>
        [MinLength(COMMENT_TEXT_MIN_LENGTH)]
        [MaxLength(COMMENT_TEXT_MAX_LENGTH)]
		public string Text { get; set; }
        /// <summary>
        /// The date when comment has been published
        /// </summary>
		public System.DateTime Date { get; set; } = System.DateTime.Now;
	}
}
