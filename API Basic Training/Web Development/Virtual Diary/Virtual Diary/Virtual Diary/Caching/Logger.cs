using System;
using System.IO;

namespace Virtual_Diary.Logging
{
    /// <summary>
    /// Provides logging functionality to record information, warnings, and errors to a log file.
    /// </summary>
    public static class Logger
    {
        #region Private Members

        // filename
        private static readonly string _logFileName = "logs.txt";

        //File Path
        private static readonly string _logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", _logFileName);

        #endregion

        #region Public Methods

        /// <summary>
        /// Logs informational messages.
        /// </summary>
        /// <param name="message">The information message to be logged.</param>
        public static void LogInfo(string message)
        {
            LogToFile($"INFO: {DateTime.Now} - {message}");
        }

        /// <summary>
        /// Logs warning messages.
        /// </summary>
        /// <param name="message">The warning message to be logged.</param>
        public static void LogWarn(string message)
        {
            LogToFile($"WARN: {DateTime.Now} - {message}");
        }

        /// <summary>
        /// Logs error messages along with exception details.
        /// </summary>
        /// <param name="message">The error message to be logged.</param>
        /// <param name="ex">The exception details associated with the error.</param>
        public static void LogError(string message, Exception ex)
        {
            LogToFile($"ERROR: {DateTime.Now} - {message} - Exception: {ex}");
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Private helper method to write log entries to the file
        /// </summary>
        /// <param name="logEntry">log Entry</param>
        private static void LogToFile(string logEntry)
        {
            try
            {
                using (StreamWriter objSw = File.AppendText(_logFilePath))
                {
                    objSw.WriteLine(logEntry);
                }
            }
            catch (Exception)
            {
                // Handle exception if needed, e.g., write to the console
                Console.WriteLine($"Error writing to log file: {logEntry}");
            }
        }

        #endregion

    }
}
