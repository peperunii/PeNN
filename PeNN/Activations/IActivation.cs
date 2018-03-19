namespace PeNN.Activations
{
    public interface IActivation
    {        
        ActivationType Type { get; }
        float CalculateOutput(float input);
    }
}
