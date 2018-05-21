using System;
using System.Collections.Generic;
using System.Text;

namespace PeNN.Data
{
    public class Data<T>
    {
        private readonly Tensor<T> _data;
        private DataShape _shape;

        public Tensor<T> GetData()
        {
            return this._data;
        }

        public Data(int dimensions, int rows, int columns)
        {
            this._data = new Tensor<T>(dimensions, rows, columns);
            this._shape = new DataShape(dimensions, columns, rows);
        }

        public void Resize(int dimensions, int rows, int columns)
        {
            /*Change dimensions*/
            if (dimensions > this._shape.NumberOfDimensions)
            {
                for (int k = 0; k < dimensions - this._shape.NumberOfDimensions; k++)
                {
                    this._data.AddDimension();
                }
            }
            if (dimensions < this._shape.NumberOfDimensions)
            {
                for (int k = 0; k < this._shape.NumberOfDimensions - dimensions; k++)
                {
                    this._data.RemoveDimension();
                }
            }

            this._shape = new DataShape(dimensions, columns, rows);
        }

        public Scalar<T> this[int dimension, int row, int column]
        {
            get => this._data[dimension][row][column];

            set => this._data[dimension][row][column] = value;
        }
    }
}
