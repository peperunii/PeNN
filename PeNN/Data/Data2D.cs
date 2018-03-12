namespace PeNN.Data
{
    public class Data2D
    {
        public float max;
        public int Width;
        public int Height;

        public float[,] Data;

        public Data2D(float maxVal, int width, int height)
        {
            this.max = maxVal;
            this.Data = new float[height, width];
            this.Height = height;
            this.Width = width;
        }

        public void Add(float val, int X, int Y)
        {
            this.Data[Y, X] = val / this.max;
        }

        public float GetData(int X, int Y)
        {
            return this.Data[Y, X];
        }
    }
}
