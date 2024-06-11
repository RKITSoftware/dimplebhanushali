using System.Diagnostics;

namespace Logging_API
{
    /// <summary>
    /// The main class of the application, responsible for starting the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        /// <param name="args">An array of command-line argument strings.</param>
        public static void Main(string[] args)
        {
            string sourceName = "Logging_API";

            // Configure and run the web host
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddJsonConsole(options =>
                    {
                        options.TimestampFormat = "[HH:mm:ss] ";
                    });

                    logging.AddEventSourceLogger();

                    logging.AddEventLog(options =>
                    {
                        options.SourceName = sourceName;
                        options.LogName = "Application";
                        options.Filter = (category, level) =>
                        {
                            return level >= LogLevel.Warning;
                        };
                    });

                    // Set console colors
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Clear();

                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .Build()
                .Run();

            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(new EventSourceCreationData(sourceName, "Application"));
            }
        }
    }
}
