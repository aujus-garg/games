using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private float startY;
    public int sceneNumber;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        float newY = Mathf.Sin(Time.time * 2) / 2;
        transform.position = new Vector3(pos.x, newY + startY, pos.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(sceneNumber);
        }
    }
}
