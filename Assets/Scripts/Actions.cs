﻿using System.Collections;
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
    private GameObject maxHP, currHP;
    public string action_name = "";

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

        //hp바, ap바 이미지 - 06.14 이세영
        maxHP = GameObject.Find("MaxHPBar");
        currHP = GameObject.Find("CurrHPBar");
    }

    void Update()
    {   
        //플레이어 중심으로 액션 이미지 배치
        //굳이 math 연산까지 할 필요는 없을 것 같아 절대값으로 작성 - 이세영
        action_menu.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(0.0f, 0.0f));
        skill1.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(0.0f, 2.0f));
        skill2.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(2.0f, 0.3f));
        skill3.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(1.0f, -2.0f));
        move.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(-1.0f, -2.0f));
        guard.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(-2.0f, 0.3f));

        //hp바, ap바 이미지 위치 조정 - 06.14 이세영
        maxHP.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(0.0f, 1.0f));
        currHP.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(0.0f, 1.0f));

        //스킬 밸런스가 정해지면 수정 - 이세영
        if (!string.IsNullOrEmpty(action_name))
        {
            if (action_name == "Guard")
            {
                Guard();
            }
            if (action_name == "Move")
            {
                player.State = StateMachine.MOVING;
            }
            if (action_name == "Skill1")
            {
                Skill1();
            }
            if (action_name == "Skill2")
            {
                Skill2();
            }
            if (action_name == "Skill3")
            {
                Skill3();
            }
            Debug.Log(action_name.ToString());
            action_name = null;
        }

        SetHPBar();
    }

    //스킬 밸런스가 정해지면 수정 - 이세영
    public void Guard()
    {
        player.Defence += 5;
        player.State = StateMachine.END;
        Debug.Log("Guard: 시전 중 " + player.Defence.ToString());
    }

    public void Move()
    {
        Debug.Log("Move: 시전 중");
    }

    public void Skill1()
    {
        Debug.Log("Skill1: 시전 중");
        player.State = StateMachine.END;
    }

    public void Skill2()
    {
        Debug.Log("Skill2: 시전 중");
        player.State = StateMachine.END;
    }
    public void Skill3()
    {
        Debug.Log("Skill3: 시전 중");
        player.State = StateMachine.END;
    }

    //06 14 수정
    //캐스터에서 선택한 액션 이름 가져옴
    public void SetActionName(string name)
    {
        action_name = name;
    }

    //현재 hp, ap 이미지 길이를 현재 hp, ap에 맞게 조정 - 06.14 이세영
    private void SetHPBar()
    {
        currHP.GetComponent<Image>().fillAmount = player.Health_Point / 100f;
    }
}