// This script manages the game state, score, lives (as hearts), and player interactions in a Unity game.
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText = null; // Reference to UI score text
    [SerializeField] private PlayerMovement playerMovement = null;

    private int score = 20;
    private int hitCount = 0;
    private bool isGameOver = false;

    public static int LastScore { get; private set; }
    public int HitCount => hitCount; // Public getter for use by other scripts
    private static bool freshRestart = false;

    private void Awake()
    {
        // Singleton pattern to persist GameManager across scenes
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Called on start to initialize score and hit count
    private void Start()
    {
        if (playerMovement == null)
            playerMovement = FindObjectOfType<PlayerMovement>();

        if (freshRestart)
        {
            score = 0;
            hitCount = 0;
            freshRestart = false;
        }

        UpdateScoreUI();
        UpdateLivesUI(); // Initialize hearts on start
    }
    // Update is called once per frame
    // Check if player has fallen off the platform
    private void Update()
    {
        if (playerMovement != null && playerMovement.transform.position.y < -5f && !isGameOver)
        {
            OnFallOffPlatform();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called every time a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (freshRestart)
        {
            score = 0;
            hitCount = 0;
            freshRestart = false;
        }

        if (scene.name != "GameOverScene")
        {
            isGameOver = false;

            playerMovement = FindObjectOfType<PlayerMovement>();

            // Re-link score text UI from the new scene
            GameObject scoreObj = GameObject.Find("ScoreText");
            if (scoreObj != null)
                scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
            else
                Debug.LogWarning("ScoreText object not found in the scene!");

            UpdateScoreUI();
            UpdateLivesUI(); // Update heart icons after scene load
        }
    }

    // Adds score and updates UI/speed
    public void AddScore(int amount = 1)
    {
        score += amount;
        UpdateScoreUI();

        if (playerMovement != null)
        {
            playerMovement.IncreaseTargetSpeed(playerMovement.speedIncreasePerPoint * amount);
        }
    }

    // Called when the player hits an obstacle
    public void OnObstacleHit()
    {
        if (isGameOver) return;

        hitCount++;
        score -= 20;
        Debug.Log($"HitCount increased to: {hitCount}");
        UpdateScoreUI();
        UpdateLivesUI();

        if (score <= 0 || hitCount >= 3)
        {
            TriggerGameOver();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    // Called when the player falls off the platform
    // This is treated as an obstacle hit for simplicity
    private void OnFallOffPlatform()
    {
        Debug.Log("Player fell off the platform!");
        OnObstacleHit(); // Treat fall as a hit
    }

    // Ends the game and saves final score
    private void TriggerGameOver()
    {
        isGameOver = true;
        score = Mathf.Max(0, score);
        LastScore = score;
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOverScene");
    }

    // Restarts the game from scratch
    public void RestartGame()
    {
        freshRestart = true;
        LastScore = 0;
        hitCount = 0;
        score = 0;

        Destroy(gameObject); // Reset singleton
        SceneManager.LoadScene("MainGameScene");
    }

    // Quits the game or stops play mode in editor
    public void QuitGame()
    {
        Time.timeScale = 1f;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Updates the score display
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
        else
        {
            Debug.LogWarning("ScoreText reference missing!");
        }

        // Update hearts
        FindObjectOfType<LivesUI>()?.UpdateLives(3 - hitCount);
    }

    // Updates the heart-based lives UI (no text)
    private void UpdateLivesUI()
    {
        // We're only using the LivesUI with heart icons now
        FindObjectOfType<LivesUI>()?.UpdateLives(3 - hitCount);
    }

    // Restores one life when collecting a LifeMushroom
    public void RestoreLife()
    {
        if (isGameOver || hitCount <= 0) return; // Don't restore if game over or full lives

        hitCount = Mathf.Max(0, hitCount - 1); // Reduce hitCount by 1 (restore 1 life)
        UpdateLivesUI(); // Update the heart icons
    }

}
