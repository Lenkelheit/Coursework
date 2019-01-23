namespace Core.Interfaces
{
    /// <summary>
    /// Represents an interface for Factory Method patern
    /// </summary>
    /// <typeparam name="TKey">
    /// A key type by which creator is registrated
    /// </typeparam>
    /// <typeparam name="TRegValue">
    /// A creator type, which know how to create an instance of the object
    /// </typeparam>
    /// <typeparam name="TReturnValue">
    /// An object type, which instance should be created
    /// </typeparam>
    public interface IFactory<in TKey, in TRegValue, out TReturnValue>
    {
        /// <summary>
        /// Registrate creator by key
        /// </summary>
        /// <param name="key">
        /// A key by creator is registered
        /// </param>
        /// <param name="value">
        /// A concrete creator
        /// </param>
        void Registrate(TKey key, TRegValue value);
        /// <summary>
        /// Unregistrate creator by key
        /// </summary>
        /// <param name="key">
        /// A key by which creator should be unregistered
        /// </param>
        void UnRegistrate(TKey key);
        /// <summary>
        /// Returns an instance of the object by key
        /// </summary>
        /// <param name="key">
        /// A key by which instance of the object should be created
        /// </param>
        /// <returns>
        /// An instance of the object, which creator was registered by current key
        /// </returns>
        TReturnValue MakeInstance(TKey key);
    }
}
