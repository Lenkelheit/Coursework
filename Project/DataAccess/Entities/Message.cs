namespace DataAccess.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public Subject Subject { get; set; }
        public System.DateTime Date { get; set; }
    }
}
