// This script is part of the PlayerMagnet system in a Unity game.
// It attracts collectibles within a specified radius towards the player when activated.
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    [SerializeField] private float attractionRadius = 10f;
    [SerializeField] private float attractionSpeed = 10f;

    private bool isMagnetActive = false;
    private float magnetTimer = 0f;

    private void Update()
    {
        if (isMagnetActive)
        {
            magnetTimer -= Time.deltaTime;

            if (magnetTimer <= 0f)
            {
                isMagnetActive = false;
            }
            else
            {
                AttractCollectibles();
            }
        }
    }

    private void AttractCollectibles()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attractionRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Collectibles"))
            {
                Transform collectible = hit.transform;
                Vector3 direction = (transform.position - collectible.position).normalized;
                collectible.position += direction * attractionSpeed * Time.deltaTime;
            }
        }
    }

    // ✅ This method works with MagnetPowerup.cs
    public void ActivateMagnet(float duration)
    {
        isMagnetActive = true;
        magnetTimer = duration;
    }

    // Optional: Visualize the attraction radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attractionRadius);
    }
}
