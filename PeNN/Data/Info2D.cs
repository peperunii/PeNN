using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Drawing;
using System.Drawing.Imaging;

namespace PeNN.Data
{
    public class Info2D : Info
    {
        public List<Data2D> Data;

        /*Empty structure - fill in all fields later*/
        public Info2D()
        {
            this.Data = new List<Data2D>();
            this.Shape = new DataShape();

        }

        public Info2D(Image<Gray, Byte> image)
        {
            this.InitGrayImage(image);
        }

        public Info2D(Image<Gray, Byte> image, string label)
        {
            this.InitGrayImage(image);
            this.Information = new Label(label);
        }
        
        public Info2D(Image<Bgr, Byte> image)
        {
            this.InitBgrImage(image);
        }

        public Info2D(Image<Bgr, Byte> image, string label)
        {
            this.InitBgrImage(image);
            this.Information = new Label(label);
        }

        public Info2D(Bitmap image)
        {
            this.InitBgrImage(image);
        }

        public Info2D(Bitmap image, string label)
        {
            this.InitBgrImage(image);
            this.Information = new Label(label);
        }

        public Info2D(string csvFilename)
        {
            this.Data = new List<Data2D>();
            this.Shape = new DataShape();
        }

        private void InitGrayImage(Image<Gray, byte> image)
        {
            var width = image.Width;
            var height = image.Height;
            var imgData = image.Data;

            this.Data = new List<Data2D>();
            this.Shape = new DataShape(1, width, height);
            var data2D = new Data2D(255, width, height);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    data2D.Add(imgData[i, j, 0], j, i);
                }
            }
            this.Data.Add(data2D);
        }

        private void InitBgrImage(Image<Bgr, byte> image)
        {
            var width = image.Width;
            var height = image.Height;
            var imgData = image.Data;

            this.Data = new List<Data2D>();
            this.Shape = new DataShape(3, width, height);

            for (int k = 0; k < 3; k++)
            {
                var data2D = new Data2D(255, width, height);

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        data2D.Add(imgData[i, j, k], j, i);
                    }
                }
                this.Data.Add(data2D);
            }
        }

        private void InitBgrImage(Bitmap image)
        {
            var width = image.Width;
            var height = image.Height;
            
            this.Data = new List<Data2D>();
            this.Shape = new DataShape(3, width, height);

            for (int k = 0; k < 3; k++)
            {
                var data2D = new Data2D(255, width, height);

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        var color = image.GetPixel(j, i);
                        data2D.Add(k == 0 ? color.B : k == 1 ? color.G : color.R, j, i);
                    }
                }
                this.Data.Add(data2D);
            }
        }

        public Info2D Clone()
        {
            var clonnedObject = new Info2D();

            clonnedObject.Information = this.Information.Clone();
            clonnedObject.Shape = this.Shape.Clone();

            clonnedObject.Data = this.Data.ToList();

            return clonnedObject;
        }

        public void ExportInfoToImage(string filename, bool StretchData = false)
        {
            /*In case of BGR image*/
            if (this.Shape.NumberOfDimensions == 3)
            {
                var width = this.Shape.Width;
                var height = this.Shape.Height;

                var bChannel = this.Data[0].Data;
                var gChannel = this.Data[1].Data;
                var rChannel = this.Data[2].Data;

                var imgData = MergeData(bChannel, gChannel, rChannel, width, height);

                var bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);

                /*Find image -min and max and then stretch data*/
                if (StretchData)
                {
                    var min = float.MaxValue;
                    var max = float.MinValue;

                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            /*Min*/
                            if (rChannel[i, j] < min)
                            {
                                min = rChannel[i, j];
                            }
                            if (gChannel[i, j] < min)
                            {
                                min = gChannel[i, j];
                            }
                            if (bChannel[i, j] < min)
                            {
                                min = bChannel[i, j];
                            }
                            /*Max*/
                            if (rChannel[i, j] > max)
                            {
                                max = rChannel[i, j];
                            }
                            if (gChannel[i, j] > max)
                            {
                                max = gChannel[i, j];
                            }
                            if (bChannel[i, j] > max)
                            {
                                max = bChannel[i, j];
                            }
                        }
                    }

                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            rChannel[i, j] -= min;
                            gChannel[i, j] -= min;
                            bChannel[i, j] -= min;

                            rChannel[i, j] *= (255 / (max - min));
                            gChannel[i, j] *= (255 / (max - min));
                            bChannel[i, j] *= (255 / (max - min));
                        }
                    }
                }

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        var r = rChannel[i, j];
                        var g = gChannel[i, j];
                        var b = bChannel[i, j];

                        var intR = (int)(r < 0 ? 0 : r > 255 ? 255 : r);
                        var intG = (int)(g < 0 ? 0 : g > 255 ? 255 : g);
                        var intB = (int)(b < 0 ? 0 : b > 255 ? 255 : b);

                        var color = Color.FromArgb(intR, intG, intB);
                        bitmap.SetPixel(j, i, color);
                    }
                }

                bitmap.Save(filename);
            }
        }

        private float[,,] MergeData(float[,] bChannel, float[,] gChannel, float[,] rChannel, int width, int height)
        {
            var mergedData = new float[height, width, 3];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    mergedData[i, j, 0] = bChannel[i, j];
                    mergedData[i, j, 1] = gChannel[i, j];
                    mergedData[i, j, 2] = rChannel[i, j];
                }
            }

            return mergedData;
        }
    }
}
