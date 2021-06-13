using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectorController : MonoBehaviour
{
    [SerializeField]
    private GameObject keyStartParent;

    public void OnDropKey()
    {
        if (gameObject.transform.childCount == 2)
        {
            KeyController key = gameObject.transform.GetChild(0).gameObject.GetComponent<KeyController>();
            key.transform.parent = keyStartParent.transform;
            key.transform.localPosition = key.startPosition;
            key.isCollected = false;
        }
    }
}
