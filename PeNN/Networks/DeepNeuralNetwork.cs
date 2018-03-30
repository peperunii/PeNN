using PeNN.Activations;
using PeNN.Algorithms;
using PeNN.Data;
using PeNN.Layers;
using PeNN.Notifications;
using PeNN.Structures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace PeNN.Networks
{
    public class DeepNeuralNetwork : INeuralNetwork
    {
        public float LearningRate { get; set; }
        public List<Layer> Layers { get; set; }

        private ILogger logger { get; }

        private Random rnd;
        /*Real list of Layers -ex: when user is adding Convo Layer and as an input, we have 10 images -> we actually create 10 convo Layers*/
        internal List<Layer> actualLayersList;

        public DeepNeuralNetwork()
        {
            this.rnd = new Random();
            this.actualLayersList = new List<Layer>();
        }

        public void AddLayer(
            LayerType layerType,
            int layerOrder = -1,
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

            /*Validate if Input layer exist. This layer is added manually - before adding any other Layers. It's important to define the Size of the Data. Input layer should with order = 1*/
            if (layerType == LayerType.Input)
            {
                /*Shape should be specified when calling 'AddLayer' function*/
                this.Layers.Add(new LayerInput(shape));
            }
            else if (ValidateInputLayer())
            {
                /*If layer order is not provided -get last layer and increase with 1*/
                if(layerOrder == -1)
                {
                    layerOrder =  this.Layers[this.Layers.Count - 1].layerOrder + 1;
                }

                /*Get previous layer Datashape*/
                var previousLayershape = (from t in this.Layers
                                          where t.layerOrder == layerOrder - 1
                                          select t.GetOutputShape()).FirstOrDefault();

                /*Get number of inputs*/
                var inputs = this.GetInputs(layerOrder);
                var numberOfItems_previousLayer = inputs.Count;//GetnumberOfInputs(layerOrder);
                /*Get previous layer dataShape*/

                /*Add layer -connect neurons with synapses from previous layer to the current one*/
                for (int i = 0; i < numberOfItems_previousLayer; i++)
                {
                    Layer currentLayer = null;
                    switch (layerType)
                    {
                        case LayerType.Convolution2D:
                            currentLayer = 
                                new LayerConvolution(
                                    layerOrder,
                                    convolution_numberOfKernels,
                                    convolution_kernelSize,
                                    convolution_preserveSizeAfterConvolution,
                                    previousLayershape,
                                    activationType,
                                    activationThreshold);
                            break;

                        case LayerType.Pooling:
                            /*Construct new Pooling layer*/
                            break;
                    }

                    this.ConnectLayerToPrevious(inputs[i], currentLayer);
                    this.Layers.Add(currentLayer);
                }
            }
        }

        private void ConnectLayerToPrevious(Layer previousLayer, Layer currentLayer)
        {
            /*Generate mapping between Layers - calculate their Shapes*/
            var inputShape = previousLayer.GetOutputShape();
            var outputShape = currentLayer.GetOutputShape();


        }

        private bool ValidateInputLayer()
        {
            var result = true;

            if(this.Layers[0].layerType != LayerType.Input)
            {
                result = false;
            }

            if (!result)
            {
                logger.LogError(Errors.MissingInputLayer);
            }

            return result;
        }

        public void AddLayer(Layer layer)
        {
            this.Layers.Add(layer);
        }

        public void Train(
            List<Info> data,
            int numbEpochs,
            float learningRate,
            LearningType learningType,
            bool dataShuffleAfterEachEpoch,
            LogLevel loglevel = LogLevel.Information)
        {       
            logger.Log("Training start", LogLevel.Information);

            if (ValidateData(data))
            {
                for (int epochIndex = 0; epochIndex < numbEpochs; epochIndex++)
                {
                    logger.Log(string.Format("Epoch: {0}", epochIndex), LogLevel.Information);

                    switch(learningType)
                    {
                        case LearningType.BackPropagation:
                            //BackPropagation.NetUpdate(this, data);
                            break;
                    }
                    
                    if(dataShuffleAfterEachEpoch && (epochIndex < numbEpochs - 1))
                        data = this.ShuffleData(data);
                }

                logger.Log("Training complete", LogLevel.Information);
            }
        }
        
        private List<Info> ShuffleData(List<Info> data)
        {
            return data.OrderBy(a => this.rnd.Next()).ToList();
        }

        public Info Test(Info data)
        {
            throw new NotImplementedException();
        }


        private bool ValidateData(List<Info> dataList)
        {
            var result = true;

            if(dataList.Count == 0)
            {
                result = false;
                logger.LogError(Errors.TrainingDataEmpty);
                return result;
            }

            var firstData = dataList[0];
            foreach(var data in dataList)
            {
                if(!firstData.Shape.IsShapeEqual(data.Shape))
                {
                    result = false;
                    logger.LogError(Errors.TrainingShapeNotSame);
                    return result;
                }
            }

            return result;
        }

        private int GetnumberOfInputs(int currentLayerOrder)
        {
            var inputsCount = 0;

            var LayersWithSmallerOrder = (from layer in this.Layers
                                          where layer.layerOrder == currentLayerOrder - 1
                                          select layer).ToList();

            foreach (var input in LayersWithSmallerOrder)
            {
                inputsCount += input.neurons.Count();
            }

            return inputsCount;
        }

        private List<Layer> GetInputs(int currentLayerOrder)
        {
            var LayersWithSmallerOrder = (from layer in this.Layers
                                          where layer.layerOrder == currentLayerOrder - 1
                                          select layer).ToList();

            //var neuronsList = new List<List<Neuron[,]>>();
            //foreach (var input in LayersWithSmallerOrder)
            //{
            //    neuronsList.Add(input.neurons);
            //}

            return LayersWithSmallerOrder;
        }

        public string GetNetworkInfo()
        {
            var info = string.Empty;

            info += "Input: " + this.Layers[0].neurons.Count + "\n";
            info += "Number of Layers: " + this.Layers.Count + "\n";
            info += "Output: " + this.Layers[this.Layers.Count - 1].neurons.Count + "\n";
            info += ".........\n";

            return info;
        }
    }
}
