using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private GameObject target;
    private GameObject actions;

    void Start()
    {
        actions = GameObject.Find("Actions");
        actions.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
            if (target == this.gameObject)
            {
                Debug.Log("플레이어 클릭");
                actions.SetActive(true);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            actions.SetActive(false);
        }    
    }

    void CastRay()
    {
        target = null;

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        { 
            Debug.Log (hit.collider.name); 
            target = hit.collider.gameObject;
        }
    }
}
