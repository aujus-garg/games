using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStars : MonoBehaviour
{
    public float randomizer = 1;

    void Update()
    {
        Vector3 pos = transform.position;
        float newY = Mathf.Tan(Time.time * 2 + randomizer);
        transform.position = new Vector3(pos.x, newY / 2 + 1, pos.z);
        transform.Rotate(Vector3.down * 360 * Time.deltaTime * Mathf.Tan(Time.time + randomizer));
    }
}
