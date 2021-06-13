using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WorldRotationController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerController;
    [SerializeField]
    private GameObject world;
    [SerializeField]
    private float rotationAngle;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 rotateTheWorld = new Vector3(rotationAngle, 0, 0);
        Vector3 rotatePlayer = new Vector3(rotationAngle, 0, 0);
        playerController.transform.DORotate(rotatePlayer, 2);
        world.transform.DORotate(rotateTheWorld, 2);

    }
}
