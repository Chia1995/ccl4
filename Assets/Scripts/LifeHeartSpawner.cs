// This script spawns life hearts in the game at specified intervals when the player's life is less than 3.
using UnityEngine;

public class LifeHeartSpawner : MonoBehaviour
{
    [SerializeField] private GameObject lifeHeartPrefab; // Reference to the life heart prefab
    [SerializeField] private float spawnInterval = 30f; // Time between spawn attempts
    [SerializeField] private float minX = -2f;
    [SerializeField] private float maxX = 2f;
    [SerializeField] private float minZOffset = 20f;
    [SerializeField] private float maxZOffset = 40f;
    [SerializeField] private float yPosition = 3.5f;
    [SerializeField] private float checkRadius = 1f;
    [SerializeField] private LayerMask obstacleLayer;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        InvokeRepeating(nameof(TrySpawnLifeHeart), 10f, spawnInterval); // Start after 10s
    }

    private void TrySpawnLifeHeart()
    {
        if (player == null || lifeHeartPrefab == null) return;

        // Only spawn if player has less than 3 lives
        if (GameManager.Instance != null && GameManager.Instance.HitCount > 0)
        {
            for (int attempts = 0; attempts < 10; attempts++)
            {
                float randomX = Random.Range(minX, maxX);
                float randomZ = Random.Range(minZOffset, maxZOffset);
                Vector3 spawnPosition = new Vector3(randomX, yPosition, player.position.z + randomZ);

                Collider[] colliders = Physics.OverlapSphere(spawnPosition, checkRadius, obstacleLayer);

                foreach (var collider in colliders)
                {
                    Debug.Log($"Collider found: {collider.gameObject.name} at position {collider.transform.position}");
                }

                if (!Physics.CheckSphere(spawnPosition, checkRadius, obstacleLayer))
                {
                    Instantiate(lifeHeartPrefab, spawnPosition, Quaternion.identity);
                    return;
                }
            }

            Debug.LogWarning("Could not find a valid life heart spawn position.");
        }
    }
}
