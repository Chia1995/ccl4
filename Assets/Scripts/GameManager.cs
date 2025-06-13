using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // Singleton instance of the GameManager for global access
    public static GameManager Instance { get; private set; }

    // UI text to display the score
    [SerializeField] private TextMeshProUGUI scoreText = null;

    // Reference to the PlayerMovement script to update speed
    [SerializeField] private PlayerMovement playerMovement = null;

    // Internal score counter
    private int score = 0;

    private void Awake()
    {
        // Ensure only one GameManager exists (Singleton pattern)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist between scene loads
    }

    private void Start()
    {
        // Auto-assign PlayerMovement if not set in Inspector
        if (playerMovement == null)
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
        }

        UpdateScoreUI(); // Initialize score display
    }

    private void OnEnable()
    {
        // Subscribe to scene load event to reassign PlayerMovement on scene reload
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called automatically when a new scene is loaded
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // Reset the score when the scene loads
        score = 0;

        // Find and reassign the PlayerMovement reference
        playerMovement = FindObjectOfType<PlayerMovement>();

        // Find and reassign the ScoreText reference
        if (scoreText == null)
        {
            GameObject scoreObj = GameObject.Find("ScoreText");
            if (scoreObj != null)
            scoreText = scoreObj.GetComponent<TMPro.TextMeshProUGUI>();
        }

        UpdateScoreUI(); // Refresh the UI after reassignment
    }

    // Adds to the player's score and increases movement speed accordingly
    public void AddScore(int amount = 1)
    {
        score += amount;
        UpdateScoreUI();

        // Gradually increase player speed based on score
        if (playerMovement != null)
        {
            playerMovement.IncreaseTargetSpeed(playerMovement.speedIncreasePerPoint * amount);
        }
    }

    // Updates the on-screen score UI
    private void UpdateScoreUI()
{
    if (scoreText != null)
    {
        scoreText.text = $"Score: {score}";
        Debug.Log($"ScoreText updated: {scoreText.text}");
        scoreText.enabled = true; // Make sure it's enabled
    }
    else
    {
        Debug.LogWarning("ScoreText reference missing!");
    }
}

}
