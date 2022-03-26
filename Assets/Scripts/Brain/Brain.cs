using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain
{
    private int[] defaultNetworkStructrue = { 3,2,1};
    private List<List<Perceptron>> NeuralNetwork;

    public List<float> Process(List<float> input)
    {
        //Set Input
        for (int i = 0; i < input.Count; i++)
        {
            NeuralNetwork[0][i].RawValue = input[i];
        }

        //Get Output
        List<float> output = new List<float>();
        for (int i = 0; i < NeuralNetwork[NeuralNetwork.Count -1].Count; i++)
        {
            output.Add((float)NeuralNetwork[NeuralNetwork.Count - 1][i].GetTotalValue());
        }
        return output;
    }

    public List<float> GetParameter()
    {
        List<float> parameterList = new List<float>();
        foreach (List<Perceptron> layer in NeuralNetwork)
        {
            foreach (Perceptron perceptron in layer)
            {
                foreach (float weight in perceptron.InputWeight)
                {
                    parameterList.Add(weight);
                }
                parameterList.Add((float)perceptron.Bias);
            }
        }
        return parameterList;
    }

    public void SetParameter(List<float> input)
    {
        Queue<float> param = new Queue<float>(input);
        for (int i = 0; i < NeuralNetwork.Count; i++)
        {
            for (int j = 0; j < NeuralNetwork[i].Count; j++)
            {
                for (int k = 0; k < NeuralNetwork[i][j].InputWeight.Count; k++)
                {
                    NeuralNetwork[i][j].InputWeight[k] = param.Dequeue();
                }
                NeuralNetwork[i][j].Bias = param.Dequeue();
            }
        }
    }

    public void NeuralNetworkSetup(int[] neuralStructure)
    {
        NeuralNetwork = new List<List<Perceptron>>();
        for (int i = 0; i < neuralStructure.Length; i++)
        {
            List<Perceptron> layer = new List<Perceptron>();
            for (int j = 0; j < neuralStructure[i]; j++)
            {
                if (i > 0)
                {
                    layer.Add(new Perceptron(0, NeuralNetwork[i - 1]));
                }
                else
                {
                    layer.Add(new Perceptron());
                }
            }
            NeuralNetwork.Add(layer);
        }
    }
}
