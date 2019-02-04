namespace DataAccess.Filters
{
    /// <summary>
    /// Consists of algorithms for filtering <see cref="Entities.Comment"/>
    /// </summary>
    public static class CommentFilter
    {
        /// <summary>
        /// Determines whether <see cref="Entities.Comment"/> is suitable for current criteria
        /// <para/>
        /// Has substring in text message
        /// </summary>
        /// <param name="comment">
        /// An instance of <see cref="Entities.Comment"/> to check
        /// </param>
        /// <param name="textSubstring">
        /// A subsring in <see cref="Entities.Comment.Text"/> to find
        /// </param>
        /// <returns>
        /// True if <see cref="Entities.Comment"/> is suitable for current criteria, otherwise — false
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when <paramref name="comment"/> or <paramref name="textSubstring"/> is null;
        /// </exception>
        public static bool Has(Entities.Comment comment, string textSubstring)
        {
            if (comment == null) throw new System.ArgumentNullException(nameof(comment));
            if (textSubstring == null) throw new System.ArgumentNullException(nameof(textSubstring));

            return comment.Text.Contains(textSubstring);
        }
        /// <summary>
        /// Determines whether <see cref="Entities.Comment"/> is suitable for current criteria
        /// <para/>
        /// Is date in range
        /// </summary>
        /// <param name="comment">
        /// An instance of <see cref="Entities.Comment"/> to check
        /// </param>
        /// <param name="from">
        /// A minimum value for <see cref="Entities.Comment.Date"/>
        /// </param>
        /// <param name="to">
        /// A maximum value for <see cref="Entities.Comment.Date"/>
        /// </param>
        /// <returns>
        /// True if <see cref="Entities.Comment"/> is suitable for current criteria, otherwise — false
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when <paramref name="comment"/> is null;
        /// </exception>
        public static bool Where(Entities.Comment comment, System.DateTime? from, System.DateTime? to)
        {
            if (comment == null) throw new System.ArgumentNullException(nameof(comment));

            bool pass = true;

            if (from != null) pass &= comment.Date > from.Value;
            if (to != null) pass &= comment.Date < to.Value;

            return pass;
        }
        /// <summary>
        /// Determines whether <see cref="Entities.Comment"/> is suitable for current criteria
        /// <para/>
        /// Was written by current user
        /// </summary>
        /// <param name="comment">
        /// An instance of <see cref="Entities.Comment"/> to check
        /// </param>
        /// <param name="user">
        /// A user that has wrote <see cref="Entities.Comment"/>
        /// </param>
        /// <returns>
        /// True if <see cref="Entities.Comment"/> is suitable for current criteria, otherwise — false
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when <paramref name="comment"/> or <paramref name="user"/> is null;
        /// </exception>
        public static bool Where(Entities.Comment comment, Entities.User user)
        {
            if (comment == null) throw new System.ArgumentNullException(nameof(comment));
            if (user == null) throw new System.ArgumentNullException(nameof(user));

            return comment.User == user;
        }
        /// <summary>
        /// Determines whether <see cref="Entities.Comment"/> is suitable for current criteria
        /// <para/>
        /// Was written by current user
        /// </summary>
        /// <param name="comment">
        /// An instance of <see cref="Entities.Comment"/> to check
        /// </param>
        /// <param name="userSubstringName">
        /// A user substring nickname
        /// </param>
        /// <returns>
        /// True if <see cref="Entities.Comment"/> is suitable for current criteria, otherwise — false
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when <paramref name="comment"/> or <paramref name="userSubstringName"/> is null;
        /// </exception>
        public static bool Where(Entities.Comment comment, string userSubstringName)
        {
            if (comment == null) throw new System.ArgumentNullException(nameof(comment));
            if (userSubstringName == null) throw new System.ArgumentNullException(nameof(userSubstringName));

            return comment.User.NickName.Contains(userSubstringName);
        }
    }
}
