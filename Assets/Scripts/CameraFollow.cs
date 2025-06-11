// This script makes the camera follow a player object in a 3D game.

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 offset;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player reference not set on CameraFollow script.");
            enabled = false;
            return;
        }

        // Calculate initial offset from the player
        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        // Follow the player with the same offset
        Vector3 targetPosition = player.position + offset;

        // Keep the camera centered on the player (only allow movement on Z & Y)
        targetPosition.x = 0;

        transform.position = targetPosition;
    }
}

