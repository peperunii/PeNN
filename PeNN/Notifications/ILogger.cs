using System;
using System.Collections.Generic;
using System.Text;

namespace PeNN.Notifications
{
    public interface ILogger
    {
        void Log(string message, LogLevel level);
        void LogError(Errors type);
        void LogWarning(Warning type);
    }
}
