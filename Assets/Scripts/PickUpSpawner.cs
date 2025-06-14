using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public PlayerController playerController;

    public GameObject heartPrefab;
    public GameObject mushroomPrefab;
    public GameObject roadPrefab;

    public Transform player;

    public float segmentLength = 20f;  // Larger segments = fewer spawns
    public float roadWidth = 0.28f;    // Your narrow road

    public int segmentsAhead = 5;

    private float lastZ = 0f;

    void Start()
    {
        lastZ = -segmentLength;
        for (int i = 0; i < segmentsAhead; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        if (player.position.z + (segmentsAhead * segmentLength) > lastZ)
        {
            SpawnSegment();
        }
    }

    void SpawnSegment()
    {
        lastZ += segmentLength;
        Debug.Log($"Spawning road at Z: {lastZ}");
        Instantiate(roadPrefab, new Vector3(0, 0, lastZ), Quaternion.identity);

        // Spawn exactly 1 mushroom
        SpawnPickup(mushroomPrefab);

        // 30% chance for heart
        if (Random.value < 0.3f)
        {
            SpawnPickup(heartPrefab);
        }
    }

    void SpawnPickup(GameObject prefab)
    {
        // Make sure pickup stays on the narrow road
        float x = Random.Range(-roadWidth / 2f, roadWidth / 2f);
        float z = lastZ - (segmentLength / 2f) + Random.Range(0, segmentLength);
        Instantiate(prefab, new Vector3(x, 0.25f, z), Quaternion.identity);
    }
}
