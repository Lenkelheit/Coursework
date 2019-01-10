using System.ComponentModel.DataAnnotations;

using static Core.Configuration.DBConfig;

namespace DataAccess.Entities
{
    /// <summary>
    /// Maps to Messages table 
    /// <para/>
    /// Messages are sent to the Admin
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Unique indetifier
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// A text of the message
        /// </summary>
        [MinLength(ADMIN_MESSAGE_MIN_LENGTH)]
        [MaxLength(ADMIN_MESSAGE_MAX_LENGTH)]
        public string Text { get; set; }
        /// <summary>
        /// A user that wrote a comment
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// A subject of the message
        /// </summary>
        public Subject Subject { get; set; }
        /// <summary>
        /// A date, when message was send
        /// </summary>
        public System.DateTime Date { get; set; }
    }
}
