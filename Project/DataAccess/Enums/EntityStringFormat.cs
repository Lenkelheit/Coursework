namespace DataAccess.Enums
{
    /// <summary>
    /// Sets format to entity method <see cref="Entities.EntityBase.ToString(EntityStringFormat)"/>
    /// </summary>
    public enum EntityStringFormat
    {
        /// <summary>
        /// Entity's name
        /// </summary>
        Name,
        /// <summary>
        /// Default ToString method
        /// </summary>
        ToString,
        /// <summary>
        /// Brief info about entity
        /// </summary>
        BriefInfo
    }
}
