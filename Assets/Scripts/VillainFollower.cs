// This script makes a villain follow the player until they are caught after 3 hits.
using UnityEngine;

public class VillainFollower : MonoBehaviour
{
    public Transform player;
    public float followDistance = 2.5f;
    public float moveSpeed = 6f;

    private Animator animator;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        animator = GetComponent<Animator>();

        // Start running immediately if you have a running parameter
        /*
        if (animator != null)
        {
            animator.SetBool("isRunning", true); // Uncomment and update parameter name if needed
        }
        */
    }

    void Update()
    {
        if (player == null) return;

        if (GameManager.Instance != null && GameManager.Instance.HitCount < 3)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > followDistance)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;

                // Calculate direction but zero out y to keep upright
                Vector3 lookDirection = player.position - transform.position;
                lookDirection.y = 0;

                // Look at player with 180 degrees Y offset to fix backward facing
                transform.rotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(0, 180, 0);
            }
        }
        else
        {
            // Player hit 3 times — villain catches them
            transform.position = Vector3.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);

            Vector3 lookDirection = player.position - transform.position;
            lookDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(0, 180, 0);

            if (animator != null)
            {
                //animator.SetTrigger("catchPlayer"); // Optional: trigger catch animation
            }
        }
    }
}
