// This script spawns magnets at random positions in front of the player, avoiding obstacles.
using UnityEngine;

public class MagnetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject magnetPrefab;
    [SerializeField] private float spawnInterval = 20f;

    [SerializeField] private float minX = -2f;
    [SerializeField] private float maxX = 2f;
    [SerializeField] private float minZOffset = 20f;
    [SerializeField] private float maxZOffset = 40f;
    [SerializeField] private float yPosition = 1f;

    [SerializeField] private float checkRadius = 1f; // How big an area to check for obstacles
    [SerializeField] private LayerMask obstacleLayer; // Set this in Inspector to the layer for obstacles

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        InvokeRepeating(nameof(SpawnMagnet), 5f, spawnInterval);
    }

    private void SpawnMagnet()
    {
        if (player == null || magnetPrefab == null) return;

        for (int attempts = 0; attempts < 10; attempts++) // Try up to 10 times to find a valid position
        {
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZOffset, maxZOffset);
            Vector3 spawnPosition = new Vector3(randomX, yPosition, player.position.z + randomZ);

            // Check if the spawn position overlaps with any obstacle
            if (!Physics.CheckSphere(spawnPosition, checkRadius, obstacleLayer))
            {
                Instantiate(magnetPrefab, spawnPosition, Quaternion.identity);
                return;
            }
        }

        Debug.LogWarning("Could not find a valid magnet spawn position (all overlapped with obstacles).");
    }
}
