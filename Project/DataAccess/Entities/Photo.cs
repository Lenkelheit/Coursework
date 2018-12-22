using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Photo
    {
		public int Id { get; set; }
		public string Path { get; set; }
		public User User { get; set; }
		public ICollection<LikeBase> Likes { get; set; }
		public ICollection<Comment> Comments { get; set; }
    }
}
