namespace DataAccess.Structs
{
    /// <summary>
    /// Contains likes and dislikes to photo
    /// <para/>
    /// Has been used as a return parameter for <see cref="Repositories.PhotoRepository.GetLikeDislikeAmount(Entities.Photo)"/>
    /// </summary>
    public class LikeDislikeAmount
    {
        /// <summary>
        /// Likes amount
        /// </summary>
        public int LikesAmount { get; set; }
        /// <summary>
        /// Dislike amount
        /// </summary>
        public int DisLikesAmount { get; set; }
    }
}
