using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어를 따라가는 카메라를 만들기 위한 스크립트
public class CameraMove : MonoBehaviour
{
    public Transform target;
    private Transform main_camera;

    void Start()
    {   
        //위치 정보를 가져옴
        main_camera = GetComponent<Transform>();    
    }

    void LateUpdate()
    {   
        //2D이지만 카메라는 z축이 있으므로 유지해야 함
        main_camera.position = new Vector3(target.position.x + 6.5f, target.position.y + 0.5f, main_camera.transform.position.z);
    }
}
