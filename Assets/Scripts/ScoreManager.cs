using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    private int score = 0;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of ScoreManager exists.
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score; // Update the score text UI
    }
}
