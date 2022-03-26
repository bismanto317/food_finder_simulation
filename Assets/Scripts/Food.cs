using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public GameEvent OnFoodEaten;
    public FoodList FoodList;
    public FoodSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        FoodList.Add(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Actor actor = other.gameObject.GetComponent<Actor>();
        if (actor != null)
        {
            actor.AddScore(20);
            OnFoodEaten.Raise();
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        FoodList.Remove(this);
        spawner.localFoodList.Remove(this);
    }
}
