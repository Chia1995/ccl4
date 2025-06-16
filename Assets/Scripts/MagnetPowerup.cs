// Description: Script to handle magnet power-up functionality in a game.
using UnityEngine;

public class MagnetPowerup : MonoBehaviour
{
    [SerializeField] private float duration = 5f;
    [SerializeField] private GameObject pickupEffectPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate magnet on the player
            PlayerMagnet magnet = other.GetComponent<PlayerMagnet>();
            if (magnet != null)
            {
                magnet.ActivateMagnet(duration);
            }

            // Spawn and attach the pickup effect
            if (pickupEffectPrefab != null)
            {
                GameObject effect = Instantiate(pickupEffectPrefab, transform.position, Quaternion.identity);

                // Parent to the player so it follows them
                effect.transform.SetParent(other.transform);

                // Destroy the effect after the duration ends
                Destroy(effect, duration);
            }

            // Destroy the magnet power-up object itself
            Destroy(gameObject);
        }
    }
}
