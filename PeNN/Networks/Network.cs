using PeNN.Algorithms;
using PeNN.Data;
using PeNN.Layers;
using PeNN.Notifications;
using System.Collections.Generic;

namespace PeNN.Networks
{
    public abstract class Network
    {
        public float learningRate;
        public List<Layer> layers;

        public Network()
        {
            this.layers = new List<Layer>();
        }

        public abstract void Train(
            List<Info> data, 
            int numbEpochs, 
            float learningRate, 
            LearningType learningType, 
            bool dataShuffleAfterEachEpoch, 
            LogLevel logLevel);

        public abstract Info Test(Info data);

        public abstract void Load(string filename);

        public abstract void Save(string filename);
        
    }
}
