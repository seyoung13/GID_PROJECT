using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Battle 스테이지의 플레이어에 관한 스크립트
public class PlayerBattle : MonoBehaviour
{
    private GameObject target;
    private GameObject actions;
    static public bool is_battled;

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
        //Battle 스테이지 돌입 시 
        is_battled = true;
        actions = GameObject.Find("Actions");
        actions.SetActive(false);
        if (this.gameObject.tag == "Player")
        {
            health_point = 100;
            action_point = 5;
            attack = 5;
            defence = 5;
        }
    }

    void FixedUpdate()
    {
        CastRay();
        //마우스가 플레이어 위에 있다면 액션 창이 활성화
        if (target == this.gameObject)
        {    
            actions.SetActive(true);
        }

        CastRay();
        //마우스 버튼이 액션 이미지 위에서 떼어졌다면 == 액션을 취했으면 비활성화
        if (target != this.gameObject && !Caster.is_active)
                actions.SetActive(false);
    }

    //광선을 쏴서 마우스와 충돌한 오브젝트를 판별하는 함수
    void CastRay()
    {
        target = null;

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        { 
            target = hit.collider.gameObject;
        }
    }
}
