using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject mushroomPrefab;
    public GameObject heartPrefab;
    public Transform player;
    public PlayerMovement playerMovement;

    public float segmentLength = 10f;
    public int segmentsAhead = 3;

    private float lastZ;
    private List<GameObject> activeSegments = new List<GameObject>();

    void Start()
    {
        if (playerMovement == null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement == null)
            {
                Debug.LogError("RoadManager: PlayerMovement not assigned and could not be found on player!");
            }
        }

        lastZ = -segmentLength;
        for (int i = 0; i < segmentsAhead; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        if (player.position.z + segmentLength * segmentsAhead > lastZ)
        {
            SpawnSegment();
        }

        if (activeSegments.Count > 0)
        {
            GameObject first = activeSegments[0];
            if (player.position.z - first.transform.position.z > segmentLength * 2)
            {
                Destroy(first);
                activeSegments.RemoveAt(0);
            }
        }
    }

    void SpawnSegment()
    {
        lastZ += segmentLength;
        GameObject newSegment = Instantiate(roadPrefab, new Vector3(0, 0, lastZ), Quaternion.identity);
        activeSegments.Add(newSegment);

        SpawnPickups(lastZ);
    }

    void SpawnPickups(float zCenter)
    {
        Debug.Log($"Calling SpawnPickups at Z: {zCenter}");

        float speed = 5f;
        float maxSpeed = 10f;

        if (playerMovement != null)
        {
            speed = playerMovement.TargetSpeed;
            maxSpeed = playerMovement.MaxSpeed;
        }

        int mushroomCount = Mathf.Clamp(Mathf.RoundToInt((speed / maxSpeed) * 5), 1, 5);
        Debug.Log($"Spawning {mushroomCount} mushrooms at Z {zCenter}");

        for (int i = 0; i < mushroomCount; i++)
        {
            float x = Random.Range(-0.14f, 0.14f);
            float z = zCenter - segmentLength / 2 + Random.Range(0f, segmentLength);
            Instantiate(mushroomPrefab, new Vector3(x, 0.25f, z), Quaternion.identity);
        }

        if (Random.value < 0.2f)
        {
            float x = Random.Range(-0.14f, 0.14f);
            float z = zCenter - segmentLength / 2 + Random.Range(0f, segmentLength);
            Instantiate(heartPrefab, new Vector3(x, 0.25f, z), Quaternion.identity);
        }
    }
}
