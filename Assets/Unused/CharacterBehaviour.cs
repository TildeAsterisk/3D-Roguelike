using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterBehaviour : MonoBehaviour
{
        public enum charType{
        GoodGuy,
        Neutral,
        Animal,
        BadGuy
    }
    public charType type;
    private CharStats charStats;
    private CharCombat charCombat;
    public GameManager gameManager;

    private NavMeshAgent agent;
    [SerializeField]
    public NavMeshAgent focus;
    public float wanderRadius = 10f;
    private float wanderTimer = 0f;
    private float wanderInterval = 1.5f;
    public float lookradius =10f;

    public void Wander(NavMeshAgent agent){
        // Check if it's time to wander.
        wanderTimer += Time.deltaTime;
        if (wanderTimer >= wanderInterval)
        {
            // Generate a random position within the wander radius.
            Vector3 newPosition = Random.insideUnitSphere * wanderRadius;
            newPosition += transform.position;

            // Set the agent's destination to the random position.
            agent.destination = newPosition;

            // Reset the wander timer.
            wanderTimer = 0f;

            //FindCharsInRange(transform.position, lookradius);
        }
    }

    void FindCharsInRange(Vector3 centre, float radius,charType? cType){
        Collider[] hitColliders = Physics.OverlapSphere(centre,radius);
        List<CharacterBehaviour> charsInRange = new List<CharacterBehaviour>();
        CharacterBehaviour foundChar = null;
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.layer == 6 && hitCollider.transform.parent != transform){
                foundChar = hitCollider.transform.parent.GetComponent<CharacterBehaviour>();
                charsInRange.Add(foundChar);
                if(foundChar.type == cType){
                    focus=foundChar.agent;
                    Debug.Log("Focusing "+focus.transform.name);
                }
            } 
        }
    }

    void FaceTarget(){
        Vector3 direction = (focus.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation=Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*5f);
    }


    //======================================\\

    void Start()
    {
        // Get the NavMeshAgent component.
        agent = GetComponent<NavMeshAgent>();
        charStats = GetComponent<CharStats>();
        charCombat=GetComponent<CharCombat>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        //setfocus
        if(focus == null){
            switch(type){
            case charType.GoodGuy: //Find bad guy and set focus
                FindCharsInRange(transform.position, lookradius,charType.BadGuy);
                break;
            case charType.BadGuy: //setfocus good guy/
                FindCharsInRange(transform.position, lookradius,charType.Neutral);
                FindCharsInRange(transform.position, lookradius,charType.GoodGuy);
                break;
            case charType.Neutral:
                break;
            case charType.Animal:
                FindCharsInRange(transform.position, lookradius,charType.GoodGuy);
                break;
            }
        }
        else if (focus != null && type!=charType.Animal){
            //has focus
            float distance=Vector3.Distance(focus.transform.position, transform.position);
            if(distance <= agent.stoppingDistance){
                CharStats targetStats = focus.GetComponent<CharStats>();
                if (targetStats!=null){
                    try{
                        charCombat.Attack(targetStats);
                    }
                    catch{
                        Debug.Log("Cannot attack target " + targetStats.transform.name);
                    }
                }
                FaceTarget();
            }
        }
        
        //Depengint on char type move...
        switch (type){
            case charType.GoodGuy:
                if(focus){
                    agent.destination = focus.transform.position;
                }
                else{
                    Wander(agent);
                }
                break;
            case charType.BadGuy:
                if(focus){
                    agent.destination = focus.transform.position;
                }
                else{
                    Wander(agent);
                }
                break;
            case charType.Neutral:
                if(focus){
                    agent.destination = focus.transform.position;
                }
                else{
                    Wander(agent);
                }
                break;
            case charType.Animal:
                if(focus){
                    agent.destination = focus.transform.position-(focus.transform.forward*3);
                }
                else{
                    Wander(agent);
                }
                break;
        }
        
    }

    void OnDrawGizmosSelected(){
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position,lookradius);
    }
    
}
