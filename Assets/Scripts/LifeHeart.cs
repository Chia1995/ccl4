// This script handles the behavior of a life-restoring heart in the game.
using UnityEngine;

public class LifeHeart : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f; // Speed of the heart's rotation

    private void OnTriggerEnter(Collider other)
    {
        // If the heart collides with an obstacle, destroy it
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        // Check that the object that collided is the player
        if (!other.CompareTag("Player"))
        {
            return;
        }

        // Restore one life if the player collects it
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestoreLife();
        }

        // Destroy the heart after being collected
        Destroy(gameObject);
    }

    // Rotate the heart around the Y axis
    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
