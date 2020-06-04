using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Move 스테이지의 플레이어에 관한 스크립트
public class PlayerMove : MonoBehaviour
{
    private float speed = 3.0f;
    static public bool is_engaged_enemy = false;
    private GameObject enemy;
    private Vector2 enemy_pos;

    void Start()
    {   
        //적과의 거리로 배틀 스테이지 돌입 여부를 정하기 때문에 에너미 오브젝트를 가져옴
        enemy = GameObject.Find("Enemy");
        enemy_pos = enemy.transform.position;
    }

    void Update()
    {   
        //적과의 거리가 일정 수치 이하면 적과 조우했다고 알림
        if (enemy_pos.x - this.gameObject.transform.position.x < 12.0f)
            is_engaged_enemy = true;

        //적과 조우하기 전까진 프레임 시간에 따라 오른쪽으로 이동
        if (!is_engaged_enemy)
            this.gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);

    }
}
