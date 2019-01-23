using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static Core.Configuration.DBConfig;

namespace DataAccess.Entities
{
    /// <summary>
    /// Maps to Subject table
    /// <para/>
    /// Has been used as a subject to admin message
    /// </summary>
    public class Subject
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Subject's name
        /// </summary>
        [MinLength(ADMIN_MESSAGE_SUBJECT_MIN_LENGTH)]
        [MaxLength(ADMIN_MESSAGE_SUBJECT_MAX_LENGTH)]
        public string Name { get; set; }
        /// <summary>
        /// A collection of messages with current subject
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
