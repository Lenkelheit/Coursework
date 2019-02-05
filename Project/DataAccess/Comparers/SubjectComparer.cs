using DataAccess.Entities;

namespace DataAccess.Comparers
{
    /// <summary>
    /// Compares <see cref="Subject"/>
    /// </summary>
    public class SubjectComparer : System.Collections.Generic.IComparer<Subject>
    {
        // FIELDS
        Enums.Comparers.SubjectCompareType subjectCompareType;
        
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="SubjectComparer"/>
        /// </summary>
        /// <param name="subjectCompareType">
        /// Determines how subject should be compared
        /// </param>
        public SubjectComparer(Enums.Comparers.SubjectCompareType subjectCompareType)
        {
            this.subjectCompareType = subjectCompareType;
        }

        // METHODS
        /// <summary>
        /// Comares two instances of <see cref="Subject"/>
        /// </summary>
        /// <param name="x">A first instance of <see cref="Subject"/></param>
        /// <param name="y">A second instance of <see cref="Subject"/></param>
        /// <returns>
        /// 1 if x is greater than y <para/>
        /// -1 if y is greater than x <para/>
        /// 0 if x and y are equal <para/> 
        /// </returns>
        public int Compare(Subject x, Subject y)
        {
            switch (subjectCompareType)
            {
                case Enums.Comparers.SubjectCompareType.Id: return x.Id.CompareTo(y.Id);
                case Enums.Comparers.SubjectCompareType.MessageAmount: return x.Messages.Count.CompareTo(y.Messages.Count);
                case Enums.Comparers.SubjectCompareType.Name: return x.Name.CompareTo(y.Name);
                default: throw new Core.Exceptions.ComparingException(compareType: subjectCompareType.ToString(), entityType: nameof(Subject));
            }
        }
    }
}
