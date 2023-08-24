using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    private int maxHealth = 100; //not a stat because we dont want modifiers on health
    public int currentHealth { get; private set; }

    public Stat Age;
    public Stat Speed;
    public Stat Damage;
    public Stat Armour;
    public int killCount;
    public CharacterBehaviour charBhvr;
    public GameManager gameManager;

    void Start()
    {
        //Health.SetBaseValue(100);
        currentHealth = maxHealth;
        CharacterBehaviour charBhvr = GetComponent<CharacterBehaviour>();
        gameManager=GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void TakeDamage (int damage, CharStats attacker)
    {
        damage -= Armour.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage. HP:"+currentHealth);
        if(currentHealth <= 0)
        {
            Die(attacker);
        }
    }

    public void Die(CharStats killedBy)
    {
        Debug.Log(transform.name + " died. Destroying "+ gameObject.name);
        //add killcount to killer, if goodguy, add points
        killedBy.killCount++;
        gameManager.points++;
        /*
        if (killedBy.charBhvr.type == CharacterBehaviour.charType.GoodGuy){
            gameManager.points++;
        }
        */

        Destroy(gameObject);
    }



    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public void Attack(CharStats targetStats){
        if(attackCooldown<=0f){
            targetStats.TakeDamage(Damage.GetValue(), this);
            attackCooldown=1f/attackSpeed;
            Debug.Log("Attacking "+targetStats.gameObject.name+" DMG:"+Damage.GetValue());
        }
    }
}