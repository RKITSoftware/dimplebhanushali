using DILifeTime.Interfaces;

namespace DILifeTime
{
    /// <summary>
    /// Entry point for the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method to start the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Resolve services within a scope
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var scopedService = serviceProvider.GetService<IScopedService>();
                if (scopedService != null)
                {
                    Console.WriteLine($"Scoped Service Operation ID: {scopedService.GetOperationID()}");
                }

                var singletonService = serviceProvider.GetRequiredService<ISingletonService>();
                Console.WriteLine($"Singleton Service Operation ID: {singletonService.GetOperationID()}");
            }

            host.Run();
        }

        /// <summary>
        /// Creates a host builder for the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        /// <returns>The configured host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
