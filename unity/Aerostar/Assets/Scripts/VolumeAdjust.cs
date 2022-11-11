using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeAdjust : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volume", 0);
    }
    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
    }
}
