using System;

namespace PeNN.Activations
{
    class ActivationSigmoid : IActivation
    {
        private float coeficient;
        public ActivationType Type => ActivationType.Sigma;

        public ActivationSigmoid(float coeficient)
        {
            this.coeficient = coeficient;         
        }

        public float CalculateOutput(float input)
        {
            return (float)(1 / (1 + Math.Exp(-input * coeficient)));
        }
    }
}
