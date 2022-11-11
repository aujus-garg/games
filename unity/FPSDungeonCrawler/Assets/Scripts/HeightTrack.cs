using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightTrack : MonoBehaviour
{
    public GameObject player;
    public Text currentHeight;
    public Text recordHeights;
    private float height;
    private float sessionRecord;
    private float deviceRecord;

    void Start()
    {
        deviceRecord = PlayerPrefs.GetFloat("deviceRecord");
    }

    void Update()
    {
        height = player.transform.position.y;
        currentHeight.text = "Current Height: " + height.ToString("F2");

        if (height >= sessionRecord)
        {
            sessionRecord = height;
        }
        if (height >= deviceRecord)
        {
            PlayerPrefs.SetFloat("deviceRecord", height);
            deviceRecord = height;
        }

        recordHeights.text = "Session Record: " + sessionRecord.ToString("F2") + "\nDevice Record: " + deviceRecord.ToString("F2");
    }
}
