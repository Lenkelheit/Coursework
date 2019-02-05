namespace DataAccess.Enums.Comparers
{
    /// <summary>
    /// Sets comparer type for <see cref="DataAccess.Comparers.SubjectComparer"/>
    /// </summary>
    public enum SubjectCompareType
    {
        /// <summary>
        /// Compares <see cref="Entities.Subject"/> by <see cref="Entities.Subject.Id"/>
        /// </summary>
        Id,
        /// <summary>
        /// Compares <see cref="Entities.Subject"/> by <see cref="Entities.Subject.Name"/>
        /// </summary>
        Name,
        /// <summary>
        /// Compares <see cref="Entities.Subject"/> by <see cref="Entities.Subject.Messages"/> amount
        /// </summary>
        MessageAmount
    }
}
