using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform targetTransform;
    public Vector3 CameraOffset;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = targetTransform.position + CameraOffset;


    }
}
