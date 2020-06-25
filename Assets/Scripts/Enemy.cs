using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//적의 정보가 있는 스크립트
public class Enemy : MonoBehaviour
{
    public enum EnemyAi { attack, move, idle };
    public enum EnemyMove {up,down,left, right }
    public int health_point, action_point, attack, defence, row, col;
    private StateMachine state;
    private Animator EnemyAnimator;
    public bool ActionEnd;

    private Enemy enemy;
    private Vector2 enemy_pos;
    private GameObject target;
    private GameObject info;
    private GameObject maxHP, currHP;
    private float next_x, next_y;

    private PathFinding tilemap;
    //public List<string> EnemyAi = new List<string>() { "attack", "move", "idle" };
    //C#에서는 getter와 setter를 private 변수의 앞글자를 대문자로 바꾼 public 변수를 만들어 사용한다고 함
    public int Health_Point
    { set { health_point = value; } get { return health_point; } }
    public int Action_Point
    { set { action_point = value; } get { return action_point; } }
    public int Attack
    { set { attack = value; } get { return attack; } }
    public int Defence
    { set { defence = value; } get { return defence; } }
    public int Row
    { set { row = value; } get { return row; } }
    public int Col
    { set { col = value; } get { return col; } }
    public StateMachine State
    { set { state = value; } get { return state; } }
    public float NextX
    { set { next_x = value; } get { return next_x; } }
    public float NextY
    { set { next_y = value; } get { return next_y; } }

    public Vector2 position;

    void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
        if (this.gameObject.tag == "Enemy")
        {
            health_point = 50;
            action_point = 5;
            attack = 10;
            defence = 5;
        }

        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        enemy_pos = enemy.transform.position;

        maxHP = GameObject.Find("EnemyMaxHPBar");
        currHP = GameObject.Find("EnemyCurrHPBar");

        info = GameObject.Find("EnemyInfo");
        info.SetActive(false);

        //state = StateMachine.END;
        //Debug.Log(EnemyAnimator);
    }
    public void ai()
    {
        Debug.Log("몬스터 이동");
        EnemyMove move = (EnemyMove)Random.Range(0, 4);
        if (move == EnemyMove.up)
        {
            if (enemy.transform.position.y < 0)
            {
                next_x = enemy.transform.position.x;
                next_y = enemy.transform.position.y + 0.75f; 
            }
        }
        else if (move == EnemyMove.down)
        {
            if (enemy.transform.position.y > -3)
            {
                next_x = enemy.transform.position.x;
                next_y = enemy.transform.position.y - 0.75f;
            }
        }
        else if (move == EnemyMove.left)
        {
            if (enemy.transform.position.x > -4.34f)
            {
                next_x = enemy.transform.position.x - 2.5f;
                next_y = enemy.transform.position.y;
            }
        }
        else if (move == EnemyMove.right)
        {
            if (enemy.transform.position.x < 6.5f)
            {
                next_x = enemy.transform.position.x + 2.5f;
                next_y = enemy.transform.position.y;
            }

        }
    }

            //action_point -= 1;
            //Debug.Log("몬스터 끝");
            //Debug.Log(action_point);
    


    private void Update()
    {
        enemy_pos = enemy.transform.position;

        maxHP.transform.position = Camera.main.WorldToScreenPoint(enemy_pos + new Vector2(0.0f, 0.5f));
        currHP.transform.position = Camera.main.WorldToScreenPoint(enemy_pos + new Vector2(0.0f, 0.5f));

        SetHPBar();
    }

    void FixedUpdate()
    {
        CastRay();
        //마우스가 플레이어 위에 있다면 액션 창이 활성화
        if (target == this.gameObject)
        {
            info.SetActive(true);
        }

        CastRay();
        //마우스 버튼이 액션 이미지 위에서 떼어졌다면 == 액션을 취했으면 비활성화
        if (target != this.gameObject && !Caster.is_active)
            info.SetActive(false);
    }

    //광선을 쏴서 마우스와 충돌한 오브젝트를 판별하는 함수
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

    private void SetHPBar()
    {
        currHP.GetComponent<Image>().fillAmount = enemy.Health_Point / 50f;
    }
}
