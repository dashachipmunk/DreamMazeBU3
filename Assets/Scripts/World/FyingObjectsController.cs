using System.Collections;
using UnityEngine;
using DG.Tweening;

public class FyingObjectsController : MonoBehaviour
{
    [SerializeField]
    private float animationTime;

    [Header("Random position")]
    private Vector3 randomPosition;
    private float randomPositionX;
    private float randomPositionY;
    private float randomPositionZ;

    private void Start()
    {
        NewPosition();
        randomPosition = new Vector3(randomPositionX, randomPositionY, randomPositionZ);
        transform.DOMove(randomPosition, animationTime);
        StartCoroutine(Repeat());
    }

    private void NewPosition()
    {
        randomPositionX = transform.position.x + Random.Range(-50, 50);
        randomPositionY = transform.position.y + Random.Range(-50, 50);
        randomPositionZ = transform.position.z + Random.Range(-50, 50);
    }

    private IEnumerator Repeat()
    {
        while (true)
        {
            yield return new WaitUntil(() => transform.position == randomPosition);
            NewPosition();
            randomPosition = new Vector3(randomPositionX, randomPositionY, randomPositionZ);
            transform.DOMove(randomPosition, animationTime);
        }
    }
}
