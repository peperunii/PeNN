using PeNN.Algorithms;
using PeNN.Data;
using PeNN.Layers;
using PeNN.Notifications;
using System;
using System.Collections.Generic;
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

        public DeepNN()
        {
            this.rnd = new Random();
            this.isLogFileSet = false;
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

        public void AddLayer(LayerType layerType, int layerOrder, int layerParam1, int layerParam2)
        {
            switch (layerType)
            {
                case LayerType.Convolution2D:
                    //this.layers.Add(new LayerConvolution(layerOrder, layerParam1, layerParam2));
                    break;

                    /*TODO: Add more layers*/
            }
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

            info += "Input: " + this.layers[0].neurons.Length + "\n";
            info += "Number of Layers: " + this.layers.Count + "\n";
            info += "Output: " + this.layers[this.layers.Count - 1].neurons.Length + "\n";
            info += ".........\n";

            return info;
        }
    }
}
