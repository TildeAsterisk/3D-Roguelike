using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharStats))]
public class CharCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    [SerializeReference]
    CharStats thisStats;
    // Start is called before the first frame update
    void Start()
    {
       thisStats = GetComponent<CharStats>();
    }

    // Update is called once per frame
    void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharStats targetStats){
        if(attackCooldown<=0f){
            targetStats.TakeDamage(thisStats.Damage.GetValue(), thisStats);
            attackCooldown=1f/attackSpeed;
        }
    }
}
