// This script manages the UI for displaying the player's lives in a game.
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    // Array to hold references to the heart UI images in the scene.
    public Image[] hearts;

    // This method updates the heart icons based on how many lives the player has left.
    public void UpdateLives(int livesLeft)
    {
        // Loop through each heart image
        for (int i = 0; i < hearts.Length; i++)
        {
            // Enable the heart if the index is less than livesLeft, otherwise disable it.
            // This visually shows how many lives remain.
            hearts[i].enabled = i < livesLeft;
        }
    }
}
