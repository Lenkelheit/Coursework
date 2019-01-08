using System.Threading;

namespace Core
{
    /// <summary>
    /// Provides opportunities to log messages
    /// </summary>
    public static class Logger
    {
        // FIELDS
        private static readonly SemaphoreSlim writeLock; // because writing to file occupy process
        // CONSTRUCTORS
        static Logger()
        {
            writeLock = new SemaphoreSlim(initialCount: 1, maxCount: 1);
        }
        // METHODS
        private static void CreateDirectoryIfNotExist()
        {
            System.IO.Directory.CreateDirectory(Configuration.AppConfig.LOG_DIRECTORY);
        }
        /// <summary>
        /// Writes a log to a file 
        /// </summary>
        /// <param name="logMode">
        /// A log level
        /// </param>
        /// <param name="message">
        /// Log message
        /// </param>
        public static void Log(LogMode logMode, string message)
        {
            CreateDirectoryIfNotExist();

            System.IO.File.AppendAllText(
                path: Configuration.AppConfig.LOG_FILE, 
                contents: string.Format(Configuration.AppConfig.LOG_TEMPLATE_FORMAT, System.DateTime.Now, logMode , message) 
                );
        }
        /// <summary>
        /// Writes a log to a file asynchronously
        /// </summary>
        /// <param name="logMode">
        /// A log level
        /// </param>
        /// <param name="message">
        /// Log message
        /// </param>
        public static async void LogAsync(LogMode logMode, string message)
        {
            try
            {
                await writeLock.WaitAsync();

                await System.Threading.Tasks.Task.Run(() => CreateDirectoryIfNotExist());

                await System.Threading.Tasks.Task.Run(() => Log(logMode, message));
            }
            finally
            {
                writeLock.Release();
            }
        }
    }
}
