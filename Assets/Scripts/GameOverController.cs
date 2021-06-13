using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private TextMeshProUGUI totalScoreTextUI;

    private void Start()
    {
        gameManager.score = PlayerPrefs.GetInt("TotalScore");
        totalScoreTextUI.text = "Your score: " + gameManager.score.ToString();
    }
}
