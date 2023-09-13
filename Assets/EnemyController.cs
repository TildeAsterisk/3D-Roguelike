using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform focus;
    private CharStats stats;

    public float dist;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<CharStats>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = focus.position;
        if(Vector3.Distance(transform.position,focus.position) <= agent.stoppingDistance){
            //Debug.Log("Attack!");
            //stats.Attack(focus.GetComponent<CharStats>());
            
            //Reverse
            focus.GetComponent<CharStats>().Attack(stats);
        }
    }
}
