namespace PeNN.Activations
{
    public abstract class Activation
    {
        public ActivationType activationType;

        public abstract float CalculateOutput(float input);
    }
}
