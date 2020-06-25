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
    
    private character[,] board = new character[5, 6];
    private GameObject path;
    private PathFinding tilemap;

    public Collider2D player_pos;
    public Collider2D enemy_pos;
    public Actions player_action;

    float curr_x, curr_y;
    private bool is_player_moved = false;
    private bool is_enemy_moved = false;
    static public bool is_someone_died = false;
    bool player_turn = true;

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

        player.State = StateMachine.WAITING;
        enemy.State = StateMachine.END;
    }

    void FixedUpdate()
    {
        SetObjectRowNColumn();

        if (player_turn)
        {
            switch (player.State)
            {
                case StateMachine.WAITING:
                    {
                        if (!string.IsNullOrEmpty(player_action.Action_Name) && !Caster.is_mousebutton_down)
                            switch (player_action.Action_Name)
                            {
                                case "Guard":
                                case "Skill1":
                                case "Skill2":
                                case "Skill3":
                                    player.State = StateMachine.ACTION;
                                    break;
                                case "Move":
                                    PathFinding.move_point = 3;
                                    if (!is_player_moved)
                                        player.State = StateMachine.MOVING;
                                    break;
                            }
                        break;
                    }
                case StateMachine.MOVING:
                    {
                        path.SetActive(true);
                        tilemap = GameObject.Find("Tilemap").GetComponent<PathFinding>();

                        if (!PathFinding.is_clicked)
                        {
                            SetLocationOnPlatform(player_pos);
                            SetLocationOnPlatform(enemy_pos);
                            curr_x = player.transform.position.x;
                            curr_y = player.transform.position.y;
                        }

                        if (PathFinding.is_clicked)
                        {
                            int diff_row = tilemap.TargetRow - player.Row;
                            int diff_col = tilemap.TargetCol - player.Col;
                            Vector2 next = new Vector2(curr_x + diff_col * 2.5f,
                                curr_y - diff_row * 0.75f);
                            player.transform.position = Vector2.MoveTowards(player.transform.position,
                                next, 0.2f);
                            if (player.transform.position == new Vector3(next.x, next.y, 0))
                            {
                                player.State = StateMachine.WAITING;
                                PathFinding.is_clicked = false;
                                for (int i = 0; i < 5; i++)
                                    for (int j = 0; j < 6; j++)
                                        board[i, j] = character.EMPTY;
                                path.SetActive(false);
                                is_player_moved = true;
                                PathFinding.is_clicked = false;
                                RefreshBoard();
                            }
                        }
                        break;
                    }
                case StateMachine.ACTION:
                    {
                        if (!Caster.is_mousebutton_down)
                        {
                            switch (player_action.Action_Name)
                            {
                                case "Guard":
                                    player_action.Guard();
                                    break;
                                case "Skill1":
                                    player_action.Skill1();
                                    SkillHitDetection(player_action.Action_Name);
                                    break;
                                case "Skill2":
                                    player_action.Skill2();
                                    SkillHitDetection(player_action.Action_Name);
                                    break;
                                case "Skill3":
                                    player_action.Skill3();
                                    SkillHitDetection(player_action.Action_Name);
                                    break;
                            }
                            player_action.Action_Name = "";
                            player.State = StateMachine.END;
                        }
                        break;
                    }
                case StateMachine.DEAD:
                    {
                        is_someone_died = true;
                        break;
                    }
                case StateMachine.END:
                    {
                        is_player_moved = false;
                        if (player.Defence >= 10)
                            player.Defence = 5;
                        if (enemy.State == StateMachine.END)
                            enemy.State = StateMachine.WAITING;
                        player_turn = false;
                        break;
                    }
            }
        }
        
        if (!player_turn)
        {
            Debug.Log("E S:" + enemy.State.ToString());
            switch (enemy.State)
            {
                case StateMachine.WAITING:
                    {
                        enemy.State = StateMachine.MOVING;
                        is_enemy_moved = false;
                        break;
                    }
                case StateMachine.MOVING:
                    {
                        if (!is_enemy_moved)
                        {
                            SetLocationOnPlatform(player_pos);
                            SetLocationOnPlatform(enemy_pos);
                            enemy.ai();
                            is_enemy_moved = true;
                        }

                        if (is_enemy_moved)
                        {
                            Vector2 next = new Vector2(enemy.NextX, enemy.NextY);
                            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position,
                               next, 0.2f);
                            if (enemy.transform.position == new Vector3(enemy.NextX, enemy.NextY, 0))
                            {
                                for (int i = 0; i < 5; i++)
                                    for (int j = 0; j < 6; j++)
                                        board[i, j] = character.EMPTY;
                                enemy.State = StateMachine.END;
                                RefreshBoard();
                            }
                        }
                        
                        break;
                    }
                case StateMachine.ACTION:
                    {
                        enemy.State = StateMachine.END;
                        break;
                    }
                case StateMachine.DEAD:
                    {
                        is_someone_died = true;
                        break;
                    }
                case StateMachine.END:
                    {
                        if (player.State == StateMachine.END)
                            player.State = StateMachine.WAITING;
                        player_turn = true;
                        break;
                    }
            }
        }

        /*
        if (player.Health_Point <= 0)
        {
            enemy.State = StateMachine.END;
            player.State = StateMachine.DEAD;
        }
        if (enemy.Health_Point <= 0)
        {
            player.State = StateMachine.END;
            enemy.State = StateMachine.DEAD; 
        }*/
        Debug.Log("enemyHP"+enemy.Health_Point.ToString());
    }

    private void SetLocationOnPlatform(Collider2D collider)
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
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 6; j++)
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

    private void SetObjectRowNColumn()
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
    }

    private void SkillHitDetection(string action_name)
    {
        if (action_name == "Skill1")
        {
            int damage = (2 * player.Attack - enemy.Defence);

            if (player.Col <= 3)
            {
                if (board[player.Row, player.Col + 1] == character.ENEMY ||
                    board[player.Row, player.Col + 2] == character.ENEMY)
                    enemy.Health_Point -= damage;
            }
            else if (player.Col == 4)
            {
                if (board[player.Row, player.Col + 1] == character.ENEMY)
                    enemy.Health_Point -= damage;
            }

        }
        else if (action_name == "Skill2")
        {
            int damage = player.Attack - enemy.Defence;

            if (1 <= player.Row && player.Row <= 3)
            {
                if (player.Col <= 3)
                {
                    if (board[player.Row - 1, player.Col + 1] == character.ENEMY ||
                        board[player.Row - 1, player.Col + 2] == character.ENEMY ||
                        board[player.Row + 1, player.Col + 1] == character.ENEMY ||
                        board[player.Row + 1, player.Col + 2] == character.ENEMY)
                        enemy.Health_Point -= damage;
                }
                else if (player.Col == 4)
                {
                    if (board[player.Row - 1, player.Col + 1] == character.ENEMY ||
                        board[player.Row + 1, player.Col + 1] == character.ENEMY)
                        enemy.Health_Point -= damage;
                }
            }
            else if (player.Row == 0)
            {
                if (player.Col <= 3)
                {
                    if (board[player.Row + 1, player.Col + 1] == character.ENEMY ||
                       board[player.Row + 1, player.Col + 2] == character.ENEMY)
                        enemy.Health_Point -= damage;
                }
                else if (player.Col == 4)
                {
                    if (board[player.Row + 1, player.Col + 1] == character.ENEMY)
                        enemy.Health_Point -= damage;
                }
            }
            else if (player.Row == 4)
            {
                if (player.Col <= 3)
                {
                    if (board[player.Row - 1, player.Col + 1] == character.ENEMY ||
                        board[player.Row - 1, player.Col + 2] == character.ENEMY)
                        enemy.Health_Point -= damage;
                }
                else if (player.Col == 4)
                {
                    if (board[player.Row - 1, player.Col + 1] == character.ENEMY)
                        enemy.Health_Point -= damage;
                }
            }
        }
        else if (action_name == "Skill3")
        {
            int damage = player.Attack - enemy.Defence;

            if (1 <= player.Row && player.Row <= 3 && 1 <= player.Col && player.Col <= 4)
            {
                if (board[player.Row - 1, player.Col] == character.ENEMY ||
                    board[player.Row - 1, player.Col + 1] == character.ENEMY ||
                    board[player.Row, player.Col + 1] == character.ENEMY ||
                    board[player.Row + 1, player.Col + 1] == character.ENEMY ||
                    board[player.Row + 1, player.Col] == character.ENEMY ||
                    board[player.Row + 1, player.Col - 1] == character.ENEMY ||
                    board[player.Row, player.Col - 1] == character.ENEMY ||
                    board[player.Row - 1, player.Col - 1] == character.ENEMY)
                    enemy.Health_Point -= damage;
            }
            else if (player.Row == 0)
            {
                if (player.Col == 0)
                {
                    if (board[player.Row, player.Col + 1] == character.ENEMY ||
                        board[player.Row + 1, player.Col + 1] == character.ENEMY ||
                        board[player.Row + 1, player.Col] == character.ENEMY)
                        enemy.Health_Point -= damage;
                }
                else if (player.Col == 5)
                {
                    if (board[player.Row + 1, player.Col] == character.ENEMY ||
                        board[player.Row + 1, player.Col - 1] == character.ENEMY ||
                        board[player.Row, player.Col - 1] == character.ENEMY)
                        enemy.Health_Point -= damage;
                }
                else
                {
                    if (board[player.Row, player.Col + 1] == character.ENEMY ||
                        board[player.Row + 1, player.Col + 1] == character.ENEMY ||
                        board[player.Row + 1, player.Col] == character.ENEMY ||
                        board[player.Row + 1, player.Col - 1] == character.ENEMY ||
                        board[player.Row, player.Col - 1] == character.ENEMY)
                        enemy.Health_Point -= damage;
                }
            }
            else if (player.Row == 4)
            {
                if (player.Col == 0)
                {
                    if (board[player.Row, player.Col + 1] == character.ENEMY ||
                        board[player.Row - 1, player.Col + 1] == character.ENEMY ||
                        board[player.Row - 1, player.Col] == character.ENEMY)
                        enemy.Health_Point -= damage;
                }
                else if (player.Col == 5)
                {
                    if (board[player.Row - 1, player.Col] == character.ENEMY ||
                        board[player.Row - 1, player.Col - 1] == character.ENEMY ||
                        board[player.Row, player.Col - 1] == character.ENEMY)
                        enemy.Health_Point -= damage;
                }
                else
                {
                    if (board[player.Row, player.Col + 1] == character.ENEMY ||
                        board[player.Row - 1, player.Col + 1] == character.ENEMY ||
                        board[player.Row - 1, player.Col] == character.ENEMY ||
                        board[player.Row - 1, player.Col - 1] == character.ENEMY ||
                        board[player.Row, player.Col - 1] == character.ENEMY)
                        enemy.Health_Point -= damage;
                }
            }
        }
    }

    private void RefreshBoard()
    {
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 6; j++)
            board[i, j] = character.EMPTY;

        SetLocationOnPlatform(player_pos);
        SetLocationOnPlatform(enemy_pos);

        SetObjectRowNColumn();
    }
}