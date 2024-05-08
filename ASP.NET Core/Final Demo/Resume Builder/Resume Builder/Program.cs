using NLog.Web;

namespace Resume_Builder
{
    public class Program
    {
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
