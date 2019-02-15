namespace Core.Enums
{
    /// <summary>
    /// Specify test mode fot DataBase
    /// </summary>
    public enum DataBaseFillMode
    {
        /// <summary>
        /// Low amount of known data 
        /// </summary>
        Regular,
        /// <summary>
        /// Low amount of random data
        /// </summary>
        Easy,
        /// <summary>
        /// Middle amount of random data
        /// </summary>
        Middle,
        /// <summary>
        /// Big amount of random data
        /// </summary>
        Hard
    }
}
