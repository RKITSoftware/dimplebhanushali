using System.Diagnostics;

namespace Logging_API
{
    public class Program
    {
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
