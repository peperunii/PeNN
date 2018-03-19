using System;

namespace PeNN.Activations
{
    class ActivationStep : IActivation
    {
        private float threshold;
        public ActivationType Type => ActivationType.Step;

        public ActivationStep(float threshold)
        {
            this.threshold = threshold;
        }

        public float CalculateOutput(float input)
        {
            return (float)Convert.ToDouble(input > this.threshold);
        }
    }
}
