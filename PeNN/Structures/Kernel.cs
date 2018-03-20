using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeNN.Structures
{
    public class Kernel
    {
        private Random rnd;

        public float[,] data;
        private int kernelSize;
        
        private float amplitude;
        public bool isSumBelowAmplitude;

        public Kernel(int kernelSize, float maxKernelAmplitude = 1)
        {
            this.data = new float[kernelSize, kernelSize];
            this.kernelSize = kernelSize;
            this.amplitude = maxKernelAmplitude;

            this.isSumBelowAmplitude = true;
            this.rnd = new Random();
        }

        public bool IsSimilar(Kernel kernel2)
        {
            var result = true;

            for(int i = 0; i < this.kernelSize; i++)
            {
                for(int j = 0; j < this.kernelSize; j++)
                {
                    if(this.data[i, j] != kernel2.data[i, j])
                    {
                        result = false;
                        break;
                    }
                }

                if (!result) break;
            }

            return result;
        }

        public void Generate()
        {
            while (true)
            {
                for (int i = 0; i < this.kernelSize; i++)
                {
                    for (int j = 0; j < this.kernelSize; j++)
                    {
                        var sign = rnd.Next(2) == 0 ? 1 : -1;

                        this.data[i, j] = (float)(sign * (rnd.Next((int)(1000 * this.amplitude)) / 1000.0));
                    }
                }

                if (!isSumBelowAmplitude || SumIsBelowAverage()) break;
            }
        }

        private bool SumIsBelowAverage()
        {
            var result = true;

            var sum = 0.0;

            for (int i = 0; i < this.kernelSize; i++)
            {
                for (int j = 0; j < this.kernelSize; j++)
                {
                    sum += this.data[i, j];   
                }
            }

            if(Math.Abs(sum) > this.amplitude)
            {
                result = false;
            }

            return result;
        }

        public ConvolutionKernelF GetEmguKernel()
        {
            var emguKernel = new ConvolutionKernelF(this.kernelSize, this.kernelSize);

            for (int i = 0; i < this.kernelSize; i++)
            {
                for (int j = 0; j < this.kernelSize; j++)
                {
                    emguKernel.Data[i, j] = this.data[i, j];
                }
            }

            return emguKernel;
        }
    }
}
