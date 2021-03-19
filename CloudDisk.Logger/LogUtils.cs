using log4net;
using Newtonsoft.Json;
using System;

namespace CloudDisk.Logger
{
    public class LogUtils  
    {
        private static readonly ILog logger;

        static LogUtils()
        {
            log4net.Config.XmlConfigurator.Configure();

            logger = log4net.LogManager.GetLogger("CloudDiskLogger");

        }

        public static void Debug(string message)
        {
            Debug(message, null, null);
        }

        public static void Debug(string message, params object[] parameters)
        {
            Debug(message, null, parameters);
        }

        public static void Debug(string message, Exception ex, params object[] parameters)
        {
            BindParameters(parameters);
            if (ex != null)
                logger.Debug(message, ex);
            else
                logger.Debug(message);
        }

        public static void Error(string message)
        {
            Error(message, null, null);
        }

        public static void Error(string message, params object[] parameters)
        {
            Error(message, null, parameters);
        }

        public static void Error(string message, Exception ex, params object[] parameters)
        {
            BindParameters(parameters);
            if (ex != null)
                logger.Error(message, ex);
            else
                logger.Error(message);
        }

        public static void Fatal(string message)
        {
            Fatal(message, null, null);
        }

        public static void Fatal(string message, params object[] parameters)
        {
            Fatal(message, null, parameters);
        }

        public static void Fatal(string message, Exception ex, params object[] parameters)
        {
            BindParameters(parameters);
            if (ex != null)
                logger.Fatal(message, ex);
            else
                logger.Fatal(message);
        }

        public static void Info(string message)
        {
            Info(message, null, null);
        }

        public static void Info(string message, params object[] parameters)
        {
            Info(message, null, parameters);
        }

        public static void Info(string message, Exception ex, params object[] parameters)
        {
            BindParameters(parameters);
            if (ex != null)
                logger.Info(message, ex);
            else
                logger.Info(message);
        }

        public static void Warn(string message)
        {
            Warn(message, null, null);
        }

        public static void Warn(string message, params object[] parameters)
        {
            Warn(message, null, parameters);
        }

        public static void Warn(string message, Exception ex, params object[] parameters)
        {
            BindParameters(parameters);
            if (ex != null)
                logger.Warn(message, ex);
            else
                logger.Warn(message);
        }

        private static void BindParameters(object[] parameters)
        {
            ThreadContext.Properties.Clear();
            if (parameters == null)
                return;
            for (int i = 0; i < parameters.Length; i++)
            {
                ThreadContext.Properties[$"ExtraInfo_{i + 1}"] = JsonConvert.SerializeObject(parameters);
            }
        }
    }
}
