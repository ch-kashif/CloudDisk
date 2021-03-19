using log4net;
using Newtonsoft.Json;
using System;

namespace CloudDisk.Logger
{
    public class LogManager : ILogger
    {
        private readonly ILog logger;

        public LogManager()
        {
            log4net.Config.XmlConfigurator.Configure();

            logger = log4net.LogManager.GetLogger("CloudDiskLogger");

        }

        public void Debug(string message)
        {
            Debug(message, null, null);
        }

        public void Debug(string message, params object[] parameters)
        {
            Debug(message, null, parameters);
        }

        public void Debug(string message, Exception ex, params object[] parameters)
        {
            BindParameters(parameters);
            if (ex != null)
                logger.Debug(message, ex);
            else
                logger.Debug(message);
        }

        public void Error(string message)
        {
            Error(message, null, null);
        }

        public void Error(string message, params object[] parameters)
        {
            Error(message, null, parameters);
        }

        public void Error(string message, Exception ex, params object[] parameters)
        {
            BindParameters(parameters);
            if (ex != null)
                logger.Error(message, ex);
            else
                logger.Error(message);
        }

        public void Fatal(string message)
        {
            Fatal(message, null, null);
        }

        public void Fatal(string message, params object[] parameters)
        {
            Fatal(message, null, parameters);
        }

        public void Fatal(string message, Exception ex, params object[] parameters)
        {
            BindParameters(parameters);
            if (ex != null)
                logger.Fatal(message, ex);
            else
                logger.Fatal(message);
        }

        public void Info(string message)
        {
            Info(message, null, null);
        }

        public void Info(string message, params object[] parameters)
        {
            Info(message, null, parameters);
        }

        public void Info(string message, Exception ex, params object[] parameters)
        {
            BindParameters(parameters);
            if (ex != null)
                logger.Info(message, ex);
            else
                logger.Info(message);
        }

        public void Warn(string message)
        {
            Warn(message, null, null);
        }

        public void Warn(string message, params object[] parameters)
        {
            Warn(message, null, parameters);
        }

        public void Warn(string message, Exception ex, params object[] parameters)
        {
            BindParameters(parameters);
            if (ex != null)
                logger.Warn(message, ex);
            else
                logger.Warn(message);
        }

        private void BindParameters(object[] parameters)
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
