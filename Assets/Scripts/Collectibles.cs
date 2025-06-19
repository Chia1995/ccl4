// Description: This script handles the behavior of collectible items in the game, such as coins.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 90f; // Speed of the coin's rotation

    private void OnTriggerEnter(Collider other)
    {
        // If the coins collides with an obstacle, destroy the coins
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        // Check that the object that collided with the coins is the player
        if (other.gameObject.name != "Player")
        {
            return;
        }

        // If it is the player, increase the score
        GameManager.Instance.AddScore();

        // Destroy the coins after being collected
        Destroy(gameObject);
    }

    // This method is called once per frame to rotate the collectible
    void Update()
    {
        transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime); // Spin around X-axis (left-to-right)
    }
}
