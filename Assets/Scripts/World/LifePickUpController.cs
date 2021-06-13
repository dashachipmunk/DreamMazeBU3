using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePickUpController : MonoBehaviour
{
    private HealthController healthController;
    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        healthController = FindObjectOfType<HealthController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            healthController.HealthIcrease();
            audioSource.Play();
            StartCoroutine(WaitSound());
        }
    }

    private IEnumerator WaitSound()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        Destroy(gameObject);
    }
}
