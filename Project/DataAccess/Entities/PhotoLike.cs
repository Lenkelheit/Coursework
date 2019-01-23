namespace DataAccess.Entities
{
    /// <summary>
    /// Maps to Photo Like table
    /// </summary>
    public class PhotoLike : LikeBase
    {
        /// <summary>
        /// A photo to which like has been set
        /// </summary>
        public virtual Photo Photo { get; set; }
    }
}
