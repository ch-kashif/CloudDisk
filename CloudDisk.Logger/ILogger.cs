using System;

namespace CloudDisk.Logger
{
    public interface ILogger
    {
        void Debug(string message);
        void Debug(string message, params object[] parameters);
        void Debug(string message, Exception ex, params object[] parameters);

        void Info(string message);
        void Info(string message, params object[] parameters);
        void Info(string message, Exception ex, params object[] parameters);

        void Warn(string message);
        void Warn(string message, params object[] parameters);
        void Warn(string message, Exception ex, params object[] parameters);

        void Error(string message);
        void Error(string message, params object[] parameters);
        void Error(string message, Exception ex, params object[] parameters);

        void Fatal(string message);
        void Fatal(string message, params object[] parameters);
        void Fatal(string message, Exception ex, params object[] parameters);
    }
}
