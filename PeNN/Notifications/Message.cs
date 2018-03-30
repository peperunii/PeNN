using System;
using System.Collections.Generic;
using System.Text;

namespace PeNN.Notifications
{
    public class Message
    {
        private Dictionary<Errors, string> ErrorMessage;

        private Dictionary<Warning, string> WarningMessage;

        public Message()
        {
            ErrorMessage = new Dictionary<Errors, string>();
            WarningMessage = new Dictionary<Warning, string>();

            ErrorMessage.Add(Errors.TrainingDataEmpty, "No data provided for training !");
            ErrorMessage.Add(Errors.TrainingShapeNotSame, "Difference in the size of the training data !");
            ErrorMessage.Add(Errors.MissingInputLayer, "Missing Input Layer! ");
        }

        public string this[Errors i] => ErrorMessage[i];
        public string this[Warning i] => WarningMessage[i];
    }
}
