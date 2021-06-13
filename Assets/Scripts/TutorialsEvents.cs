using UnityEngine;
using UnityEngine.Events;

public class TutorialsEvents : MonoBehaviour
{
    private void Start()
    {
        UIController.singletonUI.ShowTutorialPanel();
    }
}
