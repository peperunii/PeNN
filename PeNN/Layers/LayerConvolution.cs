using PeNN.Activations;
using PeNN.Data;
using PeNN.Structures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeNN.Layers
{
    public class LayerConvolution : Layer
    {
        public int numberOfKernels;
        public int kernelSize;

        public List<Kernel> kernels;

        public LayerConvolution(
            int order, 
            int numberOfKernels, 
            int kernelSize, 
            Size layerSize,
            ActivationType activationType = ActivationType.RelU,
            float activationThreshold = 0.5f) : base(LayerType.Convolution2D, order, layerSize, activationType, activationThreshold)
        {
            this.numberOfKernels = numberOfKernels;
            this.kernelSize = kernelSize;

            this.kernels = new List<Kernel>();

            this.Generatekernels();
        }

        public override List<Info> Process(Info input)
        {
            var processedInfo = new List<Info>();

            foreach(var kernel in this.kernels)
            {

            }

            return processedInfo;
        }

        private void Generatekernels()
        {
            for (int i = 0; i < this.numberOfKernels; i++)
            {
                while (true)
                {
                    var kernel = new Kernel(this.kernelSize);
                    kernel.Generate();
                    var isSimilarFound = false;

                    foreach (var ker in this.kernels)
                    {
                        if (ker.IsSimilar(kernel))
                        {
                            isSimilarFound = true;
                            break;
                        }
                    }

                    if (!isSimilarFound)
                    {
                        this.kernels.Add(kernel);
                        break;
                    }
                }

            }
        }

        /*Create Custom Convolution function. The EmguCV is using only images as input, we need universal array as input*/
        private static Info2D ApplyKernel(Info2D info, Kernel kernel)
        {
            var modifiedInfo = info.Clone();
            var semikernelSize = (int)(Math.Sqrt(kernel.data.Length) / 2);
            var kernelData = kernel.data;
            var dstData = modifiedInfo.Data;

            var dataIndex = 0;
            foreach (var data in info.Data)
            {
                var dataHeight = data.Height;
                var dataWidth = data.Width;
                var dataArr = data.Data;

                for (int i = 0; i < dataHeight; i++)
                {
                    for (int j = 0; j < dataWidth; j++)
                    {
                        /*Modify each pixel*/
                        var sum = 0.0f;
                        for (int ki = -semikernelSize; ki <= semikernelSize; ki++)
                        {
                            for (int kj = -semikernelSize; kj <= semikernelSize; kj++)
                            {
                                /*skip the edges*/
                                if ((i + ki < 0 || i + ki > dataHeight - 1) ||
                                    (j + kj < 0 || j + kj > dataWidth - 1))
                                {
                                    continue;
                                }

                                sum += (dataArr[i + ki, j + kj] * kernelData[semikernelSize + ki, semikernelSize + kj]);
                            }
                        }

                        if (sum is float.NaN) sum = 0;
                        dstData[dataIndex].Data[i, j] = sum;
                    }
                }

                dataIndex++;
            }

            return modifiedInfo;
        }
    }
}
