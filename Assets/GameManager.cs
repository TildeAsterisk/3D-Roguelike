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
    public Text GameTimeTxt;
    public float gameTimeCounter =0;

    public void UpdateUIText(){
        pointsTxt.text = /*"Points: "+*/points.ToString();
    }
    public void UpdateGameTimeUIText(){
        //string h,m,s,ms;
        //s=((int)gameTimeCounter).ToString();
        TimeSpan time = TimeSpan.FromSeconds(gameTimeCounter);
        string formattedTime = time.ToString(@"h\:mm\:ss");
        GameTimeTxt.text = formattedTime;
    }
    public Text StatsUITxt;
    public void UpdateStatsUITxt(){
        StatsUITxt.text = "ATK: "+playerStats.Damage.GetValue()+"\nDEF: "+playerStats.Armour.GetValue()+"\nSPD: "+playerStats.Speed.GetValue();
    }

    //[SerializeField]
    public List<Item> artefacts = new List<Item>();
    public GameObject upgradesPopup;
    public bool upgradesShowing=false;

    public Sprite UpgradeSprite1;
    public Text UpgradeNameTxt1;
    public Text upgradeDescTxt1;
    public Sprite UpgradeSprite2;
    public Text UpgradeNameTxt2;
    public Text upgradeDescTxt2;
    public Sprite UpgradeSprite3;
    public Text UpgradeNameTxt3;
    public Text upgradeDescTxt3;
    public Sprite[] upgradeIcons;
    public void DisplayUpgradePopup(int choice_num=0){
        //Initialize 3 new random items
        Item newOffensiveItemUpgrade = ScriptableObject.CreateInstance<Item>();
        newOffensiveItemUpgrade.InitializeNewRandomItem(newOffensiveItemUpgrade);
        newOffensiveItemUpgrade.icons=upgradeIcons;
        Item newDefensiveItemUpgrade = ScriptableObject.CreateInstance<Item>();
        newDefensiveItemUpgrade.InitializeNewRandomItem(newDefensiveItemUpgrade);
        newDefensiveItemUpgrade.icons=upgradeIcons;
        Item newMovementItemUpgrade = ScriptableObject.CreateInstance<Item>();
        newMovementItemUpgrade.InitializeNewRandomItem(newMovementItemUpgrade);
        newMovementItemUpgrade.icons=upgradeIcons;

        UpgradeSprite1=newOffensiveItemUpgrade.icon;
        UpgradeNameTxt1.text=newOffensiveItemUpgrade.name;
        upgradeDescTxt1.text=newOffensiveItemUpgrade.description+"\nATK: "+newOffensiveItemUpgrade.atk;
        UpgradeSprite2=newDefensiveItemUpgrade.icon;
        UpgradeNameTxt2.text=newDefensiveItemUpgrade.name;
        upgradeDescTxt2.text=newDefensiveItemUpgrade.description+"\nDEF: "+newDefensiveItemUpgrade.def;
        UpgradeSprite3=newMovementItemUpgrade.icon;
        UpgradeNameTxt3.text=newMovementItemUpgrade.name;
        upgradeDescTxt3.text=newMovementItemUpgrade.description+"\nSPD: "+newMovementItemUpgrade.speed;
        
        //Set Text Displays in Upgrade Menu according to Generated Items!


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
                //Item newOffensiveItemUpgrade = ScriptableObject.CreateInstance<Item>();
                //newOffensiveItemUpgrade.InitializeNewRandomItem(newOffensiveItemUpgrade);
                playerStats.playerInv.items.Add(newOffensiveItemUpgrade);
                playerStats.AddItemModifier(newOffensiveItemUpgrade, playerStats.Damage,newOffensiveItemUpgrade.atk);
                Time.timeScale = 1;
                Debug.Log("Damage Upgrade Applied.");
                upgradesPopup.SetActive(false);
                //upgradesShowing=false;

                //Destroy other items
                Destroy(newDefensiveItemUpgrade);
                Destroy(newMovementItemUpgrade);
                break;
            case 2:
                //Defensive Upgrade
                //Item newDefensiveItemUpgrade = ScriptableObject.CreateInstance<Item>();
                //newDefensiveItemUpgrade.InitializeNewRandomItem(newDefensiveItemUpgrade);
                playerStats.playerInv.items.Add(newDefensiveItemUpgrade);
                playerStats.AddItemModifier(newDefensiveItemUpgrade, playerStats.Armour,newDefensiveItemUpgrade.def);

                upgradesPopup.SetActive(false);
                //upgradesShowing=false;
                Time.timeScale = 1;
                Debug.Log("Armour Upgrade Applied.");
                //Destroy other items
                Destroy(newOffensiveItemUpgrade);
                Destroy(newMovementItemUpgrade);
                break;
            case 3:
                //Movement upgrade
                //Item newMovementItemUpgrade = ScriptableObject.CreateInstance<Item>();
                //.InitializeNewRandomItem(newMovementItemUpgrade);
                playerStats.playerInv.items.Add(newMovementItemUpgrade);
                playerStats.AddItemModifier(newMovementItemUpgrade, playerStats.Armour,newMovementItemUpgrade.def);

                upgradesPopup.SetActive(false);
                //upgradesShowing=false;
                Time.timeScale = 1;
                Debug.Log("Speed Upgrade Applied.");
                //Destroy other items
                Destroy(newOffensiveItemUpgrade);
                Destroy(newDefensiveItemUpgrade);
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
        DayNightCycle();
        // Check if it is time to spawn a new character.
        if (Time.time >= nextSpawnTime && spawning)
        {
            // Spawn a new character at a random position in the area.
            //Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(player.position.x-15, player.position.x+15), 0, -15);
            //RandomPositionOffScreen();
            
            int randomIndex = UnityEngine.Random.Range(0, spawnPrefabs.Length);
            GameObject newSpawn= Instantiate(spawnPrefabs[randomIndex], RandomPositionOffScreen(), Quaternion.identity);
            newSpawn.GetComponent<EnemyController>().focus=player;
            // Add to array
            //Array.Resize(ref spawnedObjects, spawnedObjects.Length + 1);
            //spawnedObjects[spawnedObjects.Length - 1] = newSpawn;

            // Set the next spawn time.
            nextSpawnTime = Time.time + spawnRate;
        }

        //Points Logic
        if(points % 10 ==0 && points!=0 && !upgradesShowing){
            Time.timeScale = 0;
            DisplayUpgradePopup(0);
            upgradesShowing=true;
            tmpint = points;
        }
        if (points==tmpint+1){
                upgradesShowing=false;
            }

        UpdateUIText();
        gameTimeCounter+=Time.deltaTime;
        UpdateGameTimeUIText();
        UpdateStatsUITxt();
    }

    public float spawnDist;
    public Vector3 RandomPositionOffScreen(){
        int nesw =UnityEngine.Random.Range(1,5);
        Vector3 spawnPosition;
        switch (nesw){
            case 1:
                //north
                spawnPosition = new Vector3(UnityEngine.Random.Range(player.position.x-spawnDist, player.position.x+spawnDist), 0, spawnDist);
                break;
            case 2:
                //East
                spawnPosition = new Vector3(spawnDist, 0, UnityEngine.Random.Range(player.position.z-spawnDist, player.position.z+spawnDist));
                break;
            case 3:
                //South
                spawnPosition = new Vector3(UnityEngine.Random.Range(player.position.x-spawnDist, player.position.x+spawnDist), 0, -spawnDist);
                break;
            case 4:
                //West
                spawnPosition = new Vector3(-spawnDist, 0, UnityEngine.Random.Range(player.position.z-spawnDist, player.position.z+spawnDist));
                break;
            default:
                //Default to South
                spawnPosition = new Vector3(UnityEngine.Random.Range(player.position.x-spawnDist, player.position.x+spawnDist), 0, -spawnDist);
                break;
        }
        return spawnPosition;
    }

    public Light directionalLight;
    public float cycleDuration = 600f; // 10 minutes in seconds
    private float intensityMin = 0f;
    private float intensityMax = 1.5f;
    public void DayNightCycle(){
        float time = gameTimeCounter % cycleDuration;
        float intensity = Mathf.Lerp(intensityMin, intensityMax, time / cycleDuration);
        //print(intensity);
        directionalLight.intensity = intensity;
    }

}
