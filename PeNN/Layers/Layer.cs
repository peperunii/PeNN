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

        public List<Neuron[,]> neurons;
        public string layerName;
        public int layerOrder;
        public IActivation activationFunc;

        public DataShape dataShape;

        public Layer()
        {
            
        }

        public Layer(
            LayerType type, 
            int order,
            DataShape dataSize, 
            ActivationType activationType, 
            float activationThreshold = 0.5f)
        {
            this.layerType = type;
            this.dataShape = dataSize;

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
            this.neurons = new List<Neuron[,]>();

            this.layerName = this.layerType + "_" + order;
            //this.AddNeurons();
        }

        public void AddNeurons()
        {
            var neuronArray = new Neuron[this.dataShape.Height, this.dataShape.Width];

            for (int i = 0; i < this.dataShape.Height; i++)
            {
                for (int j = 0; j < this.dataShape.Width; j++)
                {
                    neuronArray[i, j] = new Neuron(this.activationFunc);
                }
            }

            this.neurons.Add(neuronArray);
        }

        public abstract List<Info> Process(Info info);

        public abstract DataShape GetOutputShape();
    }
}
