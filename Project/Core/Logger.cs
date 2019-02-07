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
        private LogMode offLogMode;
        private readonly System.IO.FileInfo logFileInfo;

        // CONSTRUCTORS
        private Logger()
        {
            writeLock = new SemaphoreSlim(initialCount: 1, maxCount: 1);
            logFileInfo = new System.IO.FileInfo(Configuration.AppConfig.LOG_FILE);

            // To turn off log mode(s) for all application pass it(them) to method "Off".
            // For example, next line turns off Debug mode:
            // Off(LogMode.Debug);
            // For multiple off modes use "|" between them, next line turns off Debug and Info modes:
            // Off(LogMode.Debug | LogMode.Info);
            // To turn on modes that are off, only delete method "Off" with them.
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
        private void Off(LogMode logMode)
        {
            offLogMode = logMode;
        }
        private void CreateDirectoryIfNotExist()
        {
            System.IO.Directory.CreateDirectory(Configuration.AppConfig.LOG_DIRECTORY);
        }
        private void DeleteLogFileIfBig()
        {
            // Deletes log file when it exists and is big enough.
            if (logFileInfo.Exists && logFileInfo.Length > Configuration.AppConfig.LOG_FILE_SIZE_LIMIT) 
            {
                logFileInfo.Delete();
            }
        }
        private void AddMessageToLogFile(LogMode logMode, string message)
        {
            using (System.IO.StreamWriter streamWriter = logFileInfo.AppendText())
            {
                streamWriter.Write(string.Format(Configuration.AppConfig.LOG_TEMPLATE_FORMAT, System.DateTime.Now, logMode, message));
            }
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

                // Executes if logMode isn't off, otherwise - no.
                if (!offLogMode.HasFlag(logMode)) 
                {
                    CreateDirectoryIfNotExist();

                    DeleteLogFileIfBig();

                    AddMessageToLogFile(logMode, message);
                }
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

                // Executes if logMode isn't off, otherwise - no.
                if (!offLogMode.HasFlag(logMode))
                {
                    await System.Threading.Tasks.Task.Run(() => CreateDirectoryIfNotExist());
                  
                    await System.Threading.Tasks.Task.Run(() => DeleteLogFileIfBig());

                    await System.Threading.Tasks.Task.Run(() => AddMessageToLogFile(logMode, message));
                }
            }
            finally
            {
                writeLock.Release();
            }
        }
    }
}
