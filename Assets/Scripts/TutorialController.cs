using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorial;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tutorial.gameObject.SetActive(true);
            UIController.singletonUI.ShowTutorialPanel();
        }
    }
}
