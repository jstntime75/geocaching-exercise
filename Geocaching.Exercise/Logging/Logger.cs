using log4net;
using System;
using System.Reflection;

namespace Geocaching.Exercise.Logging
{
    public static class Logger
    {
        private static readonly Lazy<ILog> _log = new Lazy<ILog>(
            () => LogManager.GetLogger(Assembly.GetCallingAssembly().GetName().Name));

        private static ILog Log
        {
            get
            {
                return _log.Value;
            }
        }

        public static void LogDebug(string message)
        {
            if (Log.IsDebugEnabled)
            {
                Log.Debug(message);
            }
        }

        public static void LogInfo(string message)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Info(message);
            }
        }

        public static void LogWarn(string message)
        {
            if (Log.IsWarnEnabled)
            {
                Log.Warn(message);
            }
        }

        public static void LogError(string message)
        {
            if (Log.IsErrorEnabled)
            {
                Log.Error(message);
            }
        }

        public static void LogFatal(string message)
        {
            if (Log.IsFatalEnabled)
            {
                Log.Fatal(message);
            }
        }
    }
}
