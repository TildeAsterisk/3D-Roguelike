using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    private int maxHealth = 100; //not a stat because we dont want modifiers on health
    public int currentHealth { get; private set; }

    public Stat Health;
    public Stat Speed;
    public Stat Damage;
    public Stat Armour;
    public int killCount;
    //public CharacterBehaviour charBhvr;
    public GameManager gameManager;
    public Animator animator;
    //public Inventory inventory;
    public Inventory playerInv;
    public Transform hpBar,manaBar;

    void Start()
    {
        //Health.SetBaseValue(100);
        currentHealth = maxHealth;
        //CharacterBehaviour charBhvr = GetComponent<CharacterBehaviour>();
        gameManager=GameObject.Find("Game Manager").GetComponent<GameManager>();
        animator=GetComponent<Animator>();
        playerInv=GetComponent<Inventory>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        ApplyInventoryEffects();
    }

    public GameObject dmgNumPopup;
    public float dmgTxtLifetime=0.5f;
    public void TakeDamage (float damage, CharStats attacker)
    {   //play anim and spawn dmg text
        GameObject dmgNumTxtObj = Instantiate(dmgNumPopup, transform.position, dmgNumPopup.transform.rotation);
        Destroy(dmgNumTxtObj,dmgTxtLifetime);
        //display hp bar for some time
        if(hpBar){
            SetStatBarSize();
        }

        damage -= Armour.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        //If damage is greated thn zero after armour then play hurt
        if (damage>0){animator.Play("Base Layer.Hurt", 0, 0f);}

        dmgNumTxtObj.GetComponent<TextMesh>().text = "-"+damage.ToString();    //set text to damage value

        currentHealth = (int)(currentHealth-damage);
        //Debug.Log(transform.name + " takes " + damage + " damage. HP:"+currentHealth);
        if(currentHealth <= 0)
        {
            Die(attacker);
        }
    }

    public void Die(CharStats killedBy)
    {
        //Debug.Log(transform.name + " died. Destroying "+ gameObject.name);
        //add killcount to killer, if goodguy, add points
        killedBy.killCount++;
        gameManager.points++;
        /*
        if (killedBy.charBhvr.type == CharacterBehaviour.charType.GoodGuy){
            gameManager.points++;
        }
        */

        //Stop moving when dying
        UnityEngine.AI.NavMeshAgent enemyBhvr = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if(enemyBhvr){  //if char is AI Agent
            //Stop agent
            enemyBhvr.Stop();
        }

        //If character doesnt have a Die animation with an event to destroy the object, then destroy after x seconds
        Destroy(gameObject,2f);
        //play anim which calls Destroy(gameObject) event at the end
        animator.Play("Base Layer.Die", 0, 0f);
    }

    public void DestroyObject(){
        Destroy(gameObject);
    }



    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public void Attack(CharStats targetStats){
        if(attackCooldown<=0f){
            animator.Play("Base Layer.Attacking", 0, 0f);
            targetStats.TakeDamage(Damage.GetValue(), this);
            attackCooldown=1f/attackSpeed;
            //Debug.Log("Attacking "+targetStats.gameObject.name+" DMG:"+Damage.GetValue());
        }
    }

    public void ShootProjectile(Item projectileItem){
        //shoot a projectile
        GameObject bullet= Instantiate(projectileItem.prefab, new Vector3(transform.position.x,transform.position.y+1.7f,transform.position.z), transform.rotation);
        ProjectileBhvr prjctileBhvr = bullet.GetComponent<ProjectileBhvr>();
        prjctileBhvr.shooter=gameObject;
        prjctileBhvr.item=projectileItem;
    }

    public void ApplyInventoryEffects(){
        //apply all effects
        if(playerInv){
        playerInv.items.ForEach(item =>
        {
            // Do something with item
            if(item.ArtefactType is Item.Type.Projectile && !item.isOnCooldown){
                ShootProjectile(item);
                playerInv.StartCooldown(item);
            }
        });
        }
    }

    public void AddItemModifier(Item item, Stat mstat,float modVal){
        mstat.AddModifier((int)modVal);
    }

    public void SetStatBarSize(){
        float hpPercent = (float)currentHealth/(float)maxHealth;
        hpBar.localScale = new Vector3(hpPercent*1.8f,hpBar.localScale.y,hpBar.localScale.z);
    }
}