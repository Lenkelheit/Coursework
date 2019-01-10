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
        public int Id { get; set; }
        /// <summary>
        /// A user that has set the like
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Define is it like or dislike
        /// </summary>
        public bool IsLiked { get; set; }
    }
}
