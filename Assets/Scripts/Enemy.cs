using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//적의 정보가 있는 스크립트
public class Enemy : MonoBehaviour
{
    private int health_point, action_point, attack, defence;
    //C#에서는 getter와 setter를 private 변수의 앞글자를 대문자로 바꾼 public 변수를 만들어 사용한다고 함
    public int Health_Point
    { set; get; }
    public int Action_Point
    { set; get; }
    public int Attack
    { set; get; }
    public int Defence
    { set; get; }

    void Start()
    {   
        if (this.gameObject.tag == "Enemy")
        {
            health_point = 30;
            action_point = 5;
            attack = 5;
            defence = 5;
        }

        //차후 스크린 좌표 위치에 맞게 수정 예정 - 이세영
        this.gameObject.transform.position = (this.gameObject.transform.position);
    }

}
