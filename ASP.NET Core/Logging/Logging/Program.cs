using NLog.Web;

namespace Logging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>();
                 })
                 .ConfigureLogging(logging =>
                 {
                     logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                 })
                 .UseNLog()
                 .Build()
                 .Run();
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}
