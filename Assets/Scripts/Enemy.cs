using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//적의 정보가 있는 스크립트
public class Enemy : MonoBehaviour
{
    public enum EnemyAi { attack, move, idle };
    public enum EnemyMove {up,down,left, right }
    public int health_point, action_point, attack, defence, row, col;
    private StateMachine state;
    private Animator EnemyAnimator;
    public bool ActionEnd;
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

    void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
        if (this.gameObject.tag == "Enemy")
        {
            health_point = 30;
            action_point = 5;
            attack = 5;
            defence = 5;
        }

        state = StateMachine.END;
        Debug.Log(EnemyAnimator);
    }

    /*
    public void ai()
    {
        if (TurnButton.EnemyTurn)
        {
            for (int i = 0; i < 3; ++i)
            {
                EnemyAi rand = (EnemyAi)Random.Range(0,2);
                if (action_point > 0)
                {
                    Debug.Log(rand);
                    Debug.Log("액션포인트");
                    if (rand==EnemyAi.attack)
                    {
                        Debug.Log("몬스터 공격");
                        EnemyAnimator.SetTrigger("is_attack");
                        TurnButton.EnemyTurn = false;
                    }
                    else if (rand == EnemyAi.move)
                    {
                        Debug.Log("몬스터 이동");
                        EnemyMove move = (EnemyMove)Random.Range(0, 3);
                        if (move == EnemyMove.up)
                        {
                         
                        }
                        else if (move == EnemyMove.down)
                        {

                        }
                        else if (move == EnemyMove.left)
                        {

                        }
                        else if (move == EnemyMove.right)
                        {

                        }
                        
                        TurnButton.EnemyTurn = false;
                    }
                    else if (rand == EnemyAi.idle)
                    {
                        Debug.Log("몬스터 대기");
                        EnemyAnimator.SetBool("is_die", true);
                        TurnButton.EnemyTurn = false;
                    }
                }
                Debug.Log("액션포인트 끝");
            }
            
            //action_point -= 1;
            //Debug.Log("몬스터 끝");
            //Debug.Log(action_point);
        }
    }
    */
}
