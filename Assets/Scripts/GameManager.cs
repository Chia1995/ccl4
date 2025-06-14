using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private TextMeshProUGUI finalScoreText = null;
    [SerializeField] private GameObject gameOverPanel = null;
    [SerializeField] private PlayerMovement playerMovement = null;

    private int score = 0;
    private int hitCount = 0;
    private bool isGameOver = false;

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
        if (playerMovement == null)
            playerMovement = FindObjectOfType<PlayerMovement>();

        if (scoreText == null)
        {
            GameObject scoreObj = GameObject.Find("ScoreText");
            if (scoreObj != null)
                scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
        }

        if (finalScoreText == null)
        {
            GameObject finalScoreObj = GameObject.Find("FinalScoreText");
            if (finalScoreObj != null)
                finalScoreText = finalScoreObj.GetComponent<TextMeshProUGUI>();
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateScoreUI();
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
            // Reload the current scene but keep score and hit count
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void TriggerGameOver()
    {
        isGameOver = true;
        score = Mathf.Max(0, score);
        Time.timeScale = 0f;

        if (finalScoreText != null)
            finalScoreText.text = $"Final Score: {score}";

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Debug.Log("Game Over. Final score: " + score);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        hitCount = 0;
        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        hitCount = 0;
        score = 0;

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
