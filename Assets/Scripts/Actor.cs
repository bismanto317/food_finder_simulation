using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField]
    private float score;

    private List<float> scoreList = new List<float>();
    private float lastDistance = 0;
    private float lastPointTime = 0;
    public GameEvent OnPointUpdated;
    public ActorList actorList;
    public bool IsInAction = false;

    public Sensor sensor;
    public Brain brain;
    public Controller controller;

    private float partialScore = 0.1f;
    private float initialScore = 10;
    private float resetTime = 15;

    public List<IActorListener> listeners = new List<IActorListener>();

    private void Awake()
    {
        score = initialScore;
        actorList.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        brain = new Brain();
        brain.NeuralNetworkSetup(new int[] { 2, 3, 2 });
    }

    // Update is called once per frame
    void Update()
    {
        if(IsInAction)
        {
            if(lastPointTime > resetTime)
            {
                IsInAction = false;
                ActorReset();
            }
            else if (sensor.closestFood != null)
            {
                //partial reward, gets rewarded when get close to food
                float foodDistance = sensor.GetFoodDistance();
                if(foodDistance < lastDistance)
                {
                    score += partialScore * Time.deltaTime;
                }
                else
                {
                    score -= partialScore * Time.deltaTime;
                    if(score<0)
                    {
                        score = 0;
                    }
                }
                lastDistance = foodDistance;

                lastPointTime += Time.deltaTime;
                if (sensor.GetData().Count > 0)
                {
                    DoAction(brain.Process(sensor.GetData()));
                }
            }
            
        }
    }

    private void DoAction(List<float> decision)
    {
        if (decision[0] > 0.5)
        {
            controller.MoveNorth((decision[0] - 0.5f) * 2);
        }
        else
        {
            controller.MoveSouth(decision[0] * 2);
        }

        if (decision[1] > 0.5)
        {
            controller.MoveEast((decision[1] - 0.5f) * 2);
        }
        else
        {
            controller.MoveWest(decision[1] * 2);
        }
        //print("Decision: " + decision[0] + "," + decision[1]);
    }

    //=============== Point System ===============
    public void AddScore(int x)
    {
        score += x;
        lastPointTime = 0;
        OnPointUpdated.Raise();
    }

    public void ResetScore()
    {
        score = initialScore;
        lastPointTime = 0;
        scoreList.Clear();
    }

    private void SaveScore()
    {
        scoreList.Add(score);
    }

    private void ResetScoreList()
    {
        scoreList.Clear();
    }

    public float GetAverageScore()
    {
        float sum = 0;
        int qty = 0;
        foreach (float n in scoreList)
        {
            sum += n;
            qty++;
        }
        return sum / qty;
    }

    public float GetScore()
    {
        return score;
    }

    public void ActorReset()
    {
        foreach(IActorListener listener in listeners)
        {
            listener.OnActorReset(this);
        }
    }
}
