using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    private Transform main_camera;

    void Start()
    {
        main_camera = GetComponent<Transform>();
        
    }

    void LateUpdate()
    {
        main_camera.position = new Vector3(target.position.x + 6.5f, target.position.y + 0.5f, main_camera.transform.position.z);
    }
}
