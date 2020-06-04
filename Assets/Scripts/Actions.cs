using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actions : MonoBehaviour
{
    private PlayerBattle player;
    private Vector2 player_pos;
    private GameObject skill1;
    private GameObject skill2;
    private GameObject skill3;
    private GameObject move; 
    private GameObject guard;
    public string action_name = null;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBattle>();
        player_pos = player.transform.position;

        skill1 = GameObject.Find("Skill1");
        skill2 = GameObject.Find("Skill2");
        skill3 = GameObject.Find("Skill3");
        move = GameObject.Find("Move");
        guard = GameObject.Find("Guard");
    }

    void Update()
    {
        skill1.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(0.0f, 1.5f));
        skill2.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(1.4f, 0.2f));
        skill3.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(0.8f, -1.4f));
        move.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(-0.8f, -1.4f));
        guard.transform.position = Camera.main.WorldToScreenPoint(player_pos + new Vector2(-1.4f, 0.2f));

        if (!string.IsNullOrEmpty(action_name))
        {
            if (action_name == "Guard")
            {
                Guard();          
            }
            action_name = null;
        }
    }

    public void Guard()
    {
        Debug.Log(player.Health_Point.ToString());
        player.Health_Point += 5;
        Debug.Log(player.Health_Point.ToString());
    }

    public void SetActionName(string name)
    {
        Debug.Log("SetAction");
        action_name = name;
    }
}
