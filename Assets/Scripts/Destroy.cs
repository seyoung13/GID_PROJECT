using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // 시작시 목표 오브젝트를 파괴하는 함수 - 참고용
    void Start()
    {
        GameObject.Destroy(GameObject.Find("Title"));
    }


}
