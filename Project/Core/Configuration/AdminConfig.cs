namespace Core.Configuration
{
    /// <summary>
    /// Consists of all configuration needed to admin panel
    /// </summary>
    public static class AdminConfig
    {
        /// <summary>
        /// Menu items of the admin panel
        /// </summary>
        public static readonly string[] ADMIN_ITEMS = new string[]
            {
                "Message",
                "User",
                "Comments",
                "Subject",
                "Exit"
            };
        /// <summary>
        /// Index of exit button
        /// </summary>
        public static readonly int EXIT_ITEM_INDEX = ADMIN_ITEMS.Length - 1;
    }
}
