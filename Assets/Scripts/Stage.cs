using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour, IActorListener
{
    public Actor actor;
    public GameObject startingPoint;
    public FoodList foodList;
    public StageList stageList;
    public FoodSpawner foodSpawner;

    void Awake()
    {
        stageList.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        actor.listeners.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartStage()
    {
        PauseStage();
        ResetStage();
        PlayStage();
    }

    public void PauseStage()
    {
        foodSpawner.IsSpawning = false;

        actor.IsInAction = false;
    }

    public void ResetStage()
    {
        foodSpawner.ClearFood();

        actor.ResetScore();
        actor.gameObject.transform.position = startingPoint.transform.position;
        actor.gameObject.transform.rotation = startingPoint.transform.rotation;
    }

    public void PlayStage()
    {
        foodSpawner.IsSpawning = true;
        actor.IsInAction = true;
    }

    public void OnActorReset(Actor actor)
    {
        PauseStage();
    }
}
