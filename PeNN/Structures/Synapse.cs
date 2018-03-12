using System;
using System.Collections.Generic;
using System.Text;

namespace PeNN.Structures
{
    public class Synapse
    {
        public float weight;
        public float previousWeight;

        public Neuron fromNeuron;
        public Neuron toNeuron;

        public Synapse(Neuron fromNeuron, Neuron toNeuron, float weight)
        {
            this.fromNeuron = fromNeuron;
            this.toNeuron = toNeuron;

            this.weight = weight;
            this.previousWeight = 0;
        }

        public Synapse(Neuron fromNeuron, Neuron toNeuron, Random rnd)
        {
            this.fromNeuron = fromNeuron;
            this.toNeuron = toNeuron;

            this.weight = (float)(rnd.Next(1000) / 1000.0); ;
            this.previousWeight = 0;
        }
    }
}
