using UnityEngine;

public class MagnetPowerup : MonoBehaviour
{
    [SerializeField] private float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMagnet magnet = other.GetComponent<PlayerMagnet>();
            if (magnet != null)
            {
                magnet.ActivateMagnet(duration);
                Destroy(gameObject); // Destroy the power-up after pickup
            }
        }
    }
}
