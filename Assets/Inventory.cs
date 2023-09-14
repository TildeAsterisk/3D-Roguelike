using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void Add (Item item){
        if(!item.isDefaultItem){
            items.Add(item);
        }
    }

    public void Remove(Item item){
        items.Remove(item);
    }

    IEnumerator CooldownTimer(Item item)
    {
        item.isOnCooldown = true;
        float timeLeft = item.cooldown;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        item.isOnCooldown = false;
        ResetCooldown(item);
    }

    public void StartCooldown(Item item)
    {
        StartCoroutine(CooldownTimer(item));
    }

    public void ResetCooldown(Item item)
    {
        StopCoroutine(CooldownTimer(item));
        item.isOnCooldown = false;
    }

    void Start()
    {
        //Resetting all cooldowns
        Debug.Log("Resetting all cooldowns.");
        items.ForEach(item =>
        {
            ResetCooldown(item);
        });
    }

   /*  public void ShootProjectile(Item projectileItem){
        //shoot a projectile
        GameObject bullet= Instantiate(projectileItem.prefab, new Vector3(transform.position.x,transform.position.y+1.7f,transform.position.z), transform.rotation);
        bullet.GetComponent<ProjectileBhvr>().shooter=gameObject;
    }  */
}