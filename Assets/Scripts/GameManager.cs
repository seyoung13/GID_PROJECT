using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//게임 스테이지를 관리하는 스크립트
public class GameManager : MonoBehaviour
{
    //상단 메뉴바 오브젝트
    public GameObject menu_set;

    void Start()
    {
        //시작할 땐 메뉴바 비활성화 상태
        menu_set.SetActive(false);
    }

    void Update()
    {
        //Esc 버튼을 누르면 메뉴바 활성화 상태에 따라 키거나 끔
        if (Input.GetButtonDown("Cancel"))
            if (menu_set.activeSelf)
                menu_set.SetActive(false);
            else
                menu_set.SetActive(true);

        //각각 PlayerMove와 PlayerBattle 스크립트에 선언된 전역 변수로 플레이어 현재 상태 파악
        if (PlayerMove.is_engaged_enemy && !PlayerBattle.is_battled)
        { 
            SceneManager.LoadScene("Stage1");
            PlayerMove.is_engaged_enemy = false;
            PlayerBattle.is_battled = true;
        }
        if (BattleManager.is_someone_died)
        { 
            SceneManager.LoadScene("Title");
            BattleManager.is_someone_died = false;
        }
    }

    public void PressStart()
    {
        //Title 스테이지에서 시작 버튼을 누르면 Move 스테이지로 이동
       
        //GameObject.Destroy(GameObject.Find("Title")); move로 이동시 해당 오브젝트를 파괴한다.
        //- 메뉴셋을 일일이 호출하지 않게끔 하려고 의도되었으나 아직 개발 상태 - 임준
        SceneManager.LoadScene("Move");
    }
    public void PressExit()
    {
        //유니티 에디터에서 종료
        //엔진 상에서 게임 테스트할땐 이 코드를 사용
        UnityEditor.EditorApplication.isPlaying = false;
        //게임에서 종료
        //Application.Quit();
    }
    public void PressBackTitle()
    {   
        //메뉴바의 back title 버튼을 누르면 title 스테이지로 이동
        menu_set.SetActive(false);
        SceneManager.LoadScene("Title");
    }
}
