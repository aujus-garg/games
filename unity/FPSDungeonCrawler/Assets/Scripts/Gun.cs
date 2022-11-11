using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float timeSinceShoot = -0.5f;
    public float reloadSpeed = 0.5f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public LayerMask playerMask;
    public AudioSource laserSFX;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time - timeSinceShoot > reloadSpeed)
        {
            Shoot();
            timeSinceShoot = Time.time;
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        laserSFX.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, playerMask))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 1f);
        }
    }
}
