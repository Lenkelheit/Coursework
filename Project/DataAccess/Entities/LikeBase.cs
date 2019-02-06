using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    /// <summary>
    /// An abstract class for <see cref="CommentLike"/> and <see cref="PhotoLike"/>
    /// </summary>
    public abstract class LikeBase : EntityBase
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        [Key, DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public override System.Guid Id { get; set; } // Guide because TPC
        /// <summary>
        /// A user that has set the like
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// Define is it like or dislike
        /// </summary>
        public bool IsLiked { get; set; }

        // METHODS
        #region  to string option
        /// <summary>
        /// Gets brief information about entity
        /// </summary>
        /// <returns>Brief information about entity</returns>
        protected override string GetBriefInfo()
        {
            return "Like";
        }
        /// <summary>
        /// Gets entity name
        /// </summary>
        /// <returns>Entity's name</returns>
        protected override string GetName()
        {
            return "Like";
        }
        #endregion
    }
}
