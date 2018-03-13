using PeNN.Activations;
using PeNN.Algorithms;
using PeNN.Data;
using PeNN.Layers;
using PeNN.Notifications;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace PeNN.Networks
{
    public class DeepNN : Network
    {
        private bool isLogFileSet;
        private string logFilename;
        private LogLevel logLevel;

        private Random rnd;
        /*Real list of layers -ex: when user is adding Convo Layer and as an input, we have 10 images -> we actually create 10 convo layers*/
        internal List<Layer> actualLayersList;

        public DeepNN(Size dataSize)
        {
            this.rnd = new Random();
            this.isLogFileSet = false;
            this.actualLayersList = new List<Layer>();
        }

        public void SetLogFile(string logFilename, bool useTimeInfo = false)
        {
            this.isLogFileSet = true;
            this.logFilename = logFilename;

            if (useTimeInfo)
            {
                var timeStamp = DateTime.Now.ToFileTimeUtc();
                this.logFilename = logFilename + "_" + timeStamp + ".txt";
            }

            File.Create(this.logFilename);
        }

        public void AddLayer(
            LayerType layerType,
            int layerOrder,
            DataShape shape = null,
            /*Convolution parameters*/
            int convolution_numberOfKernels = 64,
            int convolution_kernelSize = 3,
            bool convolution_preserveSizeAfterConvolution = true,
            /*Pooling parameters*/
            PoolingType poolingType = PoolingType.Max,
            /*UnPooling Layer parameters*/
            UnPoolingType unPoolingType = UnPoolingType.ExactLocation,
            /*Fully Connected parameters*/
            int fullyConnected_numberOfNeurons = 512,
            ActivationType activationType = ActivationType.RelU,
            float activationThreshold = 0.5f)
        {
            /*Validate if Input layer exist. This layer is added manually - before adding any other layers. It's important to define the Size of the Data. Input layer should with order = 1*/
            if (layerType == LayerType.Input)
            {
                /*Shape should be specified when calling 'AddLayer' function*/
                this.layers.Add(new LayerInput(shape));
            }
            else if (ValidateInputLayer() && layerOrder > 1)
            {
                /*Get previous layer Datashape*/
                var previousLayerShape = (from t in this.layers
                                          where t.layerOrder == layerOrder - 1
                                          select t.dataShape).FirstOrDefault();

                /*Get number of inputs*/
                var numberOfItems_previousLayer = GetnumberOfInputs(layerOrder);
                /*Get previous layer dataShape*/

                /*Add layer -connect neurons with synapses from previous layer to the current one*/
                for (int i = 0; i < numberOfItems_previousLayer; i++)
                {
                    switch (layerType)
                    {
                        case LayerType.Convolution2D:
                            this.layers.Add(
                                new LayerConvolution(
                                    layerOrder,
                                    convolution_numberOfKernels,
                                    convolution_kernelSize,
                                    convolution_preserveSizeAfterConvolution,
                                    previousLayerShape,
                                    activationType,
                                    activationThreshold));
                            break;

                        case LayerType.Pooling:
                            /*Construct new Pooling layer*/
                            break;
                    }
                }
            }
        }

        private bool ValidateInputLayer()
        {
            var result = true;

            if(this.layers[0].layerType != LayerType.Input)
            {
                result = false;
            }

            if (!result)
            {
                Log("Missing Input Layer! ", LogLevel.Important);
            }

            return result;
        }

        public void AddLayer(Layer layer)
        {
            this.layers.Add(layer);
        }

        public override void Train(
            List<Info> data,
            int numbEpochs,
            float learningRate,
            LearningType learningType,
            bool dataShuffleAfterEachEpoch,
            LogLevel loglevel)
        {
            this.logLevel = loglevel;

            Log("Training start", LogLevel.Information);

            if (ValidateData(data))
            {
                for (int epochIndex = 0; epochIndex < numbEpochs; epochIndex++)
                {
                    Log(string.Format("Epoch: {0}", epochIndex), LogLevel.Information);

                    switch(learningType)
                    {
                        case LearningType.BackPropagation:
                            //BackPropagation.NetUpdate(this, data);
                            break;
                    }
                    
                    if(dataShuffleAfterEachEpoch && (epochIndex < numbEpochs - 1))
                        data = this.ShuffleData(data);
                }

                Log("Training complete", LogLevel.Information);
            }
        }
        
        private List<Info> ShuffleData(List<Info> data)
        {
            return data.OrderBy(a => this.rnd.Next()).ToList();
        }

        public override Info Test(Info data)
        {
            throw new NotImplementedException();
        }


        /*TODO: File format, import and export all Network properties, layers and weights*/
        public override void Load(string filename)
        {
            throw new NotImplementedException();
        }

        public override void Save(string filename)
        {
            throw new NotImplementedException();
        }


        private bool ValidateData(List<Info> dataList)
        {
            var result = true;

            if(dataList.Count == 0)
            {
                result = false;
                Log(
                    MapNotificationToMessage.mapErrorToMessage[Errors.TrainingDataEmpty], 
                    LogLevel.Important);
                return result;
            }

            var firstData = dataList[0];
            foreach(var data in dataList)
            {
                if(!firstData.Shape.IsShapeEqual(data.Shape))
                {
                    result = false;
                    Log(
                        MapNotificationToMessage.mapErrorToMessage[Errors.TrainingShapeNotSame],
                        LogLevel.Important);
                    return result;
                }
            }

            return result;
        }

        private int GetnumberOfInputs(int currentLayerOrder)
        {
            var inputsCount = 0;

            var layersWithSmallerOrder = (from layer in this.layers
                                          where layer.layerOrder == currentLayerOrder - 1
                                          select layer).ToList();

            foreach (var input in layersWithSmallerOrder)
            {
                inputsCount += input.neurons.Count();
            }

            return inputsCount;
        }

        private void Log(string msg, LogLevel msgLogLevel)
        {
            if (this.logLevel != LogLevel.Nothing)
            {
                if (this.logLevel == msgLogLevel || this.logLevel == LogLevel.Everything)
                {
                    if (!this.isLogFileSet)
                    {
                        Console.WriteLine(msg);
                    }
                    else
                    {
                        var timeInfo = DateTime.Now.ToString();
                        File.AppendAllLines(this.logFilename, new List<string>() { timeInfo + " : " + msg });
                    }
                }
            }
        }

        public string GetNetworkInfo()
        {
            var info = string.Empty;

            info += "Input: " + this.layers[0].neurons.Count + "\n";
            info += "Number of Layers: " + this.layers.Count + "\n";
            info += "Output: " + this.layers[this.layers.Count - 1].neurons.Count + "\n";
            info += ".........\n";

            return info;
        }
    }
}
