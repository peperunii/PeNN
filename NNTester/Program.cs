using Emgu.CV;
using Emgu.CV.Structure;
using PeNN.Networks;
using System;
using System.Drawing;

namespace NNTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\Petar\source\repos\PeNN\NNTester\bin\Debug\netcoreapp2.0\";
            var image = new Bitmap(path + "testImage.png");
            var network = new DeepNN();

            /*Define Architecture*/
            network.AddLayer(PeNN.Layers.LayerType.Input, shape: new PeNN.Data.DataShape(3, image.Width, image.Height));
            network.AddLayer(PeNN.Layers.LayerType.Convolution2D);
        }
    }
}
