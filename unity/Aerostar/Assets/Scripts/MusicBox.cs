using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicBox : MonoBehaviour
{
    public AudioMixer audioMixer;

    void Update()
    {
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("volume"));
    }
}
