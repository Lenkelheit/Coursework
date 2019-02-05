namespace DataAccess.Interfaces
{
    /// <summary>
    /// Represents default interface for entities
    /// </summary>
    public interface IEntity : System.IFormattable
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        System.Guid Id { get; set; }

        /// <summary>
        /// Gets entity as a string formated in specific way
        /// </summary>
        /// <param name="entityStringFormat">
        /// Entity format option
        /// </param>
        /// <returns>
        /// Entity represented as string
        /// </returns>
        string ToString(Enums.EntityStringFormat entityStringFormat);
    }
}
