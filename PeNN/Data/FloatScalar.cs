namespace PeNN.Data
{
    public class FloatScalar : Scalar<float>
    {
        public FloatScalar(float value) : base(value)
        {
        }

        public override Scalar<float> Divide(Scalar<float> value)
        {
            return new FloatScalar(this.Value / value.Value);
        }

        public override Scalar<float> Multiply(Scalar<float> value)
        {
            return new FloatScalar(this.Value * value.Value);
        }

        public override Scalar<float> Add(Scalar<float> value)
        {
            return new FloatScalar(this.Value + value.Value);
        }

        public override Scalar<float> Negate()
        {
            return new FloatScalar(-this.Value);
        }

        public override Scalar<float> Subtract(Scalar<float> value)
        {
            return new FloatScalar(this.Value - value.Value);
        }
    }
}