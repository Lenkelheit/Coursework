namespace Core
{
    public static class Logger
    {
        public static void Log(string message)
        {
            System.IO.File.AppendAllText(Configuration.AppConfig.LOG_FILE, message);
        }
    }
}
