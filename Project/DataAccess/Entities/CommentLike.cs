namespace DataAccess.Entities
{
    /// <summary>
    /// Maps tp comment likes table
    /// </summary>
	public class CommentLike : LikeBase
	{
        /// <summary>
        /// A comment to which like has been set
        /// </summary>
		public Comment Comment { get; set; }
	}
}
