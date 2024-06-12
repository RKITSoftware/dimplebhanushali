using NLog;
using NLog.Targets;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Resume_Builder.Helpers
{
    public class UserIdLogWrapper
    {
        private readonly Logger _logger;

        public UserIdLogWrapper(Logger logger)
        {
            _logger = logger;
        }

        public void Log(LogLevel level, HttpContext context, string message)
        {
            var logEvent = new LogEventInfo(ConvertLogLevel(level), _logger.Name, message);

            var userId = context.GetUserIdFromClaims();
            if (userId > 0)
            {
                var config = LogManager.Configuration;
                var fileTarget = config.FindTargetByName<FileTarget>("traceFile");
                if (fileTarget != null)
                {
                    fileTarget.FileName = $"${{currentdir}}/log/{userId}/log.txt";
                    LogManager.ReconfigExistingLoggers();
                }
            }

            _logger.Log(logEvent);
        }

        private NLog.LogLevel ConvertLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return NLog.LogLevel.Trace;
                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case LogLevel.Information:
                    return NLog.LogLevel.Info;
                case LogLevel.Warning:
                    return NLog.LogLevel.Warn;
                case LogLevel.Error:
                    return NLog.LogLevel.Error;
                case LogLevel.Critical:
                    return NLog.LogLevel.Fatal;
                default:
                    return NLog.LogLevel.Info;
            }
        }
    }
}
