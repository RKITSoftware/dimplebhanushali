using NLog.Web;

namespace Filters_Web_API
{
    /// <summary>
    /// The main class of the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The entry point of the program.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            // Build and run the web host
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates an instance of the web host builder.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        /// <returns>An instance of IHostBuilder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Use the Startup class for configuring the application
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog();
    }
}