namespace Core
{
    public static class Logger
    {
        public static void Log(LogMode logMode, string message)
        {
            System.IO.File.AppendAllText(
                path: Configuration.AppConfig.LOG_FILE, 
                contents: string.Format(Configuration.AppConfig.LOG_TEMPLATE_FORMAT, System.DateTime.Now, logMode , message) 
                );
        }
    }
}
