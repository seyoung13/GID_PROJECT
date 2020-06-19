using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum character
{
    EMPTY,
    PLAYER,
    ENEMY
}
public enum StateMachine
{
    //명령 대기
    WAITING,
    //이동
    MOVING,
    //액션
    ACTION,
    //턴 종료
    END,
    //사망 상태
    DEAD
}

//배틀 정보를 관리하는 스크립트
public class BattleManager : MonoBehaviour
{
    private PlayerBattle player;
    private Enemy enemy;
    public Collider2D player_pos;
    public Collider2D enemy_pos;
    private character[,] board = new character[5, 6];
    private GameObject path;
    private PathFinding tilemap;

    float curr_x, curr_y;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBattle>();
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        player_pos = GameObject.Find("Player").GetComponent<Collider2D>();
        enemy_pos = GameObject.Find("Enemy").GetComponent<Collider2D>();
        path = GameObject.Find("Tilemap");
        path.SetActive(false);
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 6; j++)
                board[i, j] = character.EMPTY;
    }

    void Update()
    {
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 6; j++)
                if (board[i, j] == character.PLAYER)
                {   
                    player.Row = i;
                    player.Col = j;
                }
                else if (board[i, j] == character.ENEMY)
                {
                    enemy.Row = i;
                    enemy.Col = j;
                }

        if (player.State != StateMachine.END) 
            switch (player.State)
            {
                case StateMachine.WAITING:
                    Debug.Log("Player State WATING");
                    break;
                case StateMachine.MOVING:
                    Debug.Log("Player State MOVING");
                    path.SetActive(true);
                    tilemap = GameObject.Find("Tilemap").GetComponent<PathFinding>();

                    if (!PathFinding.is_clicked)
                    {
                        GetObjectLocationOnPlatform(player_pos);
                        GetObjectLocationOnPlatform(enemy_pos);
                        curr_x = player.transform.position.x;
                        curr_y = player.transform.position.y;
                    }

                    if (PathFinding.is_clicked)
                    {
                        int diff_row = tilemap.TargetRow - player.Row;
                        int diff_col = tilemap.TargetCol - player.Col;
                        Vector2 ref_velocity = Vector2.zero;
                        Vector2 next = new Vector2(curr_x + diff_col * 2.5f,
                            curr_y - diff_row * 0.75f);
                        player.transform.position = Vector2.SmoothDamp(player.transform.position,
                            next, ref ref_velocity, 0.1f);
                        Debug.Log("player:"+player.Row.ToString() + player.Col.ToString());
                        if (player.Row == tilemap.TargetRow && player.Col == tilemap.TargetCol)
                        { 
                            player.State = StateMachine.END;
                            PathFinding.is_clicked = false;
                            for (int i = 0; i < 5; i++)
                                for (int j = 0; j < 6; j++)
                                    board[i, j] = character.EMPTY;
                            GetObjectLocationOnPlatform(player_pos);
                            GetObjectLocationOnPlatform(enemy_pos);
                        }
                    }
                    break;
                case StateMachine.ACTION:
                    Debug.Log("Player State ACTION");
                    break;
                case StateMachine.DEAD:
                    break;
                case StateMachine.END:
                    Debug.Log("Player State END");
                    enemy.State = StateMachine.WAITING;
                    break;
            }

        if (enemy.State != StateMachine.END)
            switch (enemy.State)
            {
                case StateMachine.WAITING:
                    Debug.Log("Enemy State WAITING.");
                    enemy.State = StateMachine.MOVING;
                    break;
                case StateMachine.MOVING:
                    Debug.Log("Enemy State MOVING.");
                    enemy.State = StateMachine.ACTION;
                    break;
                case StateMachine.ACTION:
                    Debug.Log("Enemy State ACTION.");
                    enemy.State = StateMachine.END;
                    break;
                case StateMachine.DEAD:
                    break;
                case StateMachine.END:
                    Debug.Log("Enemy State END");
                    player.State = StateMachine.WAITING;
                    break;
            }

        Debug.Log("ps "+player.State.ToString());
        Debug.Log("es "+enemy.State.ToString());
    }

    private void GetObjectLocationOnPlatform(Collider2D collider)
    {
        character ch = character.EMPTY;
        if (collider.gameObject.tag == "Player")
            ch = character.PLAYER;
        else if (collider.gameObject.tag == "Enemy")
            ch = character.ENEMY;

        float x = collider.transform.position.x;
        float y = collider.transform.position.y;
        if (ch == character.PLAYER)
            y -= 1;

        double left, top, right, bot;
        for (int i=0; i<5; i++)
            for (int j=0; j<6; j++)
            {
                top = 0.5 - i * 0.75;
                bot = top - 0.75;
                
                left = -8 + j * 2.5;
                right = left + 2.5;

                if ((left <= x && x < right) && (bot <= y && y < top))
                {
                    board[i, j] = ch;
                }
            }
    }

}