using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Input system reference for managing player controls
    private PlayerInputActions inputActions;

    // Stores horizontal movement input
    private Vector2 inputVector;

    // Indicates if the player is alive
    private bool alive = true;

    // Movement speed values
    [SerializeField] private float speed = 5f;               // Current speed (interpolated)
    [SerializeField] private float targetSpeed = 5f;         // Target speed (increased when collecting items)
    [SerializeField] private float smoothSpeedLerp = 2f;     // How quickly speed moves toward targetSpeed
    [SerializeField] private float maxSpeed = 10f;          // Maximum speed the player can reach


    [SerializeField] private Rigidbody rb;                   // Reference to the Rigidbody for physics-based movement

    // Horizontal movement multiplier and jump strength
    [SerializeField] private float horizontalMultiplier = 2f;
    [SerializeField] private float jumpForce = 400f;

    // Ground detection
    [SerializeField] private LayerMask groundMask;

    // Amount to increase speed per score point
    [SerializeField] public float speedIncreasePerPoint = 0.01f;

    private void Awake()
    {
        // Initialize the input system and bind the jump action
        inputActions = new PlayerInputActions();
        inputActions.Gameplay.Jump.performed += ctx => Jump();
    }

    private void OnEnable()
    {
        // Enable gameplay actions and subscribe to movement input
        inputActions.Gameplay.Enable();
        inputActions.Gameplay.Move.performed += OnMove;
        inputActions.Gameplay.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        // Clean up input bindings
        inputActions.Gameplay.Move.performed -= OnMove;
        inputActions.Gameplay.Move.canceled -= OnMove;
        inputActions.Gameplay.Disable();
    }

    // Reads movement input
    private void OnMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (!alive) return;

        // Smoothly interpolate current speed toward target speed
        speed = Mathf.Lerp(speed, targetSpeed, smoothSpeedLerp * Time.fixedDeltaTime);

        // Forward movement (always moving forward)
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;

        // Horizontal movement (left/right input)
        Vector3 horizontalMove = transform.right * inputVector.x * speed * Time.fixedDeltaTime * horizontalMultiplier;

        // Apply movement to the Rigidbody
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    private void Update()
    {
        // If the player falls off the map, trigger death
        if (transform.position.y < -5)
        {
            Die();
        }
    }

    // Handles player death
    public void Die()
    {
        alive = false;
        Invoke(nameof(Restart), 2f); // Restart scene after delay
    }

    // Reloads the current scene
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Handles jumping if the player is grounded
    private void Jump()
    {
        // Cast a ray downward to check if the player is on the ground
        bool isGrounded = Physics.Raycast(transform.position + new Vector3(0,0.1f,0), Vector3.down, 0.15f, groundMask);

        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    // Called externally to increase how fast the player should move
    public void IncreaseTargetSpeed(float amount)
    {
        targetSpeed = Mathf.Min(targetSpeed + amount, maxSpeed);
    }

}
