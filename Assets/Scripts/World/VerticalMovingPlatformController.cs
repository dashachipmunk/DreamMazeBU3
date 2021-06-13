using UnityEngine;
using DG.Tweening;

public class VerticalMovingPlatformController : MonoBehaviour
{
    [SerializeField]
    private RigidBodyControllerMovement playerController;
    [SerializeField]
    private float yFinalPosition;
    [SerializeField]
    private float speed;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (playerController.isHealthReduced)
        {
            transform.DOMoveY(startPosition.y, speed / 4);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.DOMoveY(yFinalPosition, speed);
    }
}
