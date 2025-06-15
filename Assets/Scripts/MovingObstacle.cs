// This script moves an obstacle side-to-side in a specified range at a constant speed.
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float moveRange = 2f;   // Total distance to move side-to-side
    [SerializeField] private float moveSpeed = 2f;   // Speed of movement

    private Vector3 startPos;
    private float leftLimit;
    private float rightLimit;
    
    // Start is called before the first frame update
    private void Start()
    {
        startPos = transform.position;

        // Calculate left and right limits based on start position and range
        leftLimit = startPos.x - moveRange / 2f;
        rightLimit = startPos.x + moveRange / 2f;
    }
    // Update is called once per frame
    private void Update()
    {
        // PingPong returns a value between 0 and 1, we scale it to fit our range
        float t = Mathf.PingPong(Time.time * moveSpeed, 1f);
        float newX = Mathf.Lerp(leftLimit, rightLimit, t);
        transform.position = new Vector3(newX, startPos.y, startPos.z);
    }
}
