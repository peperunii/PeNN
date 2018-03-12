using PeNN.Data;
using PeNN.Networks;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeNN.Algorithms
{
    public static class BackPropagation
    {
        private static Network net;

        static BackPropagation()
        {
            net = null;
        }

        //public static void NetUpdate(Network network, List<Info> data)
        //{
        //    net = network;
        //
        //    for (int j = 0; j < net.inputs.GetLength(0); j++)
        //    {
        //        PushInputValues(inputs[j]);
        //
        //        var outputs = new List<double>();
        //
        //        // Get outputs.
        //        _layers.Last().Neurons.ForEach(x =>
        //        {
        //            outputs.Add(x.CalculateOutput());
        //        });
        //
        //        // Calculate error by summing errors on all output neurons.
        //        totalError = CalculateTotalError(outputs, j);
        //        HandleOutputLayer(j);
        //        HandleHiddenLayers();
        //    }
        //}
        //
        //public void PushInputValues(float[] inputs)
        //{
        //    _layers.First().Neurons.ForEach(x => x.sPushValueOnInput(inputs[_layers.First().Neurons.IndexOf(x)]));
        //}
        //
        //private static double CalculateTotalError(List<double> outputs, int row)
        //{
        //    double totalError = 0;
        //
        //    outputs.ForEach(output =>
        //    {
        //        var error = Math.Pow(output - _expectedResult[row][outputs.IndexOf(output)], 2);
        //        totalError += error;
        //    });
        //
        //    return totalError;
        //}
        //
        ///// <summary>
        ///// Hellper function that runs backpropagation algorithm on the output layer of the network.
        ///// </summary>
        ///// <param name="row">
        ///// Input/Expected output row.
        ///// </param>
        //private static void HandleOutputLayer(int row)
        //{
        //    _layers.Last().Neurons.ForEach(neuron =>
        //    {
        //        neuron.Inputs.ForEach(connection =>
        //        {
        //            var output = neuron.CalculateOutput();
        //            var netInput = connection.GetOutput();
        //
        //            var expectedOutput = _expectedResult[row][_layers.Last().Neurons.IndexOf(neuron)];
        //
        //            var nodeDelta = (expectedOutput - output) * output * (1 - output);
        //            var delta = -1 * netInput * nodeDelta;
        //
        //            connection.UpdateWeight(_learningRate, delta);
        //
        //            neuron.PreviousPartialDerivate = nodeDelta;
        //        });
        //    });
        //}
        //
        ///// <summary>
        ///// Hellper function that runs backpropagation algorithm on the hidden layer of the network.
        ///// </summary>
        ///// <param name="row">
        ///// Input/Expected output row.
        ///// </param>
        //private static void HandleHiddenLayers()
        //{
        //    for (int k = _layers.Count - 2; k > 0; k--)
        //    {
        //        _layers[k].Neurons.ForEach(neuron =>
        //        {
        //            neuron.Inputs.ForEach(connection =>
        //            {
        //                var output = neuron.CalculateOutput();
        //                var netInput = connection.GetOutput();
        //                double sumPartial = 0;
        //
        //                _layers[k + 1].Neurons
        //                .ForEach(outputNeuron =>
        //                {
        //                    outputNeuron.Inputs.Where(i => i.IsFromNeuron(neuron.Id))
        //                    .ToList()
        //                    .ForEach(outConnection =>
        //                    {
        //                        sumPartial += outConnection.PreviousWeight * outputNeuron.PreviousPartialDerivate;
        //                    });
        //                });
        //
        //                var delta = -1 * netInput * sumPartial * output * (1 - output);
        //                connection.UpdateWeight(_learningRate, delta);
        //            });
        //        });
        //    }
        //}
    }
}
