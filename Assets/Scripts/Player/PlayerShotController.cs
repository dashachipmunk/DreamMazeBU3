using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    private Rigidbody rigidBody;
    [SerializeField]
    private float speed;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rigidBody.velocity = transform.forward * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
