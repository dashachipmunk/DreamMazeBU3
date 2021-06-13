using System.Collections;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    [SerializeField]
    private int scoreAmount;
    private GameManager gameManager;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            gameManager.AddScore(scoreAmount);
            StartCoroutine(WaitSound());
        }
    }

    private IEnumerator WaitSound()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        Destroy(gameObject);
    }
}
