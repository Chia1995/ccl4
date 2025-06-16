// This script is responsible for spawning ground tiles in the game.
// It instantiates ground tiles at specified spawn points and optionally spawns items like obstacles and coins on them.
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject groundTile;
    private Vector3 nextSpawnPoint;

    // Spawns a ground tile at the next spawn point
    // If spawnItems is true, spawn obstacles and coins on the tile
    public void SpawnTile(bool spawnItems)
    {
        GameObject tileInstance = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        // Update the next spawn point based on the second child of the tile
        nextSpawnPoint = tileInstance.transform.GetChild(1).position;

        var groundTileComponent = tileInstance.GetComponent<GroundTile>();

        if (spawnItems)
        {
            groundTileComponent.SpawnObstacle();
            groundTileComponent.SpawnCoins();
        }
    }

    private void Start()
    {
        // Spawn initial ground tiles, first 3 without items, rest with items
        for (int i = 0; i < 10; i++)
        {
            bool spawnItems = i >= 3;
            SpawnTile(spawnItems);
        }
    }
}

