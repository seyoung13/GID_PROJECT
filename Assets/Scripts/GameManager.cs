using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menu_set;
    private int stage_number;

    void Start()
    {
        stage_number = 0;
        menu_set.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel")) 
        if (menu_set.activeSelf)
            menu_set.SetActive(false);
        else
            menu_set.SetActive(true);
    }

    public void PressStart()
    {
        stage_number += 1;
        SceneManager.LoadScene("stage"+stage_number.ToString());
    }
    public void PressExit()
    {
        //유니티 에디터에서 종료
        UnityEditor.EditorApplication.isPlaying = false;
        //게임에서 종료
        //Application.Quit();
    }
    public void PressBackTitle()
    {
        menu_set.SetActive(false);
        stage_number = 0;
        SceneManager.LoadScene("Title");
    }
}
