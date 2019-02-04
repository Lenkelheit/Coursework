namespace DataAccess.Entities
{
    /// <summary>
    /// Represents abstract class for entities
    /// </summary>
    public abstract class EntityBase : Interfaces.IEntity
    {
        // PROPERTIES
        /// <summary>
        /// Unique identifier
        /// </summary>
        public abstract System.Guid Id { get; set; }

        // METHODS
        /// <summary>
        /// Gets entity as a string formated in specific way
        /// </summary>
        /// <param name="entityStringFormat">
        /// Entity format option
        /// </param>
        /// <returns>
        /// Entity represented as string
        /// </returns>
        public string ToString(Enums.EntityStringFormat entityStringFormat)
        {
            return ToString(entityStringFormat.ToString(), null);
        }
        /// <summary>
        /// Formats the value of the entity using the specified format.
        /// </summary>
        /// <param name="format"> 
        /// The format to use
        /// <para> -or- </para>
        /// A null reference to use the default format defined for the type of the System.IFormattable implementation.
        /// </param>
        /// <param name="formatProvider">
        /// The provider to use to format the value.
        /// <para>-or-</para>
        /// A null reference to obtain the numeric format information from the current locale setting of the operating system.
        /// </param>
        /// <returns>
        /// The value of the entity in the specified format
        /// </returns>
        public string ToString(string format, System.IFormatProvider formatProvider)
        {
            if (string.IsNullOrWhiteSpace(format)) return ToString();

            if (format == Enums.EntityStringFormat.Name.ToString()) return GetName();
            else if (format == Enums.EntityStringFormat.BriefInfo.ToString()) return GetBriefInfo();
            else if (format == Enums.EntityStringFormat.ToString.ToString()) return ToString();

            return ToString();
        }

        #region format method
        /// <summary>
        /// Gets entity name
        /// </summary>
        /// <returns>Entity's name</returns>
        protected abstract string GetName();
        /// <summary>
        /// Gets brief information about entity
        /// </summary>
        /// <returns>Brief information about entity</returns>
        protected abstract string GetBriefInfo();
        #endregion
    }
}
