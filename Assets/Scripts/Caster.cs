﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//마우스 드래그 반응을 살피기 위해 만든 스크립트
//노란 사각형을 캐스터라 명명
public class Caster : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private GameObject player;
    private Actions player_action;
    private Vector2 player_pos;
    string action_name;
    static public bool is_active = false, is_mousebutton_down = false;

    void Start()
    {
        //플레이어 정보를 가져오고 캐스터를 가운데 배치
        player = GameObject.Find("Player");
        player_pos = player.transform.position;
        this.transform.position = Camera.main.WorldToScreenPoint(player_pos);

        player_action = GameObject.Find("Actions").GetComponent<Actions>();
    }

    //캐스터를 마우스로 누르면 마우스 버튼이 눌리고 활성화
    //마우스 콜백 함수가 다른 스크립트에 정보가 가기 전에 바뀌어서 bool 변수를 따로 설정함
    public void OnBeginDrag(PointerEventData eventData)
    {
        is_active = true;
        is_mousebutton_down = true;
    }

    //드래그 중이면 캐스터는 마우스를 따라감
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mouse_pos = Input.mousePosition;
        this.transform.position = mouse_pos;
    }

    //마우스를 떼면 캐스터는 플레이어 위치로 돌아감
    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.position = Camera.main.WorldToScreenPoint(player_pos);
        is_mousebutton_down = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Action")
        {
            //Debug.Log("Mouse On" + player_action.Action_Name.ToString());
            player_action.Action_Name = collision.gameObject.name;
        }
    }

    //캐스터가 플레이어 위치로 돌아가 충돌에서 벗어난 후
    private void OnTriggerExit2D(Collider2D collider)
    {
        //마우스 버튼을 뗐는데
        if (!is_mousebutton_down)
        {   //액션 이미지 위에서 떼어졌다면
            player_pos = player.transform.position;
            this.transform.position = Camera.main.WorldToScreenPoint(player_pos);
            is_active = false;
        }
    }
}