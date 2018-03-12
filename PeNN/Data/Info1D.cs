using System;
using System.Collections.Generic;
using System.Text;

namespace PeNN.Data
{
    public class Info1D : Info
    {
        public List<Data1D> Data;

        /*Empty structure - fill in all fields later*/
        public Info1D()
        {
            this.Data = new List<Data1D>();
            this.Shape = new DataShape();
        }


        public Info1D(string csvFilename)
        {
            this.Data = new List<Data1D>();
            this.Shape = new DataShape();
        }
    }
}
