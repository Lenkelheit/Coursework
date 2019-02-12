using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Core.Configuration.DBConfig;

namespace DataAccess.Entities
{
    /// <summary>
    /// Maps to Subject table
    /// <para/>
    /// Has been used as a subject to admin message
    /// </summary>
    public class Subject : EntityBase
    {
        // PROPERTIES
        /// <summary>
        /// Unique identifier
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override System.Guid Id { get; set; }
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

        // METHODS
        #region to string option
        /// <summary>
        /// Gets brief information about entity
        /// </summary>
        /// <returns>Brief information about entity</returns>
        protected override string GetBriefInfo()
        {
            return string.Concat(nameof(Subject), " with name : ", Name);
        }
        /// <summary>
        /// Gets entity name
        /// </summary>
        /// <returns>Entity's name</returns>
        protected override string GetName()
        {
            return nameof(Subject);
        }
        #endregion
    }
}
