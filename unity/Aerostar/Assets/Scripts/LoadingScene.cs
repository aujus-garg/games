using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public float waitTime;
    public int loadIndex;

    void Update()
    {
        waitTime -= Time.deltaTime;
        if (waitTime < 0)
        {
            SceneManager.LoadScene(loadIndex);
        }
    }
}
