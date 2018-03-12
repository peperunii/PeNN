using System;

namespace PeNN.Activations
{
    class ActivationStep : Activation
    {
        private float threshold;

        public ActivationStep(float threshold)
        {
            this.threshold = threshold;
            this.activationType = ActivationType.Step;
        }

        public override float CalculateOutput(float input)
        {
            return (float)Convert.ToDouble(input > this.threshold);
        }
    }
}
