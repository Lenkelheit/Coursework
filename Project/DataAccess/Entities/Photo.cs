using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Core.Configuration.DBConfig;

namespace DataAccess.Entities
{
    /// <summary>
    /// Maps to Photo table
    /// </summary>
    public class Photo : EntityBase
    {
        // PROPERTIES
        /// <summary>
        /// Unique identifier
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override System.Guid Id { get; set; }
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
		public virtual User User { get; set; }
        /// <summary>
        /// A collection of likes and dislikes to photo
        /// </summary>
		public virtual ICollection<PhotoLike> Likes { get; set; } = new List<PhotoLike>();
        /// <summary>
        /// A comment collection relative to photo
        /// </summary>
		public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        // METHODS
        #region to string option
        /// <summary>
        /// Gets brief information about entity
        /// </summary>
        /// <returns>Brief information about entity</returns>
        protected override string GetBriefInfo()
        {
            return nameof(Photo);
        }
        /// <summary>
        /// Gets entity name
        /// </summary>
        /// <returns>Entity's name</returns>
        protected override string GetName()
        {
            return nameof(Photo);
        }
        #endregion
    }
}
