using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingRotationPlatformsController : MonoBehaviour
{
    private bool isContacted;
    [SerializeField]
    private RigidBodyControllerMovement playerController;

    private void Update()
    {
        if (playerController.isHealthReduced)
        {
            isContacted = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isContacted)
        {
            playerController.CountingRotationPlatforms();
            isContacted = true;
        }
    }
}
