using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public List<GameObject> foodList;
    PlayerController player;
    GameObject foodToSpawn;
    public float spawnHeight = 10f;
    public float spawnInterval = 5f; // Baþlangýçta her 5 saniyede bir spawn yapar
    private float timeSinceLastSpawn;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnFoodAboveCamera();
            timeSinceLastSpawn = 0f;
        }

        if(player.skor >= 35)
        {
            spawnInterval = 1.5f;
        }
        else if(player.skor >= 25)
        {
            spawnInterval = 2f;
        }
        else if(player.skor >= 20)
        {
            spawnInterval = 3f;
        }
        else if (player.skor >= 10)
        {
            spawnInterval = 4f;
        }
    }

    void SpawnFoodAboveCamera()
    {
        if (foodList.Count == 0) return;

        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        Vector3 cameraPosition = cam.transform.position;
        float randomX = Random.Range(-camWidth / 2f, camWidth / 2f) + cameraPosition.x;

        Vector3 spawnPosition = new Vector3(randomX, cameraPosition.y + spawnHeight, 1);
        foodToSpawn = foodList[Random.Range(0, foodList.Count)];

        Instantiate(foodToSpawn, spawnPosition, Quaternion.identity);
    }
}
