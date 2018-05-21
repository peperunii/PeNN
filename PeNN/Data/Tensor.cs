using System;
using System.Collections.Generic;
using System.Linq;

namespace PeNN.Data
{
    public class Tensor<T>
    {
        private readonly List<Matrix<T>> _tensorData;
        private readonly DataShape _shape;

        public Matrix<T> this[int index]
        {
            get => _tensorData[index];

            set => _tensorData[index] = value;
        }

        public Tensor(int dimensions, int rows, int columns)
        {
            _tensorData = new List<Matrix<T>>();
            Enumerable.Repeat<Action>(() => { _tensorData.Add(new Matrix<T>(rows, columns)); }, rows);

            _shape = new DataShape(dimensions, columns, rows);
        }

        public void AddDimension()
        {
            _tensorData.Add(new Matrix<T>(_shape.Height, _shape.Width));
        }

        public void RemoveDimension()
        {
            _tensorData.RemoveAt(_tensorData.Count-1);
        }
    }
}
