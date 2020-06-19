using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class PathFinding : MonoBehaviour
{
    public Tilemap tilemap;
    private int target_row, target_col;
    static public bool is_clicked = false;
    public int TargetRow
    { set { target_row = value; } get { return target_row; } }
    public int TargetCol
    { set { target_col = value; } get { return target_col; } }

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
            Debug.Log("target: " + target_row.ToString() + target_col.ToString());
            is_clicked = true;
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

            this.tilemap.SetTileFlags(v3int, TileFlags.None);
            this.tilemap.SetColor(v3int, Color.red);

            Debug.Log("tile"+(-(y+1)).ToString()+(x+3).ToString());
        }
    }

    private void OnMouseExit()
    {
        this.tilemap.RefreshAllTiles();
    }
}
