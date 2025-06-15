// This script defines a ground tile in a 3D game, which can spawn obstacles and coins when the player exits the tile. It also handles the spawning of different types of obstacles based on a defined chance. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject obstacle2Prefab;
    [SerializeField] float obstacle2SpawnChance = 0.2f; // Chance to spawn an obstacle
    GroundSpawner groundSpawner;
    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        //SpawnObstacle();
        //SpawnCoins();

    }
    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2f); // Optional: Destroy the tile after a delay to avoid clutter
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SpawnObstacle()
    {
        // Choose which obstacle to spawn based on the chance
        GameObject obstacleToSpawn = obstaclePrefab;
        float random = Random.Range(0f, 1f);
        if (random < obstacle2SpawnChance)
        {
            obstacleToSpawn = obstacle2Prefab;
        }

        // Randomly choose a position on the tile to spawn the obstacle
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        // Instantiate the obstacle at the chosen position
        Instantiate(obstacleToSpawn, spawnPoint.position, Quaternion.identity, transform);

    }


    public void SpawnCoins()
    {
        int coinsToSpawn = 10;
        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
        );
        // Ensure the point is within the collider bounds
        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }
}
