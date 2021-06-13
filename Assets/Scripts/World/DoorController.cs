using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DoorController : MonoBehaviour
{
    [SerializeField]
    private Vector3 newPosition;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.Play();
        transform.DOMove(newPosition, 2f);
    }
}
