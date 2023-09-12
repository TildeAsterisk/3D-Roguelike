using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public GameObject[] spawnPrefabs;
    public int points;
    private CharStats playerStats;
    public Text pointsTxt;
    public void UpdateUIText(){
        pointsTxt.text = "Points: "+points;
    }
    public GameObject upgradesPopup;
    public bool upgradesShowing=false;
    public void DisplayUpgradePopup(int choice_num=0){
        switch(choice_num){
            case -1:
                //Close Panel
                upgradesPopup.SetActive(false);
                Time.timeScale = 1;
                upgradesShowing=false;
                break;
            case 0:
                //Open Panel
                upgradesPopup.SetActive(true);
                upgradesShowing=true;
                Time.timeScale = 0;
                
                break;
            case 1:
                //Offensive Upgrade
                playerStats.Damage.AddModifier(10);
                Time.timeScale = 1;
                Debug.Log("Damage Upgrade Applied.");
                upgradesPopup.SetActive(false);
                //upgradesShowing=false;
                break;
            case 2:
                //Defensive Upgrade
                playerStats.Armour.AddModifier(10);
                upgradesPopup.SetActive(false);
                //upgradesShowing=false;
                Time.timeScale = 1;
                Debug.Log("Armour Upgrade Applied.");
                break;
            case 3:
                //Movement upgrade
                playerStats.Speed.AddModifier(1);
                upgradesPopup.SetActive(false);
                //upgradesShowing=false;
                Time.timeScale = 1;
                Debug.Log("Speed Upgrade Applied.");
                break;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // oks
        playerStats=player.GetComponent<CharStats>();
        DisplayUpgradePopup(-1);
    }

    // Update is called once per frame    public GameObject[] characters;
    public float spawnRate = 1;
    public bool spawning;
    private float nextSpawnTime;
    private GameObject[] spawnedObjects;
    private int tmpint;

    void Update()
    {
        // Check if it is time to spawn a new character.
        if (Time.time >= nextSpawnTime && spawning)
        {
            // Spawn a new character at a random position in the area.
            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-10, 11), 0, UnityEngine.Random.Range(-10, 11));
            int randomIndex = UnityEngine.Random.Range(0, spawnPrefabs.Length);
            GameObject newSpawn= Instantiate(spawnPrefabs[randomIndex], spawnPosition, Quaternion.identity);
            newSpawn.GetComponent<EnemyController>().focus=player;
            // Add to array
            //Array.Resize(ref spawnedObjects, spawnedObjects.Length + 1);
            //spawnedObjects[spawnedObjects.Length - 1] = newSpawn;

            // Set the next spawn time.
            nextSpawnTime = Time.time + spawnRate;
        }

        //Points Logic
        if(points % 5 ==0 && points!=0 && !upgradesShowing){
            Time.timeScale = 0;
            DisplayUpgradePopup(0);
            upgradesShowing=true;
            tmpint = points;
        }
        if (points==tmpint+1){
                upgradesShowing=false;
            }

        UpdateUIText();
    }

}
