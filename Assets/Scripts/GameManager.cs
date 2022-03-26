using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IActorListener
{
    public ActorList actorList;
    public StageList stageList;

    private int readyActor;

    private Trainer trainer;

    private float trainingTimeLimit = 120;
    [SerializeField]
    private float trainingTime;
    private bool IsTraining;

    //debugging
    private int generationCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        trainer = new Trainer();

        foreach(Actor actor in actorList.Items)
        {
            List<IActorListener> listz = actor.listeners;
            listz.Add(this);
        }
        readyActor = 0;

        //ResetStage();
        IsTraining = true;
        trainingTime = 0;
        PlayStage();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsTraining)
        {
            trainingTime += Time.deltaTime;
            if(trainingTime > trainingTimeLimit)
            {
                print("Training Time Limit Reached!");
                NewGeneration();
            }
        }
    }

    public void NewGeneration()
    {
        readyActor = 0;
        trainingTime = 0;
        PauseStage();
        TrainActor();
        ResetStage();
        PlayStage();
    }

    private void PauseStage()
    {
        foreach(Stage stage in stageList.Items)
        {
            stage.PauseStage();
        }
    }

    private void TrainActor()
    {
        List<List<float>> dataList = new List<List<float>>();
        List<float> scoreList = new List<float>();

        // get brain + score data
        foreach (Actor actor in actorList.Items)
        {
            dataList.Add(actor.brain.GetParameter());
            scoreList.Add(actor.GetScore());
        }


        //debugging
        generationCounter++;
        float maxScore = scoreList[0];
        int maxIndex = 0;
        if (scoreList.Count > 0)
        {
            for (int i = 0; i < scoreList.Count; i++)
            {
                if (maxScore < scoreList[i]) maxScore = scoreList[i];
                maxIndex = i;
            }
        }
        print("Generation " + generationCounter + ": " + maxScore);

        // train brains
        //print("===");
        //print("BEFORE");
        //DebugData(dataList);
        List<List<float>> newDataList = trainer.TrainGeneticAlgorithm(dataList, scoreList);
        //print("AFTER");
        //DebugData(newDataList);
        //print("===");

        // apply brain
        for (int i = 0; i < actorList.Items.Count; i++)
        {
            actorList.Items[i].brain.SetParameter(newDataList[i]);
            actorList.Items[i].ResetScore();
        }
    }

    private void ResetStage()
    {
        foreach (Stage stage in stageList.Items)
        {
            stage.ResetStage();
        }
    }

    private void PlayStage()
    {
        foreach (Stage stage in stageList.Items)
        {
            stage.PlayStage();
        }
    }

    public void OnActorReset(Actor actor)
    {
        readyActor++;
        if(readyActor == actorList.Items.Count)
        {
            NewGeneration();
        }
    }

    private void DebugData(List<List<float>> dataList)
    {
        string message;

        foreach (List<float> data in dataList)
        {
            message = "(";
            bool firstBit = true;
            foreach (float bit in data)
            {
                if (firstBit)
                {
                    firstBit = false;
                }
                else
                {
                    message += ",";
                }
                message += bit;
            }
            message += ")";
            print(message);
        }
    }
}
