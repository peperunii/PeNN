using System;
using System.Collections.Generic;
using System.Text;

namespace PeNN.Data
{
    public class Matrix<T>
    {
        private readonly Vector<T>[] _matrixData;

        public Matrix(Vector<T>[] matrixData)
        {
            _matrixData = matrixData;
        }

        public Matrix(int rows, int columns)
        {
            this._matrixData = new Vector<T>[rows * columns];
        }

        public Vector<T> this[int row, int column]
        {
            get => this._matrixData[row * column];

            set => this._matrixData[row * column] = value;
        }

        public Vector<T> this[int index]
        {
            get => this._matrixData[index];

            set => this._matrixData[index] = value;
        }
    }
}
