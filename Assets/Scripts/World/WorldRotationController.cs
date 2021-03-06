using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WorldRotationController : MonoBehaviour
{
    [SerializeField]
    private GameObject world;
    [SerializeField]
    private float rotationAngle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 rotateTheWorld = new Vector3(rotationAngle, 0, 0);
            world.transform.DORotate(rotateTheWorld, 2);
        }
    }
}
