using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour
{
    // 해당 스크립트가 포홤된 오브젝트는 씬 이동시 자동으로 파괴되지 않는다. 단, 최상위 오브젝트에 첨부해야함 - 임준
    private static DontDestroy s_Instance = null;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public static DontDestroy instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = new GameObject("DontDestroy").AddComponent<DontDestroy>();
                //오브젝트가 생성이 안되있을경우 생성.
            }
            return s_Instance;
        }
    }


    void OnApplicationQuit()
    {
        s_Instance = null;
        //게임종료시 삭제.
    }
}