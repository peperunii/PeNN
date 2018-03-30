using PeNN.Algorithms;
using PeNN.Data;
using PeNN.Layers;
using PeNN.Notifications;
using System.Collections.Generic;

namespace PeNN.Networks
{
    public interface INeuralNetwork
    {
        float LearningRate { get; set; }
        List<Layer> Layers { get; set; }        

        void Train(
            List<Info> data, 
            int numbEpochs, 
            float learningRate, 
            LearningType learningType, 
            bool dataShuffleAfterEachEpoch, 
            LogLevel logLevel);

        Info Test(Info data);
    }
}
