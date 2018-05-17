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
        public LayerType LayerType;

        public List<Neuron[,]> Neurons;
        public string LayerName;
        public int LayerOrder;
        public IActivation ActivationFunc;

        public DataShape dataShape;

        public Layer(
            LayerType type, 
            int order,
            DataShape dataSize, 
            IActivation function)
        {
            this.LayerType = type;
            this.dataShape = dataSize;            
            this.LayerOrder = order;
            this.Neurons = new List<Neuron[,]>();

            this.LayerName = this.LayerType + "_" + order;
        }

        public void AddNeurons()
        {
            var shape = GetOutputShape();
            var neuronArray = new Neuron[this.dataShape.Height, this.dataShape.Width];

            for (int i = 0; i < this.dataShape.Height; i++)
            {
                for (int j = 0; j < this.dataShape.Width; j++)
                {
                    neuronArray[i, j] = new Neuron(this.ActivationFunc);
                }
            }

            this.Neurons.Add(neuronArray);
        }

        public abstract List<Info> Process(Info info);

        public abstract DataShape GetOutputShape();
    }
}
