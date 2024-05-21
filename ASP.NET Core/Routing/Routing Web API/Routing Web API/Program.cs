namespace Routing_Web_API
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
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Use the Startup class for configuring the application
                    webBuilder.UseStartup<Startup>();
                })
                .Build()
                .Run();
        }
    }
}