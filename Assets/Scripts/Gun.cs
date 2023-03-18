using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public LayerMask puff;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward * range, out hit, range, puff))
        {
            Debug.Log(hit.transform.name);

        

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range, Color.green, range);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.transform.forward * impactForce);
            }

            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 10f);

        }
        else
        {
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range, Color.red, range);
        }
    }
}
