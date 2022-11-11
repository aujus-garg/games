using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private void OnCollisionEnter(Collision collisionObject)
    {
        PlayerController player = collisionObject.gameObject.transform.GetComponent<PlayerController>();
        if (player != null)
        {
            player.trampJump();
        }
    }
}
