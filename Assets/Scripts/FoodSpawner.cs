using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public IntVariable maxFoodQuantity;

    public bool IsSpawning;
    public List<Food> localFoodList;

    // Start is called before the first frame update
    void Start()
    {
        localFoodList = new List<Food>();
        IsSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSpawning)
        {
            SpawnInZone();
        }
    }
        

    void SpawnInZone()
    {
        while (this.gameObject.transform.childCount < maxFoodQuantity.Value)
        {
            GameObject spawnArea = this.gameObject;
            float minX = spawnArea.transform.position.x - spawnArea.transform.localScale.x / 2;
            float maxX = spawnArea.transform.position.x + spawnArea.transform.localScale.x / 2;
            float minZ = spawnArea.transform.position.z - spawnArea.transform.localScale.z / 2;
            float maxZ = spawnArea.transform.position.z + spawnArea.transform.localScale.z / 2;
            float posY = spawnArea.transform.position.y;
            Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), posY, Random.Range(minZ, maxZ));
            GameObject newObject = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
            newObject.transform.parent = this.gameObject.transform;
            Food newFood = newObject.GetComponent<Food>();
            newFood.spawner = this;
            localFoodList.Add(newFood);
        }
    }

    public void ClearFood()
    {
        foreach(Food food in localFoodList)
        {
            Destroy(food.gameObject);
        }
    }
}
