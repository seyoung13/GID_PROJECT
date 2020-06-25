using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

#if UNITY_EDITOR
        using Debug = UnityEngine.Debug;
#endif

public class PathFinding : MonoBehaviour
{
    public Transform target;
    float tileDistance;
    public static int move_point;

    public Tilemap tilemap_range;
    public Tilemap tilemap;

    private int target_row, target_col;
    
    static public bool is_clicked = false;
    
    public List<Vector3> tileWorldLocations;
    public List<int> TileRow;
    public List<int> TileCol;
    public Actions player_action;
    private Vector3Int target_ontile;

    public int TargetRow
    { set { target_row = value; } get { return target_row; } }
    public int TargetCol
    { set { target_col = value; } get { return target_col; } }

    void Start()
    {
        target_ontile = this.tilemap.WorldToCell(this.target.position);

        //각 타일맵의 좌표를 월드상의 좌표로 바꿔서 리스트에 불러옴
        TileRow = new List<int>();
        TileCol = new List<int>();
        tileWorldLocations = new List<Vector3>();
        foreach (var pos in tilemap_range.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = tilemap_range.CellToWorld(localPlace);
            if (tilemap_range.HasTile(localPlace))
            {
                tileWorldLocations.Add(place);
            }
        }

        foreach (Vector3 tile_vec in tileWorldLocations)
        {
            if (tile_vec.x == tileWorldLocations[0].x)
            {
                TileRow.Add((int)(tile_vec.y/ 0.75));
            }
            if (tile_vec.y == tileWorldLocations[0].y)
            {
                TileCol.Add((int)(tile_vec.x / 2.5 ));
            }
        }

        //print(tileWorldLocations);
    }

    void Update()
    {
        // 현재 상태에 따라 나타나게 끔 해야함 
        this.tilemap_range.RefreshAllTiles();
        target_ontile = this.tilemap.WorldToCell(this.target.position);

        foreach (int y in TileRow)
        {
            foreach (int x in TileCol)
            {
                Vector3Int v3int = new Vector3Int(x, y, 0);
                //플레이어 중심의 마름모 꼴 범위
                if (Math.Abs(x - target_ontile.x) + Math.Abs(y - target_ontile.y + 2) <= move_point)
                {
                    this.tilemap_range.SetTileFlags(v3int, TileFlags.None);
                    this.tilemap_range.SetColor(v3int, Color.red);
                }
            }
        }

        /*
        Debug.Log("Skill Tile:"+player_action.Action_Name);
        switch (player_action.Action_Name)
        {
            case "Skill1":
                for (int i=0; i<3; i++)
                {
                    Vector3Int v3 = new Vector3Int(-2+i, -5, 0);

                    this.tilemap_range.SetTileFlags(v3, TileFlags.None);
                    this.tilemap_range.SetColor(v3, Color.yellow);
                }
               break;
            case "Skill2":
                for (int i=0; i<3; i++)
                {
                    Vector3Int v3 = new Vector3Int(target_ontile.x + i, target_ontile.y - 3, 0);

                    this.tilemap_range.SetTileFlags(v3, TileFlags.None);
                    this.tilemap_range.SetColor(v3, Color.yellow);

                    v3 = new Vector3Int(target_ontile.x + i, target_ontile.y - 1, 0);

                    this.tilemap_range.SetTileFlags(v3, TileFlags.None);
                    this.tilemap_range.SetColor(v3, Color.yellow);
                }
                break;
            case "Skill3":
                for (int i = 0; i < 3; i++)
                    for(int j=0; j< 3; j++)
                    {
                        Vector3Int v3 = new Vector3Int(target_ontile.x -1 + i, target_ontile.y -3 +j, 0);

                        this.tilemap_range.SetTileFlags(v3, TileFlags.None);
                        this.tilemap_range.SetColor(v3, Color.yellow);
                    }
                break;
        }*/
    }


    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);

        if (this.tilemap = hit.transform.GetComponent<Tilemap>())
        {
           
            this.tilemap.RefreshAllTiles();
            int x = this.tilemap.WorldToCell(ray.origin).x;
            int y = this.tilemap.WorldToCell(ray.origin).y;
            Vector3Int v3int = new Vector3Int(x, y, 0);

            this.tilemap.SetTileFlags(v3int, TileFlags.None);
            this.tilemap.SetColor(v3int, Color.red);
        
            target_row = -(y + 1);
            target_col = x + 3;
            //Debug.Log("target: " + target_row.ToString() + target_col.ToString());

            //행동 수치가 0이하면 클릭 불가 - 추후 수정
            if (move_point <= 0)
                is_clicked = false;
            
            //범위 이내에 있다면 클릭 가능 
            else if (Math.Abs(x - target_ontile.x) + Math.Abs(y - target_ontile.y + 2) <= move_point)
            {
                is_clicked = true;
                move_point -= Math.Abs(x - target_ontile.x) + Math.Abs(y - target_ontile.y + 2);
            }
            //범위 밖에 있다면 클릭 불가
            else
                is_clicked = false;
        }
    }

    private void OnMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);


        if (this.tilemap = hit.transform.GetComponent<Tilemap>())
        {

            this.tilemap.RefreshAllTiles();
            int x = this.tilemap.WorldToCell(ray.origin).x;
            int y = this.tilemap.WorldToCell(ray.origin).y;
            Vector3Int v3int = new Vector3Int(x, y, 0);

            if (Math.Abs(x - target_ontile.x) + Math.Abs(y - target_ontile.y + 2) <= move_point)
            {
                this.tilemap.SetTileFlags(v3int, TileFlags.None);
                this.tilemap.SetColor(v3int, Color.green);
            }
            else
            {
                this.tilemap.SetTileFlags(v3int, TileFlags.None);
                this.tilemap.SetColor(v3int, Color.red);
            }

            //Debug.Log("tile"+(-(y+1)).ToString()+(x+3).ToString());


        }
    }

    private void OnMouseExit()
    {
        this.tilemap.RefreshAllTiles();
    }

    private int BoardToCellY(int j)
    {
        return j - 3;
    }

    private int BoardToCellX(int i)
    {
        return -(i + 1);
    }


}
