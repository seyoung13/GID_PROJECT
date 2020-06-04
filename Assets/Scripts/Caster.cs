using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Caster : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private GameObject player;
    private Actions player_action;
    private Vector2 player_pos;
    static public bool is_active = false, is_mousebutton_down = false;

    void Start()
    {
        player = GameObject.Find("Player");
        player_pos = player.transform.position;
        this.transform.position = Camera.main.WorldToScreenPoint(player_pos);
        player_action = GameObject.Find("Actions").GetComponent<Actions>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("캐스터 드래그 시작");
        is_active = true;
        is_mousebutton_down = true;
    } 

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mouse_pos = Input.mousePosition;
        this.transform.position = mouse_pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.position = Camera.main.WorldToScreenPoint(player_pos);
        is_mousebutton_down = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Action")
        {
            Debug.Log(collider.gameObject.name.ToString()+"준비");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(!is_mousebutton_down)
        {
            if (collider.gameObject.tag == "Action")
            {
                Debug.Log(collider.gameObject.name.ToString() + "시전");
                //player_action.SetActionName(collider.gameObject.name);
            }

            is_active = false;
        }
    }
}
