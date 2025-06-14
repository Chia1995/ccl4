using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject mushroomPrefab;
    public GameObject heartPrefab;
    public Transform player;
    public float segmentLength = 50f;
    public int segmentsAhead = 3;

    private float lastZ;
    private List<GameObject> activeSegments = new List<GameObject>();

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
        // Spawn mushrooms
        int mushroomCount = Random.Range(2, 5);
        for (int i = 0; i < mushroomCount; i++)
        {
            float x = Random.Range(-2f, 2f);
            float z = zCenter - segmentLength / 2 + Random.Range(0f, segmentLength);
            Instantiate(mushroomPrefab, new Vector3(x, 0.25f, z), Quaternion.identity);
        }

        // Spawn hearts
        int heartCount = Random.Range(0, 2);
        for (int i = 0; i < heartCount; i++)
        {
            float x = Random.Range(-2f, 2f);
            float z = zCenter - segmentLength / 2 + Random.Range(0f, segmentLength);
            Instantiate(heartPrefab, new Vector3(x, 0.25f, z), Quaternion.identity);
        }
    }
}
