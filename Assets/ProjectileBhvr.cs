using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBhvr : MonoBehaviour
{
    public Item item;
    public float speed=10f;
    public float lifetime=5f;
    public GameObject shooter;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,lifetime);
    }

    // FixedUpdate is called at a fixed time period
    private void FixedUpdate()
    {
        transform.position+=(transform.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        //ContactPoint contact = collision.contacts[0];
        //Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 position = contact.point;
        //Instantiate(explosionPrefab, position, rotation);
        if(collision.collider.gameObject!=shooter){
            Debug.Log("HIT! "+collision.collider.name);
            CharStats targetStats = collision.gameObject.GetComponent<CharStats>();
            if(targetStats!=null){
                targetStats.TakeDamage(item.atk, targetStats);
            }
            //Destroy projectile
            Destroy(gameObject);
        }
    }

}
