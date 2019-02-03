﻿namespace DataAccess.Filters
{
    /// <summary>
    /// Consists of algorithms for filtering <see cref="Entities.Message"/>
    /// </summary>
    public static class MessageFilter
    {
        /// <summary>
        /// Determines whether <see cref="Entities.Message"/> is suitable for current criteria
        /// <para/>
        /// Is subject name the same
        /// </summary>
        /// <param name="message">
        /// An instance of <see cref="Entities.Message"/> to check
        /// </param>
        /// <param name="subjectName">
        /// An subject name that <see cref="Entities.Message"/> should have to pass criterion
        /// </param>
        /// <returns>
        /// True if <see cref="Entities.Message"/> is suitable for current criteria, otherwise — false
        /// </returns>
        public static bool Where(Entities.Message message, string subjectName)
        {
            if (message == null) throw new System.ArgumentNullException(nameof(message));
            if (subjectName == null) throw new System.ArgumentNullException(nameof(subjectName));

            return message.Subject.Name == subjectName;
        }
        /// <summary>
        /// Determines whether <see cref="Entities.Message"/> is suitable for current criteria
        /// <para/>
        /// Is date in range
        /// </summary>
        /// <param name="message">
        /// An instance of <see cref="Entities.Message"/> to check
        /// </param>
        /// <param name="from">
        /// A minimum value for <see cref="Entities.Message.Date"/>
        /// </param>
        /// <param name="to">
        /// A maximum value for <see cref="Entities.Message.Date"/>
        /// </param>
        /// <returns>
        /// True if <see cref="Entities.Message"/> is suitable for current criteria, otherwise — false
        /// </returns>
        public static bool Where(Entities.Message message, System.DateTime? from, System.DateTime? to)
        {
            if (message == null) throw new System.ArgumentNullException(nameof(message));

            bool ok = true;

            if (from != null) ok &= message.Date > from.Value;
            if (to != null) ok &= message.Date < to.Value;

            return ok;
        }

    }
}
