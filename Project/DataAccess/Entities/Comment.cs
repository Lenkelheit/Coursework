namespace DataAccess.Entities
{
	public class Comment
	{
		public int Id { get; set; }
		public User User { get; set; }
        	public Photo Photo { get; set; }
		public System.Collections.Generic.ICollection<CommentLike> Likes { get; set; }
		public string Text { get; set; }
		public System.DateTime Date { get; set; }
	}
}
