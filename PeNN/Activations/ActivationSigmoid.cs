using System;

namespace PeNN.Activations
{
    class ActivationSigmoid : Activation
    {
        private float coeficient;

        public ActivationSigmoid(float coeficient)
        {
            this.coeficient = coeficient;
            this.activationType = ActivationType.Sigma;
        }

        public override float CalculateOutput(float input)
        {
            return (float)(1 / (1 + Math.Exp(-input * coeficient)));
        }
    }
}
