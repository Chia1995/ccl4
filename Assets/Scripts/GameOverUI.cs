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

    private void OnRestartButtonClicked()
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

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
