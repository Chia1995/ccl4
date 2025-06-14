using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private PlayerMovement playerMovement = null;

    private int score = 0;
    private int hitCount = 0;
    private bool isGameOver = false;

    public static int LastScore { get; private set; } // Used to show final score in GameOverScene

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (playerMovement == null)
            playerMovement = FindObjectOfType<PlayerMovement>();

        UpdateScoreUI();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

   
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    // Reset game state ONLY if we're not in the GameOverScene
    if (scene.name != "GameOverScene")
    {
        isGameOver = false;

        playerMovement = FindObjectOfType<PlayerMovement>();

        GameObject scoreObj = GameObject.Find("ScoreText");
        if (scoreObj != null)
            scoreText = scoreObj.GetComponent<TextMeshProUGUI>();

        UpdateScoreUI();
    }
}


    public void AddScore(int amount = 1)
    {
        score += amount;
        UpdateScoreUI();

        if (playerMovement != null)
        {
            playerMovement.IncreaseTargetSpeed(playerMovement.speedIncreasePerPoint * amount);
        }
    }

    public void OnObstacleHit()
    {
        if (isGameOver) return;

        hitCount++;
        score -= 20;
        UpdateScoreUI();

        if (score <= 0 || hitCount >= 3)
        {
            TriggerGameOver();
        }
        else
        {
            // Reload current scene with retained state
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void TriggerGameOver()
    {
        isGameOver = true;
        score = Mathf.Max(0, score);
        LastScore = score;
        Time.timeScale = 1f; // Just in case
        SceneManager.LoadScene("GameOverScene");
    }


    public void RestartGame()
{
    // Destroy the current GameManager instance before reloading
    Destroy(gameObject);
    
    Time.timeScale = 1f;
    isGameOver = false;
    hitCount = 0;
    score = 0;
    LastScore = 0;

    SceneManager.LoadScene("MainGameScene");
}



    public void QuitGame()
    {
        Time.timeScale = 1f;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
            scoreText.enabled = true;
        }
        else
        {
            Debug.LogWarning("ScoreText reference missing!");
        }
    }
}
