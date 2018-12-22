namespace DataAccess.Entities
{
    public abstract class LikeBase
    {
        public int Id { get; set; }
        public User User { get; set; }
        public bool IsLiked { get; set; }
    }
}
