using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 10f;
    public GameObject reward;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameObject tramp = Instantiate(reward);
            tramp.transform.position += transform.position - transform.up * 5;
            Destroy(gameObject);
        }
    }
}
