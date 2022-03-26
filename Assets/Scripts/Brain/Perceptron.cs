//using System;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Perceptron
{
    private readonly int maxRandomRange = 3;

    public double RawValue
    {
        get
        {
            return Math.Log((1-Value) / Value);
        }
        set
        {
            Value = Sigmoid(value);
        }
    }
    public double Value { get; set; }
    public List<double> InputWeight { get; set; }
    public List<double> WeightUpdate { get; set; }
    public double Bias { get; set; }

    List<Perceptron> inputPerceptron = new List<Perceptron>();
    double biasUpdate = 0;

    public Perceptron(int newValue = 0, List<Perceptron> newInput = null)
    {
        Value = newValue;
        if (newInput != null) SetPerceptron(newInput);
        InputWeight = new List<double>();
        WeightUpdate = new List<double>();
        Bias = UnityEngine.Random.Range((float)-maxRandomRange, (float)maxRandomRange);
        foreach (Perceptron p in inputPerceptron)
        {
            InputWeight.Add(UnityEngine.Random.Range((float)-maxRandomRange, (float)maxRandomRange));
            WeightUpdate.Add(0);
        }
    }

    private double GetRandomDouble(int rangeMin, int rangeMax)
    {
        //System.Random rnd = new System.Random();
        //return rnd.NextDouble() + rnd.Next(-rangeMin, rangeMax - 1);

        return UnityEngine.Random.Range((float)-rangeMin, (float)rangeMax);
    }

    private void SetPerceptron(List<Perceptron> newSet)
    {
        inputPerceptron = newSet;
    }

    public double GetTotalValue()
    {
        if (inputPerceptron.Count > 0)
        {
            double total = 0;
            for (int i = 0; i < inputPerceptron.Count; i++)
            {
                total += inputPerceptron[i].GetTotalValue() * InputWeight[i];
            }
            total += Bias;
            double squishedValue = Sigmoid(total);
            Value = squishedValue;
            return Value;
        }
        else
        {
            return Value;
        }
    }

    private double Sigmoid(double x)
    {

        return 1 / (1 + Math.Exp(-1 * x));
    }

    private double DSigmoid(double x)
    {
        return Sigmoid(x) * (1 - Sigmoid(x));
    }

    public void BackPropagateSetup(double modifier)
    {
        DzDx(modifier * DSigmoid(Value));
    }

    private void DzDx(double modifier)
    {
        biasUpdate += modifier;
        for (int i = 0; i < inputPerceptron.Count; i++)
        {
            WeightUpdate[i] += modifier * inputPerceptron[i].Value;
            inputPerceptron[i].BackPropagateSetup(modifier * InputWeight[i]);
        }
    }

    public void ApplyUpdate()
    {
        Bias += biasUpdate;
        for (int i = 0; i < InputWeight.Count; i++)
        {
            InputWeight[i] += WeightUpdate[i];
        }
        ResetUpdate();
    }

    private void ResetUpdate()
    {
        biasUpdate = 0;
        for (int i = 0; i < InputWeight.Count; i++)
        {
            WeightUpdate[i] = 0;
        }
    }
}