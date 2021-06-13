using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1f;
        if (scene == "Menu" || scene == "MazeScene")
        {
            PlayerPrefs.DeleteKey("TotalScore");
            PlayerPrefs.DeleteKey("Health");
        }

    }

    public void ResetScene()
    {
        PlayerPrefs.DeleteKey("TotalScore");
        PlayerPrefs.DeleteKey("Health");
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