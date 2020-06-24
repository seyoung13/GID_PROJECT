using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

#if UNITY_EDITOR
        using Debug = UnityEngine.Debug;
#endif

public class TileRange : MonoBehaviour
{
    public Transform target;
    public Tilemap tilemap;
    public List<Vector3> tileWorldLocations;
    float tileDistance;
    public int move_point;

    private void OnMouseDown()
    {
        move_point -= 1;
    }

        void Start()
    {
        //각 타일맵의 좌표를 월드상의 좌표로 바꿔서 리스트에 저장
        tileWorldLocations = new List<Vector3>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = tilemap.CellToWorld(localPlace);
            if (tilemap.HasTile(localPlace))
            {
                tileWorldLocations.Add(place);
            }
        }

        //print(tileWorldLocations);
    }

    void Update()
    {
        foreach (Vector3 tile_vec in tileWorldLocations)
        {
            Vector3 temp = new Vector3(-1.25f, -1.6f, 0.0f);

            //리스트에 있는 각 월드 좌표와 target의 월드 좌표의 거리
            //tileDistance = Vector3.Distance(this.target.position, tile_vec-temp);

            Vector3Int target_ontile = this.tilemap.WorldToCell(this.target.position);
            Vector3Int v3int = this.tilemap.WorldToCell(tile_vec);


            //원점
            for (int i = -move_point; i <= move_point; i++)
            {
                if (i < 1)
                {
                    for (int j = (i + move_point); j >= -(i + move_point); j--)
                    {
                        if (target_ontile.x == v3int.x - i && target_ontile.y == v3int.y + 2 + j)
                        {
                            this.tilemap.SetTileFlags(v3int, TileFlags.None);
                            this.tilemap.SetColor(v3int, Color.red);
                        }
                    }
                }
                else
                {
                    for (int j = (i- move_point); j <= -(i- move_point); j++)
                    {
                        if (target_ontile.x == v3int.x - i && target_ontile.y == v3int.y + 2 + j)
                        {
                            this.tilemap.SetTileFlags(v3int, TileFlags.None);
                            this.tilemap.SetColor(v3int, Color.red);
                        }
                    }
                }
            }
        }
    }
}
