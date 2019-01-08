namespace Core.Configuration
{
    public static class AppConfig
    {
        internal static readonly string DIRECTORY_SEPARATOR_STR = System.IO.Path.DirectorySeparatorChar.ToString();

        public static readonly string DIRECTORY_EXE_PATH = System.AppDomain.CurrentDomain.BaseDirectory;


        public static readonly string APP_NAME = "Galagram";

        public static readonly string LOG_FILE = null;
        public static readonly string LOG_TEMPLATE_FORMAT = "-{0}- [{1}] \t {2}";

        public static readonly string PHOTOS_SAVE_PATH = null;

    }
}
