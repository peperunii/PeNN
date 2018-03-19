using System;

namespace PeNN.Activations
{
    class ActivationRelu : IActivation
    {
        public ActivationType Type => ActivationType.RelU;

        public float CalculateOutput(float input)
        {
            return (float)Math.Max(0, input);
        }
    }
}
