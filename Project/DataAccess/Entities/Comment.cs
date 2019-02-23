using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Core.Configuration.DBConfig;

namespace DataAccess.Entities
{
    /// <summary>
    /// Maps to Comments table
    /// </summary>
	public class Comment : EntityBase
    {
        // PROPERTIES
        /// <summary>
        /// Unique identifier
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override System.Guid Id { get; set; }

        /// <summary>
        /// An user that wrote a comment
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// A photo to which comment has been written
        /// </summary>
        public virtual Photo Photo { get; set; }
        /// <summary>
        /// A collection of likes to current comment
        /// </summary>
		public virtual ICollection<CommentLike> Likes { get; set; } = new HashSet<CommentLike>();
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

        #region  to string option
        /// <summary>
        /// Gets brief information about entity
        /// </summary>
        /// <returns>Brief information about entity</returns>
        protected override string GetBriefInfo()
        {
            return string.Concat(nameof(Comment), " with text : ", Text.Substring(startIndex: 0, length: 20));
        }
        /// <summary>
        /// Gets entity name
        /// </summary>
        /// <returns>Entity's name</returns>
        protected override string GetName()
        {
            return nameof(Comment);
        }
        #endregion
    }
}
