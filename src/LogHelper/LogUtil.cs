using Serilog;

namespace LogHelper
{
    public static class LogUtil
    {
        public static void Init()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs\\log-.txt",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true)
                .CreateLogger();
        }

        public static void Error(string message) 
        {
            Log.Error(message);
        }
    }
}
