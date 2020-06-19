using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// BGM 관련 스크립트
public class Sound : MonoBehaviour
{
    public Slider backVolume;
    public AudioSource audio;
    private float backVol = 1f;     
    
    //
    private void Start()
    {
        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        backVolume.value = backVol;
        audio.volume = backVolume.value;
    }

    //볼륨 값이 바뀔때마다 바로 바꿔줌
    void Update()
    {
        soundSlider();
    }

    //볼륨 조절시 슬라이드 기능이 작동되도록 하는 함수
    private void soundSlider()
    {
        audio.volume = backVolume.value;

        backVol = backVolume.value;
        PlayerPrefs.GetFloat("backvol", backVol);

    }

}
