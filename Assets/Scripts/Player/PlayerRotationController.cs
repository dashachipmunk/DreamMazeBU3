using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerRotationController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerController;
    [SerializeField]
    private float rotationAngle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 rotatePlayer = new Vector3(rotationAngle, 0, 0);
            playerController.transform.DORotate(rotatePlayer, 2);
        }
    }
}
