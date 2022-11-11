using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public bool pointSet;
    private float startY;

    void Start()
    {
        startY = transform.position.y;

    }

    void Update()
    {
        Vector3 pos = transform.position;
        float newY = Mathf.Sin(Time.time * 2) / 2;
        transform.position = new Vector3(pos.x, newY + startY, pos.z);
        if (pointSet)
        {
            transform.Rotate(Vector3.down * 360 * Time.deltaTime * Mathf.Tan(Time.time));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().respawnPoint != null)
            {
                other.GetComponent<PlayerController>().respawnPoint.GetComponent<RespawnPoint>().pointSet = false;
            }
            other.GetComponent<PlayerController>().respawnPoint = gameObject;
            pointSet = true;
        }
    }
}
