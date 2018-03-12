using System;

namespace PeNN.Activations
{
    class ActivationRelu : Activation
    {
        public ActivationRelu()
        {
            this.activationType = ActivationType.Step;
        }

        public override float CalculateOutput(float input)
        {
            return (float)Math.Max(0, input);
        }
    }
}
