using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIZombie : MonoBehaviour
{

    public PlayerController plc;
    public float fov = 120f;
    public float viewDistance = 10f;
    public bool isAware = false;
    private NavMeshAgent agent;
    private Renderer renderer;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<Renderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (isAware)
        {
            agent.SetDestination(plc.transform.position);
            renderer.material.color = Color.red;
        }
        else
        {
            SearchForPlayer();
            renderer.material.color = Color.green;
        }
    }

    public void SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(plc.transform.position)) < fov / 2f)
        {
            if (Vector3.Distance(plc.transform.position, transform.position) < viewDistance)
            {
                OnAware();
            }
        }
    }

    public void OnAware()
    {
        isAware = true;
    }
}
