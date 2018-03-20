using PeNN.Activations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeNN.Structures
{
    public class Neuron
    {
        public Guid neuronIndex;
        public List<Synapse> inputs;
        public List<Synapse> outputs;

        public float lastOutputResult;

        private IActivation activatonFunc;
        
        public Neuron(IActivation activationFunc)
        {
            this.activatonFunc = activationFunc;
            this.neuronIndex = Guid.NewGuid();

            this.inputs = new List<Synapse>();
            this.outputs = new List<Synapse>();

            this.lastOutputResult = 0;
        }

        public void AddInput(Synapse synapse)
        {
            this.inputs.Add(synapse);
        }

        public void AddOutput(Synapse synapse)
        {
            this.outputs.Add(synapse);
        }

        private float GetInputSum()
        {
            return this.inputs.Select(x => x.weight * x.fromNeuron.lastOutputResult).Sum();
        }

        public float CalculateOutput()
        {
            var input = GetInputSum();
            this.lastOutputResult = this.activatonFunc.CalculateOutput(input);

            return this.lastOutputResult;
        }
    }
}
