using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        UpdateUI();
    }

    public void AddScore(int amount)
    {
       
        score += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText)
            scoreText.text = "Score: " + score;
    }
}
