using NLog.Web;

namespace NlogDemo
{
    /// <summary>
    /// The main class of the application, responsible for configuring and starting the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        /// <param name="args">An array of command-line argument strings.</param>
        public static void Main(string[] args)
        {
            // Configure NLog and get a logger instance
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                // Build and run the host
                var host = CreateHostBuilder(args).Build();
                host.Run();
            }
            catch (Exception exception)
            {
                // Log any unhandled exceptions during startup
                logger.Error(exception, "An unhandled exception occurred during application startup.");
                throw;
            }
            finally
            {
                // Ensure NLog resources are released
                NLog.LogManager.Shutdown();
            }
        }

        /// <summary>
        /// Creates and configures a host builder.
        /// </summary>
        /// <param name="args">An array of command-line argument strings.</param>
        /// <returns>An initialized <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Specify the Startup class
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    // Clear default logging providers and set the minimum log level to Trace
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                // Use NLog for logging
                .UseNLog();
    }
}
