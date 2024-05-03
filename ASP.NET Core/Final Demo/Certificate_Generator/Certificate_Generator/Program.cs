namespace Certificate_Generator
{
    /// <summary>
    /// Program Class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main Method Entry Point of the Program
        /// </summary>
        /// <param name="args">Args Array</param>
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
               .Build()
               .Run();
        }
    }
}
