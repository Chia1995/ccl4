// Description: This script handles the behavior of collectible items in the game, such as mushrooms.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 90f; // Speed of the mushroom's rotation

    private void OnTriggerEnter(Collider other)
    {
        // If the mushroom collides with an obstacle, destroy the mushroom
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        // Check that the object that collided with the mushroom is the player
        if (other.gameObject.name != "Player")
        {
            return;
        }

        // If it is the player, increase the score
        GameManager.Instance.AddScore();

        // Destroy the mushroom after being collected
        Destroy(gameObject);
    }

    // Rotate the mushroom around the Y axis
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
