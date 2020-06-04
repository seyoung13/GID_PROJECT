using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject actions;
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
        if (this.gameObject.tag == "Enemy")
        {
            health_point = 10;
            action_point = 4;
            attack = 2;
            defence = 2;
        }

        this.gameObject.transform.position = (this.gameObject.transform.position);
    }

}
