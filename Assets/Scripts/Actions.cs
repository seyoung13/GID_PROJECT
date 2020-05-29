using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actions : MonoBehaviour
{
    private GameObject player;
    private Vector2 player_pos;
    private GameObject action1;
    private GameObject action2;
    private GameObject action3;
    private GameObject action4; 
    private GameObject action5;
    private GameObject action6;

    void Start()
    {
        player = GameObject.Find("Player");
        player_pos = player.transform.position;
        action1 = GameObject.Find("Action1");
        action2 = GameObject.Find("Action2");
        action3 = GameObject.Find("Action3");
        action4 = GameObject.Find("Action4");
        action5 = GameObject.Find("Action5");
        action6 = GameObject.Find("Action6");
    }

    void Update()
    {
        action1.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(1.0f, 1.2f));
        action2.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(1.5f, 0.0f));
        action3.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(1.0f, -1.2f));
        action4.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(-1.0f, -1.2f));
        action5.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(-1.5f, 0.0f));
        action6.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(-1.0f, 1.2f));
    }

    private void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Caster")
        { 
            this.transform.localScale = new Vector2(2.0f, 2.0f);
            Debug.Log("충돌");
        }
    }

    private void OnTriggerExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Caster")
        {
            this.transform.localScale = new Vector2(0.5f, 0.5f);
            Debug.Log("충돌 끝");
        }
    }
}
