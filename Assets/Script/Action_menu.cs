using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//단순한 이미지 생성을 위한 스크립트
public class Action_menu : MonoBehaviour
{
    private GameObject player;
    private Actions player_action;
    private Vector2 player_pos;

    void Start()
    {
        //플레이어 정보를 가져오고 이미지를 가운데 배치
        player = GameObject.Find("Player");
        player_pos = player.transform.position;
        this.transform.position = Camera.main.WorldToScreenPoint(player_pos);
        player_action = GameObject.Find("Actions").GetComponent<Actions>();
    }
    void Update()
    {

    }
}
