using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    /// <summary>
    /// An abstract class for <see cref="CommentLike"/> and <see cref="PhotoLike"/>
    /// </summary>
    public abstract class LikeBase
    {
        /// <summary>
        /// Unique indetifier
        /// </summary>
        [Key, DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } // Guide because TPC
        /// <summary>
        /// A user that has set the like
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// Define is it like or dislike
        /// </summary>
        public bool IsLiked { get; set; }
    }
}
