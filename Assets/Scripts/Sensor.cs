using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public FoodList foodList;
    public Food closestFood;

    [SerializeField]
    private float directionX;
    [SerializeField]
    private float directionZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (foodList.Items.Count>0)
        {
            closestFood = foodList.Items[0];
            float closestDistance = Vector3.Distance(closestFood.gameObject.transform.position, this.gameObject.transform.position);
            foreach (Food food in foodList.Items)
            {
                float checkDistance = Vector3.Distance(food.gameObject.transform.position, this.gameObject.transform.position);
                if (checkDistance < closestDistance)
                {
                    closestFood = food;
                    closestDistance = checkDistance;
                }
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        
        if (closestFood != null)
        {
            Transform target = closestFood.gameObject.transform;
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }

    public List<float> GetData()
    {
        List<float> data = new List<float>();
        if(closestFood != null)
        {
            // --- Attempt 1 ---
            //data.Add(GetFoodDistanceX());
            //data.Add(GetFoodDistanceY());

            // --- Attempt 2 ---
            Vector3 direction = GetFoodDirection();
            directionX = direction.x;
            directionZ = direction.z;
            data.Add(direction.x);
            data.Add(direction.z);
        }
        return data;
    }

    private float GetFoodDistanceX()
    {
        return closestFood.gameObject.transform.position.x - gameObject.transform.position.x;
    }

    private float GetFoodDistanceY()
    {
        return closestFood.gameObject.transform.position.y - gameObject.transform.position.y;
    }

    public float GetFoodDistance()
    {
        Vector3 foodPos = closestFood.gameObject.transform.position;
        Vector3 actorPos = gameObject.transform.position;
        return Vector3.Distance(foodPos,actorPos);
    }

    private Vector3 GetFoodDirection()
    {
        Vector3 foodPos = closestFood.gameObject.transform.position;
        Vector3 actorPos = gameObject.transform.position;
        return Vector3.Normalize(foodPos - actorPos);
    }
}
