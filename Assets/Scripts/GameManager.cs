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

    public static int LastScore { get; private set; }
    public int HitCount => hitCount; // Expose hit count for VillainFollower
    private static bool freshRestart = false;

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

        if (freshRestart)
        {
            score = 0;
            hitCount = 0;
            freshRestart = false;
        }

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

        GameObject scoreObj = GameObject.Find("ScoreText");
        if (scoreObj != null)
            scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
        else
            Debug.LogWarning("ScoreText object not found in the scene!");

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void TriggerGameOver()
    {
        isGameOver = true;
        score = Mathf.Max(0, score);
        LastScore = score;
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOverScene");
    }

    public void RestartGame()
    {
        // Flag for fresh start
        freshRestart = true;
        LastScore = 0;
        hitCount = 0;
        score = 0;

        // Destroy the GameManager instance to reset state
        Destroy(gameObject); 

        // Reload and let Start() handle reset
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
        }
        else
        {
            Debug.LogWarning("ScoreText reference missing!");
        }
    }
}
