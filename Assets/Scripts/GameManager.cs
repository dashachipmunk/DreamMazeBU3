using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public int totalScore;
    [HideInInspector]
    public int score;
    public int levelScore;

    [SerializeField]
    private TextMeshProUGUI scoreTextUI;
    private bool isPaused;

    private void Awake()
    {
        score = PlayerPrefs.GetInt("TotalScore");
        PlayerPrefs.SetInt("LevelScore", score);
        levelScore = PlayerPrefs.GetInt("LevelScore");
    }

    private void Update()
    {
        scoreTextUI.text = score.ToString();
        PauseGame();
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        scoreTextUI.text = score.ToString();
        PlayerPrefs.SetInt("TotalScore", score);
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                UIController.singletonUI.ShowPausePanel();
                isPaused = true;
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void ResumeGame()
    {
        UIController.singletonUI.HidePausePanel();
        isPaused = false;
    }
}
