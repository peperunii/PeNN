using System;
using System.Collections.Generic;
using PeNN.Data;
using PeNN.Structures;
using PeNN.Activations;

namespace PeNN.Layers
{
    public class LayerInput : Layer
    {
        public LayerInput(
            DataShape size, 
            ActivationType activationType = ActivationType.RelU) 
            : base(LayerType.Input, 1, size, new ActivationRelu())
        {
            this.LayerName = "Input";
            
            this.dataShape = size;
            this.AddNeurons();
        }

        public override DataShape GetOutputShape()
        {
            return this.dataShape;
        }

        public override List<Info> Process(Info info)
        {
            /*The Input Layer does not change the information*/
            var listOutput = new List<Info>();
            listOutput.Add(info);

            return listOutput;
        }
    }
}
