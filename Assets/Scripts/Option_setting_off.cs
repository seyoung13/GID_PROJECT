using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Option_setting_off : MonoBehaviour
{
    GameObject m_goText;
    float m_fCount = 0;
    bool m_bPause = false;

    void Update()
    {
        if (!m_bPause)
        {
            m_fCount += Time.deltaTime;
            m_goText.GetComponent<Text>().text = "";
            m_goText.GetComponent<Text>().text += (int)m_fCount;
        }
        // 백버튼을 눌렀을 경우 게임 종료
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    // 임의로 버튼에 OnApplicationPause호출
    public void SetPause()
    {
        OnApplicationPause(!m_bPause);
    }
    // 백버튼 눌렀을 때 발생하는 함수
    void OnApplicationPause(bool pauseStatus)
    {
        m_bPause = pauseStatus;
    }

}