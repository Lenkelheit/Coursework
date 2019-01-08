namespace Core.Configuration
{
    /// <summary>
    /// Consists of all important configuration to application.
    /// </summary>
    public static class AppConfig
    {
        /// <summary>
        /// Name of the application.
        /// </summary>
        public static readonly string APP_NAME = "Galagram";

        internal static readonly string DIRECTORY_SEPARATOR_STR = System.IO.Path.DirectorySeparatorChar.ToString();
        /// <summary>
        /// A path to executing file.
        /// </summary>
        public static readonly string DIRECTORY_EXE_PATH = System.AppDomain.CurrentDomain.BaseDirectory;


        internal static readonly string LOG_DIRECTORY = string.Join(DIRECTORY_SEPARATOR_STR, DIRECTORY_EXE_PATH, "Log");
        /// <summary>
        /// A path to a file with logs.
        /// </summary>
        public static readonly string LOG_FILE = string.Join(DIRECTORY_SEPARATOR_STR, LOG_DIRECTORY, "log.txt");
        /// <summary>
        /// Template for logs.
        /// <para/>
        /// Date, logMode, message.
        /// </summary>
        public static readonly string LOG_TEMPLATE_FORMAT = "-{0}- [{1}] \t {2}\n";
        /// <summary>
        /// A path to saved photos.
        /// </summary>
        public static readonly string PHOTOS_SAVE_PATH = null;

    }
}
