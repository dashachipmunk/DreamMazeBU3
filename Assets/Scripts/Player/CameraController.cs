using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerBody;
    [SerializeField]
    private float rotationSpeedVertical;
    //[SerializeField]
    //private float rotationSpeedHorizontal;
    //[SerializeField]
    //private float viewAngle;

    private float mouseRotateVertical;
   // private float mouseRotateHorizontal;

    private void Update()
    {
        //mouseRotateHorizontal = Input.GetAxis("Mouse X") * rotationSpeedHorizontal;
        //playerBody.transform.Rotate(Vector3.up, mouseRotateHorizontal * Time.deltaTime);
        mouseRotateVertical = Input.GetAxis("Mouse Y") * rotationSpeedVertical;
        //mouseRotateVertical = Mathf.Clamp(mouseRotateVertical, -viewAngle, viewAngle);
        transform.Rotate(Vector3.right, mouseRotateVertical * Time.deltaTime);
    }
}
