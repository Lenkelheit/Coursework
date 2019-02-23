namespace Core.Exceptions
{
    /// <summary>
    /// Has been used if comparing type if not allowed
    /// </summary>
    [System.Serializable]
    public class ComparingException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ComparingException"/>
        /// </summary>
        public ComparingException(): base() { }
        /// <summary>
        /// Initialize a new instance of <see cref="ComparingException"/>
        /// </summary>
        /// <param name="compareType">
        /// The name of comparing type which cause exception
        /// </param>
        /// <param name="entityType">
        /// The name of entity for each comparing type is not allowed
        /// </param>
        public ComparingException(string compareType, string entityType) : base(string.Format(Messages.Error.Exceptions.WRONG_COMPARING_TYPE_FORMAT, compareType, entityType)) { }
        /// <summary>
        /// Initializes a new instance of <see cref="ComparingException"/> with error message
        /// </summary>
        /// <param name="message">
        /// An error message
        /// </param>
        public ComparingException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of <see cref="ComparingException"/> with error message and inner exception
        /// </summary>
        /// <param name="message">
        /// An error message
        /// </param>
        /// <param name="inner">
        /// An inner error
        /// </param>
        public ComparingException(string message, System.Exception inner) : base(message, inner) { }
        /// <summary>
        /// Initializes a new instance of <see cref="ComparingException"/> with serialized data
        /// </summary>
        /// <param name="info">
        /// Holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// Contains contextual information about the source or destination.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="info"/> is null
        /// </exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="System.Exception.HResult"/> is zero (0)
        /// </exception>
        protected ComparingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
