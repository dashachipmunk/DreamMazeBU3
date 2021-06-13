using UnityEngine;
using System.Collections;
using DG.Tweening;

public class HorizontalMovingPlatformController : MonoBehaviour
{
    [SerializeField]
    private float zFinalPosition;
    [SerializeField]
    private float speed;

    private void Start()
    {
        transform.DOMoveZ(zFinalPosition, speed);
        StartCoroutine(MoveOppositeSize());
    }

    private IEnumerator MoveOppositeSize()
    {
        while (true)
        {
            yield return new WaitUntil(() => transform.position.z == zFinalPosition);
            zFinalPosition = -zFinalPosition;
            transform.DOMoveZ(zFinalPosition, speed);
        }
    }
}
