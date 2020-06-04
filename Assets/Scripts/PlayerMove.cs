using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private float speed = 3.0f;
    static public bool is_engaged_enemy = false;
    private GameObject enemy;
    private Vector2 enemy_pos;

    void Start()
    {
        enemy = GameObject.Find("Enemy");
        enemy_pos = enemy.transform.position;
    }

    void Update()
    {
        if (enemy_pos.x - this.gameObject.transform.position.x < 12.0f)
            is_engaged_enemy = true;

        if (!is_engaged_enemy)
            this.gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);

    }
}
