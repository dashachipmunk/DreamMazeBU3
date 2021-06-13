using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private DoorController door;
    [SerializeField]
    private string doorName;
    [SerializeField]
    private KeyCollectorController keyCollector;

    [HideInInspector]
    public bool isCollected;
    [HideInInspector]
    public Vector3 startPosition;

    [Header("Events")]
    [SerializeField]
    private UnityEvent dropKey;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isCollected)
            {
                audioSource.Play();
                transform.parent = keyCollector.transform;
                transform.localPosition = keyCollector.transform.localPosition;
                isCollected = true;
            }
            else
            {
                dropKey.Invoke();
            }
        }
        if (other.gameObject.name == doorName)
        {
            door.enabled = true;
            Destroy(gameObject);
        }
    }
}
