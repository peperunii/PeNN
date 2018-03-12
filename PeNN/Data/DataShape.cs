using System;

namespace PeNN.Data
{
    public class DataShape
    {
        public int NumberOfDimensions;
        public int Width;
        public int Height;

        public DataShape()
        {
            this.NumberOfDimensions = 0;
            this.Width = 0;
            this.Height = 0;
        }

        public DataShape(int dim, int width, int height)
        {
            this.NumberOfDimensions = dim;
            this.Width = width;
            this.Height = height;
        }

        public bool IsShapeEqual(DataShape shape2)
        {
            var result = true;

            if (this.Width != shape2.Width ||
                this.Height != shape2.Height ||
                this.NumberOfDimensions != shape2.NumberOfDimensions)
            {
                result = false;
            }

            return result;
        }

        internal DataShape Clone()
        {
            var dataShape = new DataShape(this.NumberOfDimensions, this.Width, this.Height);

            return dataShape;
        }
    }
}
