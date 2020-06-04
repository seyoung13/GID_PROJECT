using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattle : MonoBehaviour
{
    private GameObject target;
    private GameObject actions;
    static public bool is_battled;

    private int health_point, action_point, attack, defence;
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
        is_battled = true;
        actions = GameObject.Find("Actions");
        actions.SetActive(false);
        if (this.gameObject.tag == "Player")
        {
            health_point = 10;
            action_point = 5;
            attack = 5;
            defence = 5;
        }
    }

    void FixedUpdate()
    {
        CastRay();
        if (target == this.gameObject)
        {
            actions.SetActive(true);
        }

        CastRay();
        if (target != this.gameObject && !Caster.is_active)
                actions.SetActive(false);
    }

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
