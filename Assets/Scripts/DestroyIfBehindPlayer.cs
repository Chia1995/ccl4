//// This script destroys the game object if it is positioned behind the player
using UnityEngine;

public class DestroyIfBehindPlayer : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float destroyDistance = 10f; // Distance behind player to destroy the magnet

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null) return;

        // If this object is far behind the player on the Z axis, destroy it
        if (transform.position.z < player.position.z - destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
