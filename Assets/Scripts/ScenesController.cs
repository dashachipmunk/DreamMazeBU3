using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    [SerializeField]
    private HealthController healthController;
    [SerializeField]
    private GameManager gameManager;

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("LevelScore", gameManager.levelScore);
        if (scene == "Menu" || scene == "MazeScene")
        {
            PlayerPrefs.DeleteKey("TotalScore");
            PlayerPrefs.SetInt("Health", healthController.initialHealth);
        }
    }

    public void ResetScene()
    {
        PlayerPrefs.SetInt("Health", healthController.initialHealth);
        PlayerPrefs.SetInt("LevelScore", gameManager.levelScore);
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}