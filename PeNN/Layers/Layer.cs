using Emgu.CV;
using PeNN.Activations;
using PeNN.Data;
using PeNN.Structures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeNN.Layers
{
    public abstract class Layer
    {
        public LayerType layerType;

        public Neuron[,] neurons;
        public string layerName;
        public int layerOrder;
        public Activation activationFunc;

        public Size layerSize;

        public Layer(LayerType type, int order, Size size, ActivationType activationType, float activationThreshold = 0.5f)
        {
            this.layerType = type;
            this.layerSize = size;

            switch (activationType)
            {
                case ActivationType.RelU:
                    this.activationFunc = new ActivationRelu();
                    break;

                case ActivationType.Sigma:
                    this.activationFunc = new ActivationSigmoid(activationThreshold);
                    break;

                case ActivationType.Step:
                    this.activationFunc = new ActivationStep(activationThreshold);
                    break;
            }

            
            this.layerOrder = order;
            this.neurons = new Neuron[size.Height, size.Width];

            this.layerName = this.layerType + "_" + order;
            this.AddNeurons();
        }

        private void AddNeurons()
        {
            for(int i = 0; i < this.layerSize.Height; i++)
            {
                for (int j = 0; j < this.layerSize.Width; j++)
                {
                    this.neurons[i, j] = new Neuron(this.activationFunc);
                }
            }
        }
        public abstract List<Info> Process(Info info);
    }
}
