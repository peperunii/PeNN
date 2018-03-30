using System;
using System.Collections.Generic;
using System.Text;

namespace PeNN.Notifications
{
    public class ConsoleLogger : ILogger
    {
        private Message messages = new Message();

        public void Log(string message, LogLevel level)
        {
            Console.WriteLine(message);
        }

        public void LogError(Errors type)
        {
            Log(messages[type], LogLevel.Important);
        }

        public void LogWarning(Warning type)
        {
            Log(messages[type], LogLevel.Information);
        }
    }
}
