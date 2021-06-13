using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    [SerializeField]
    private GameDataScriptableObject gameData;

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1f;
        if (scene == "Menu" || scene == "MazeScene")
        {
            PlayerPrefs.DeleteKey("TotalScore");
            PlayerPrefs.DeleteKey("Health");
            PlayerPrefs.DeleteKey("Tutorial");
        }
    }

    public void ResetScene()
    {
        PlayerPrefs.DeleteKey("TotalScore");
        PlayerPrefs.DeleteKey("Health");
        PlayerPrefs.DeleteKey("Tutorial");
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}