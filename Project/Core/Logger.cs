using System.Threading;

namespace Core
{
    /// <summary>
    /// Provides opportunities to log messages
    /// <para/>
    /// Implements a Singleton pattern
    /// </summary>
    public class Logger
    {
        // FIELDS
        private readonly SemaphoreSlim writeLock; // because writing to file occupy process
        private static Logger instance;

        // CONSTRUCTORS
        private Logger()
        {
            writeLock = new SemaphoreSlim(initialCount: 1, maxCount: 1);
        }
        static Logger()
        {
            instance = new Logger();
        }
        /// <summary>
        /// Release unmanaged resources
        /// </summary>
        ~Logger()
        {
            writeLock.Dispose();
        }
        // PROPERTIES
        /// <summary>
        /// Gets logger instance
        /// </summary>
        public static Logger GetLogger => instance;
        // METHODS
        private void CreateDirectoryIfNotExist()
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
        [System.Obsolete("Core.Logger.Log has been deprecated. Please use other method Core.Logger.LogAsync", false)]
        public void Log(LogMode logMode, string message)
        {
            try
            {
                writeLock.Wait();

                CreateDirectoryIfNotExist();

                System.IO.File.AppendAllText(
                    path: Configuration.AppConfig.LOG_FILE, 
                    contents: string.Format(Configuration.AppConfig.LOG_TEMPLATE_FORMAT, System.DateTime.Now, logMode , message) 
                    );
            }
            finally
            {
                writeLock.Release();
            }
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
        public async void LogAsync(LogMode logMode, string message)
        {
            try
            {
                await writeLock.WaitAsync();

                await System.Threading.Tasks.Task.Run(() => CreateDirectoryIfNotExist());

                await System.Threading.Tasks.Task.Run(() => 
                            System.IO.File.AppendAllText(
                                    path: Configuration.AppConfig.LOG_FILE,
                                    contents: string.Format(Configuration.AppConfig.LOG_TEMPLATE_FORMAT, System.DateTime.Now, logMode, message)
                                    ));
            }
            finally
            {
                writeLock.Release();
            }
        }
    }
}
