using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;



public class TileMapChoose : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject goCube;

  
    //마우스가 타일 위에 위치할 때만 작업할 것이기 때문에 onMouseOver를 사용

    private void OnMouseOver()
    {
        UnityEngine.Debug.Log("mouse is over GameObject");

        try
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            UnityEngine.Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 3.5f);


            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);


            if (this.tilemap == hit.transform.GetComponent<Tilemap>())
            {
                this.tilemap.RefreshAllTiles();

                int x, y;
                x = this.tilemap.WorldToCell(ray.origin).x;
                y = this.tilemap.WorldToCell(ray.origin).y;

                Vector3Int v3Int = new Vector3Int(x, y, 0);



                //타일 색 바꿀 때 이게 있어야 하더군요
                this.tilemap.SetTileFlags(v3Int, TileFlags.None);

                //타일 색 바꾸기
                this.tilemap.SetColor(v3Int, (Color.red));

            }
        }
        catch (NullReferenceException ie)
        {
            print(ie);
        }
      
    }
    //디버그용으로 마우스가 타일에 들어갈 때 호출됨

  
    private void OnMouseExit()
    {
        this.tilemap.RefreshAllTiles();
    }

    private void Update()
    {

        try
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                UnityEngine.Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 3.5f);

                RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);



                if (this.tilemap == hit.transform.GetComponent<Tilemap>())
                {
                    int x, y;
                    x = this.tilemap.WorldToCell(ray.origin).x;
                    y = this.tilemap.WorldToCell(ray.origin).y;



                    Vector3Int v3Int = new Vector3Int(x, y+2, 0);

                    StartCoroutine(this.Move(this.tilemap.CellToWorld(v3Int)));
                }
            }
        }
        catch (NullReferenceException)
        {

        }
    }
    IEnumerator Move(Vector3 target)
    {
        this.goCube.transform.LookAt(target);
        float disOld = 100000;

        while (true)
        {
            this.goCube.transform.Translate(Vector3.forward * Time.deltaTime * 5);
            var distance = Vector3.Distance(goCube.transform.position, target);
            yield return null;

            if (disOld < distance)
                break;
            disOld = distance;

        }

        this.goCube.transform.rotation = new Quaternion(0, 0, 0, 0);

        this.goCube.transform.position = target;
    }

}