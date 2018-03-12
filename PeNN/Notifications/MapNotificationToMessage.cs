using System;
using System.Collections.Generic;
using System.Text;

namespace PeNN.Notifications
{
    public static class MapNotificationToMessage
    {
        public static Dictionary<Errors, string> mapErrorToMessage;

        public static Dictionary<Warning, string> mapWarningToMessage;

        static MapNotificationToMessage()
        {
            mapErrorToMessage = new Dictionary<Errors, string>();
            mapWarningToMessage = new Dictionary<Warning, string>();

            mapErrorToMessage.Add(Errors.TrainingDataEmpty, "No data provided for training !");
            mapErrorToMessage.Add(Errors.TrainingShapeNotSame, "Difference in the size of the training data !");
        }
    }
}
