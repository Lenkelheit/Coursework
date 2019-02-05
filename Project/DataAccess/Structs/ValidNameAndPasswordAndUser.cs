namespace DataAccess.Structs
{
    /// <summary>
    /// Determines if name and password are valid and by them returns this user
    /// <para/>
    /// Has been used as a return parameter for <see cref="Repositories.UserRepository.IsDataValid(string, string)"/>
    /// </summary>
    public struct ValidNameAndPasswordAndUser
    {
        /// <summary>
        /// Determines if NickName and Password are valid
        /// </summary>
        public ValidNameAndPassword ValidNameAndPassword { get; set; }
        /// <summary>
        /// Determines user by NickName and Password
        /// </summary>
        public Entities.User User { get; set; }
    }
}
