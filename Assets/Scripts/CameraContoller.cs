using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 15, 0);
    private Transform playerTransform;
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
    }
}
