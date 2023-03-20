using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public AudioClip shootSound;
    public float soundIntensity = 5f;
    public float walkEnemyPerceptionRadius = 1f;
    public float sprintEnemyPerceptionRadius = 1.5f;
    public LayerMask zombieLayer;

    private AudioSource audioSource;
    private PlayerController plc;
    private SphereCollider sphereCollider;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        plc.GetComponent<PlayerController>();
        sphereCollider = GetComponent<SphereCollider>();
    }


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (plc.GetPlayerStealthProfile() == 0)
        {
            sphereCollider.radius = walkEnemyPerceptionRadius;
        }
        else
        {
            sphereCollider.radius = sprintEnemyPerceptionRadius;
        }
    }


    public void Shoot()
    {
        audioSource.PlayOneShot(shootSound);
        Collider[] zombies = Physics.OverlapSphere(transform.position, soundIntensity, zombieLayer);
        for (int i = 0; i < zombies.Length; i++)
        {
            zombies[i].GetComponent<AIZombie>().OnAware();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            other.GetComponent<AIZombie>().OnAware();
        }
    }
}
