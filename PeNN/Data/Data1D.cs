using System;
using System.Collections.Generic;
using System.Text;

namespace PeNN.Data
{
    public class Data1D
    {
        public double max;

        public List<double> Data;

        public Data1D(double maxVal)
        {
            this.max = maxVal;
            this.Data = new List<double>();
        }

        public void Add(double val)
        {
            this.Data.Add(val / this.max);
        }
    }
}
