using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//플레이어 액션에 관한 스크립트
public class Actions : MonoBehaviour
{
    private PlayerBattle player;
    private Vector2 player_pos;
    
    private GameObject action_menu;
    private GameObject skill1;
    private GameObject skill2;
    private GameObject skill3;
    private GameObject move; 
    private GameObject guard;
    public string action_name = null;

    void Start()
    {   
        //액션 창을 플레이어 위치 기준으로 띄우기 위해 플레이어 정보를 가져옴
        player = GameObject.Find("Player").GetComponent<PlayerBattle>();
        player_pos = player.transform.position;

        action_menu = GameObject.Find("Action_menu");
        skill1 = GameObject.Find("Skill1");
        skill2 = GameObject.Find("Skill2");
        skill3 = GameObject.Find("Skill3");
        move = GameObject.Find("Move");
        guard = GameObject.Find("Guard");
    }

    void Update()
    {   
        //플레이어 중심으로 액션 이미지 배치
        //굳이 math 연산까지 할 필요는 없을 것 같아 절대값으로 작성 - 이세영
        action_menu.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(0.0f, 0.0f));
        skill1.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(0.0f, 3.0f));
        skill2.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(2.8f, 0.4f));
        skill3.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(1.6f, -2.8f));
        move.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(-1.6f, -2.8f));
        guard.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(-2.8f, 0.4f));

        //스킬 밸런스가 정해지면 수정 - 이세영
        if (!string.IsNullOrEmpty(action_name))
        {
            if (action_name == "Guard")
            {
                Guard();          
            }
            action_name = null;
        }
    }

    //스킬 밸런스가 정해지면 수정 - 이세영
    public void Guard()
    {
        Debug.Log(player.Health_Point.ToString());
        player.Health_Point += 5;
        Debug.Log(player.Health_Point.ToString());
    }

    //캐스터에서 선택한 액션 이름 가져옴
    public void SetActionName(string name)
    {
        Debug.Log("SetAction");
        action_name = name;
    }
}
