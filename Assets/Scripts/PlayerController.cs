using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction moveAction;   // Assigned in Inspector (Vector2: X=left/right, Y=unused or W/S if desired)
    public InputAction jumpAction;   // Assigned in Inspector (Button: spacebar or other)

    public float baseSpeed = 5f;
    public float speedIncreaseRate = 0.1f;
    public float sideSpeed = 5f;
    public float jumpForce = 5f;
    public int maxLife = 100;

    private float currentSpeed;
    private Rigidbody rb;
    private float distanceTraveled = 0f;
    private Vector3 lastPosition;
    private int currentLife;

    public Slider lifeBar;
    public Text distanceText;
    public Animator animator;

    public GameObject villain;

    private bool isGameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = baseSpeed;
        lastPosition = transform.position;
        currentLife = maxLife;

        lifeBar.maxValue = maxLife;
        lifeBar.value = currentLife;

        villain.SetActive(false);

        moveAction.Enable();
        jumpAction.Enable();
    }

    void Update()
    {
        if (isGameOver) return;

        Vector2 input = moveAction.ReadValue<Vector2>();
        float horizontal = input.x;

        float forwardMove = currentSpeed * Time.deltaTime;
        float sideMove = horizontal * sideSpeed * Time.deltaTime;

        Vector3 move = new Vector3(sideMove, 0, forwardMove);

        rb.MovePosition(rb.position + move);

        // Jump
        if (jumpAction.WasPressedThisFrame() && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Distance tracking
        distanceTraveled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
        distanceText.text = $"Distance: {Mathf.FloorToInt(distanceTraveled)} m";

        // Speed increase
        currentSpeed = baseSpeed + speedIncreaseRate * distanceTraveled;

        // Animation speed
        if (animator != null)
        {
            animator.speed = currentSpeed / baseSpeed;
        }

        // Villain logic
        if (currentLife < 30 && !villain.activeSelf)
        {
            villain.SetActive(true);
        }

        if (villain.activeSelf)
        {
            Vector3 targetPos = transform.position - new Vector3(0, 0, 5);
            villain.transform.position = Vector3.Lerp(villain.transform.position, targetPos, Time.deltaTime * currentSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mushroom"))
        {
            AdjustLife(-10);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Heart"))
        {
            AdjustLife(+10);
            Destroy(other.gameObject);
        }
    }

    void AdjustLife(int amount)
    {
        currentLife = Mathf.Clamp(currentLife + amount, 0, maxLife);
        lifeBar.value = currentLife;

        if (currentLife <= 0 && !isGameOver)
        {
            isGameOver = true;
            Debug.Log("Game Over!");
            // TODO: Game over logic (scene load etc.)
        }
    }

    void OnDestroy()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }
}
