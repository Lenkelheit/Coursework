using DataAccess.Entities;

namespace DataAccess.Comparers
{
    /// <summary>
    /// Compares <see cref="User"/>
    /// </summary>
    public class UserComparer : System.Collections.Generic.IComparer<User>
    {
        // FIELDS
        Enums.Comparers.UserCompareType userCompareType;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="User"/>
        /// </summary>
        /// <param name="userCompareType">
        /// Determines how <see cref="User"/> should be compared
        /// </param>
        public UserComparer(Enums.Comparers.UserCompareType userCompareType)
        {
            this.userCompareType = userCompareType;
        }

        // METHODS
        /// <summary>
        /// Compares two instances of <see cref="User"/>
        /// </summary>
        /// <param name="x">A first instance of <see cref="User"/></param>
        /// <param name="y">A second instance of <see cref="User"/></param>
        /// <returns>
        /// 1 if x is greater than y <para/>
        /// -1 if y is greater than x <para/>
        /// 0 if x and y are equal <para/> 
        /// </returns>
        public int Compare(User x, User y)
        {
            switch (userCompareType)
            {
                case Enums.Comparers.UserCompareType.NickName: return x.NickName.CompareTo(y.NickName);
                default: throw new Core.Exceptions.ComparingException(compareType: userCompareType.ToString(), entityType: nameof(User));
            }
        }
    }
}
