using System;
using System.Collections.Generic;
using System.Text;

namespace PeNN.Notifications
{
    [Flags]
    public enum LogLevel
    {
        Nothing = 0,
        Information = 1,
        Important = 2,
        Everything = 3,
    }
}
