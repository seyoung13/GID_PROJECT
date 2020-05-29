using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Caster : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private GameObject player;
    private Vector2 player_pos;
    private Vector2 default_pos;

    void Start()
    {
        player = GameObject.Find("Player");
        player_pos = player.transform.position;
        this.transform.position = Camera.main.WorldToScreenPoint(player_pos);
    }

    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        default_pos = this.transform.position;
    } 

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mouse_pos = Input.mousePosition;
        this.transform.position = mouse_pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.position = default_pos;
    }
}
