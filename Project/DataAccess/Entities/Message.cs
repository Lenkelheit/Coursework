using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Core.Configuration.DBConfig;

namespace DataAccess.Entities
{
    /// <summary>
    /// Maps to Messages table 
    /// <para/>
    /// Messages are sent to the Admin
    /// </summary>
    public class Message : EntityBase
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override System.Guid Id { get; set; }
        /// <summary>
        /// A text of the message
        /// </summary>
        [MinLength(ADMIN_MESSAGE_MIN_LENGTH)]
        [MaxLength(ADMIN_MESSAGE_MAX_LENGTH)]
        public string Text { get; set; }
        /// <summary>
        /// A user that wrote a comment
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// A subject of the message
        /// </summary>
        public virtual Subject Subject { get; set; }
        /// <summary>
        /// A date, when message was sent
        /// </summary>
        public System.DateTime Date { get; set; } = System.DateTime.Now;


        #region  to string option
        /// <summary>
        /// Gets brief information about entity
        /// </summary>
        /// <returns>Brief information about entity</returns>
        protected override string GetBriefInfo()
        {
            return string.Concat(nameof(Message), " with text : ", Text.Substring(startIndex: 0, length: 20));
        }
        /// <summary>
        /// Gets entity name
        /// </summary>
        /// <returns>Entity's name</returns>
        protected override string GetName()
        {
            return nameof(Message);
        }
        #endregion
    }
}
