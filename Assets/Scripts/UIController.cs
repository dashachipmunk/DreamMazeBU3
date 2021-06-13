using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController singletonUI { get; set; }

    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject tutorialPanel;

    private void Awake()
    {
        singletonUI = this;
    }

    private void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        UnlockCursor();
    }

    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        LockCursor();
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        UnlockCursor();
    }

    public void ShowTutorialPanel()
    {
        tutorialPanel.SetActive(true);
        Time.timeScale = 0f;
        UnlockCursor();
    }

    public void StartTime()
    {
        Time.timeScale = 1f;
        LockCursor();
    }
}
