namespace DataAccess.Filters
{
    /// <summary>
    /// Consists of algorithms for filtering <see cref="Entities.User"/>
    /// </summary>
    public class UserFilter
    {
        /// <summary>
        /// Determines whether <see cref="Entities.User"/> is suitable for current criteria
        /// <para/>
        /// Has substring in his nickname
        /// </summary>
        /// <param name="user">
        /// An instance of <see cref="Entities.User"/> to check
        /// </param>
        /// <param name="substring">
        /// A subsring in <see cref="Entities.User.NickName"/> to find
        /// </param>
        /// <returns>
        /// True if <see cref="Entities.User"/> is suitable for current criteria, otherwise — false
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when <paramref name="user"/> or <paramref name="substring"/> is null;
        /// </exception>
        public static bool Has(Entities.User user, string substring)
        {
            if (user == null) throw new System.ArgumentNullException(nameof(user));
            if (substring == null) throw new System.ArgumentNullException(nameof(substring));

            return user.NickName.Contains(substring);
        }
    }
}
