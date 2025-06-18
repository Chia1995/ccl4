// This script handles the Game Over UI, displaying the final score and providing options to restart or quit the game.
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;
    private void Start()
    {
        finalScoreText.text = $"Final Score: {GameManager.LastScore}";

        restartButton.onClick.AddListener(OnRestartButtonClicked);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void OnRestartButtonClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame(); // Call GameManager's restart logic
        }
        else
        {
            // Fallback in case GameManager is missing
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainGameScene");
        }
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // In case the game was paused
        SceneManager.LoadScene("StartGame"); // Make sure this matches your scene name exactly
    }
   
}
