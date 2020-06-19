using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Variable : MonoBehaviour
{
    public float sfxVol;
    public float bgmVol;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }

}
 