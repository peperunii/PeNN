namespace PeNN.Data
{
    public class Vector<T>
    {
        private readonly Scalar<T>[] _vectorData;

        public Vector(Scalar<T>[] vectorData)
        {
            _vectorData = vectorData;
        }

        public Scalar<T> this[int index]
        {
            get => _vectorData[index];

            set => _vectorData[index] = value;
        }
    }
}
