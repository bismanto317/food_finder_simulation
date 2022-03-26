using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainer
{
    float mutationRate;
    float genotypeMin;
    float genotypeMax;
    int elitismQuantity;
    int completeRandomQuantity;

    public Trainer(float mutationRate = 0, float genotypeMin = -1, float genotypeMax = 1, int elitismQuantity = 1, int completeRandomQuantity = 1)
    {
        this.mutationRate = mutationRate;
        this.genotypeMin = genotypeMin;
        this.genotypeMax = genotypeMax;
        this.elitismQuantity = elitismQuantity;
        this.completeRandomQuantity = completeRandomQuantity;
    }

    public List<List<float>> TrainGeneticAlgorithm(List<List<float>> data, List<float> score)
    {
        List<List<float>> result = new List<List<float>>();

        float maxScore = score[0];
        int maxIndex = 0;
        for (int i = 0; i < score.Count; i++)
        {
            if (maxScore < score[i]) maxScore = score[i];
            maxIndex = i;
        }

        int eliteQty = elitismQuantity;
        int rndQty = completeRandomQuantity;
        for (int i=0; i<data.Count; i++)
        {
            if(eliteQty-- > 0)
            {
                //=== Elitism: pick highest score genome
                result.Add(data[maxIndex]);
            }
            else if (rndQty-- > 0)
            {
                List<float> randomData = new List<float>();
                for (int j = 0; j < data[i].Count; j++)
                {
                    float randomBit = Random.Range(genotypeMin, genotypeMax);
                    randomData.Add(randomBit);
                }
                result.Add(randomData);
            }
            else
            {
                // Complete Crossover: for each genotype, pick either from parentA or parentB
                List<float> parentA = SelectParent(data, score);
                List<float> parentB = SelectParent(data, score);
                List<float> child = CompleteCrossover(parentA, parentB);
                //BUG: child all same!!!
                for(int j=0; j<child.Count; j++)
                {
                    float debugRng1 = Random.Range(0f, 1f);
                    float debugRng2 = Random.Range(genotypeMin, genotypeMax);
                    bool isMutated = debugRng1 < mutationRate;
                    if(isMutated)
                    {
                        child[j] = debugRng2;
                    }
                }
                result.Add(child);
            }
        }

        return result;
    }

    private List<float> SelectParent(List<List<float>> data, List<float> score)
    {
        float randomValue = Random.Range(0f, 1f);

        float totalScore = 0;
        for(int i=0; i<score.Count; i++)
        {
            totalScore += score[i];
        }

        float selectedScore = randomValue * totalScore;
        int selectedIndex = -1;

        totalScore = 0;
        for (int i = 0; i < score.Count; i++)
        {
            totalScore += score[i];
            if(totalScore>=selectedScore)
            {
                selectedIndex = i;
                break;
            }
        }
        return data[selectedIndex];
    }

    private List<float> CompleteCrossover(List<float> parentA, List<float> parentB)
    {
        List<float> result = new List<float>();
        for(int i=0; i<parentA.Count; i++)
        {
            int parentSelection = Random.Range(0, 2); //random integer between 0 or 1
            switch(parentSelection)
            {
                case 0:
                    result.Add(parentA[i]);
                    break;
                case 1:
                    result.Add(parentB[i]);
                    break;
            }
        }

        return result;
    }

    private void DebugData(List<float> data)
    {
        string message = "";
        foreach(float bit in data)
        {
            message += bit + "|";
        }
        //print(message);
    }
}
