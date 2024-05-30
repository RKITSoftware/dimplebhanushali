using NLog.Web;

namespace Resume_Builder
{
    /// <summary>
    /// Program Class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main Entry point of the Program.
        /// </summary>
        /// <param name="args"></param>
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
               .UseNLog()
               .Build()
               .Run();
        }
    }
}
