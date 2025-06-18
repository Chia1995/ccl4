// This script spawns life mushrooms in the game at specified intervals when the player's life is less than 3.
using UnityEngine;

public class LifeMushroomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject lifeMushroomPrefab; // Reference to the life mushroom prefab
    [SerializeField] private float spawnInterval = 30f; // Time between spawn attempts
    [SerializeField] private float minX = -2f; // Min X position for spawning
    [SerializeField] private float maxX = 2f; // Max X position
    [SerializeField] private float minZOffset = 20f; // Min Z distance ahead of player
    [SerializeField] private float maxZOffset = 40f; // Max Z distance ahead
    [SerializeField] private float yPosition = 4f; // Fixed Y position
    [SerializeField] private float checkRadius = 1f; // Radius to check for obstacles
    [SerializeField] private LayerMask obstacleLayer; // Layer for obstacles

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        InvokeRepeating(nameof(TrySpawnLifeMushroom), 10f, spawnInterval); // Start after 10s
    }



    private void TrySpawnLifeMushroom()
    {
        if (player == null || lifeMushroomPrefab == null) return;

        // Only spawn if player has less than 3 lives (hitCount > 0)
        if (GameManager.Instance != null && GameManager.Instance.HitCount > 0)
        {
            for (int attempts = 0; attempts < 10; attempts++) // Try up to 10 times
            {
                float randomX = Random.Range(minX, maxX);
                float randomZ = Random.Range(minZOffset, maxZOffset);
                Vector3 spawnPosition = new Vector3(randomX, yPosition, player.position.z + randomZ);

                Collider[] colliders = Physics.OverlapSphere(spawnPosition, checkRadius, obstacleLayer);

                foreach (var collider in colliders)
                {
                    Debug.Log($"Collider found: {collider.gameObject.name} at position {collider.transform.position}");
                }

                // Check if the spawn position overlaps with obstacles
                if (!Physics.CheckSphere(spawnPosition, checkRadius, obstacleLayer))
                {
                    Instantiate(lifeMushroomPrefab, spawnPosition, Quaternion.identity);
                    return;
                }
            }

            Debug.LogWarning("Could not find a valid life mushroom spawn position.");
        }
    }

}