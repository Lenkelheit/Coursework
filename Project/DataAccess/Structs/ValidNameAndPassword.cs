namespace DataAccess.Structs
{
    /// <summary>
    /// Determines if name and password are valid
    /// <para/>
    /// Has been used in <see cref="Repositories.UserRepository.IsDataValid(string, string)"/>
    /// </summary>
    public struct ValidNameAndPassword
    {
        /// <summary>
        /// Determines if NickName is valid
        /// </summary>
        public bool IsNameValid { get; set; }
        /// <summary>
        /// Determines if Password is valid
        /// </summary>
        public bool IsPasswordValid { get; set; }
    }
}
