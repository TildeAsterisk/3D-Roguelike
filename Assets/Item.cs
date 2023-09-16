using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName="New Item", menuName="Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public string description="Item Description";
    public Sprite icon = null;
    public bool isDefaultItem=false;
    public enum Type{
        Projectile, //[size,speed,type,phasing?]
        Ally, //atk,def,hp,spd
        SpecialAbility,
        Upgrade
    }
    public Type ArtefactType;
    public float size,speed,atk,def,hp;
    public float cooldown;
    private float currentCooldown;
    public string[] modifiers;
    public GameObject prefab;
    public bool isOnCooldown=false;

    //"Sword", "Axe", "Mace", "Bow", "Staff",
    List<string[]> itemsTxt = new List<string[]> {
    new string[] { "Swords of Goujian", "A pair of ancient Chinese swords that were discovered in 1965 in Hubei province. They are known for their sharpness and durability." },
    new string[] { "Spear of Destiny",  "A spear that is believed to have been used by a Roman soldier to pierce the side of Jesus Christ during his crucifixion. It is said to have mystical powers and has been the subject of many legends throughout history." },
    new string[] { "Linothorax",        "A type of armor that was used by ancient Greeks. It was made from layers of linen that were glued together and then covered with a thin layer of bronze or leather. It was lightweight, flexible, and provided good protection against arrows and other projectiles." },
    new string[] { "Lorica Segmentata", "A type of armor that was used by Roman soldiers. It consisted of overlapping metal plates that were attached to leather straps. It provided good protection against swords and other cutting weapons." },
    new string[] { "Wings of Icarus",   "According to Greek mythology, Icarus flew too close to the sun while wearing wings made of feathers and wax. The wings are a symbol of ambition and the dangers of overreaching." },
    new string[] { "Sandals of Hermes", "According to Greek mythology, Hermes wore winged sandals that allowed him to fly. The sandals are a symbol of speed and agility." },
    new string[] { "Onzil",             "A type of throwing knife that was used in central Africa for warfare and hunting. It had multiple iron blades and was effective up to a range of 50 meters (160 feet)" },
    new string[] { "Kulbeda",           "A type of throwing knife that was used in central Africa for warfare and hunting. It had multiple iron blades and was effective up to a range of 50 meters (160 feet)" },
    new string[] { "Mambele",           "A type of throwing knife that was used in central Africa for warfare and hunting. It had multiple iron blades and was effective up to a range of 50 meters (160 feet)" },
    new string[] { "Pinga",             "A type of throwing knife that was used in central Africa for warfare and hunting. It had multiple iron blades and was effective up to a range of 50 meters (160 feet)" },
    new string[] { "Trombash",          "A type of throwing knife that was used in central Africa for warfare and hunting. It had multiple iron blades and was effective up to a range of 50 meters (160 feet)" },
    };

    string[] names = { "Onzil", "Kulbeda", "Mambele", "Pinga","Trombash","Swords of Goujian","Spear of Destiny","Linothorax","Lorica Segmentata","Wings of Icarus","Boots of Eris"};
    string[] descriptions = { "A powerful weapon", "A deadly tool", "A magical artifact", "A legendary item", "Ancient artifact" };
    public Sprite[] icons = {};
    public Item InitializeNewRandomItem(Item newItem){
        //Item newItem = new Item();
        int nameIndex = Random.Range(0, itemsTxt.Count);
        //int descriptionIndex = Random.Range(0, itemsTxt.Length);
        int iconIndex = Random.Range(0,icons.Length);
        int itemInfoIndex = Random.Range(0,itemsTxt.Count);

        newItem.name = itemsTxt[itemInfoIndex][0];
        newItem.description = itemsTxt[itemInfoIndex][1];
        if(icons.Length>0){
            newItem.icon=icons[iconIndex];
            Debug.Log(newItem.icon);
        }
        newItem.isDefaultItem=false;
        newItem.ArtefactType = Item.Type.Upgrade;
        newItem.size = Random.Range(0.5f, 2.0f);
        newItem.speed = Random.Range(1.0f, 5.0f);
        newItem.atk = Random.Range(10.0f, 50.0f);
        newItem.def = Random.Range(5.0f, 25.0f);
        newItem.hp = Random.Range(50.0f, 100.0f);
        //newItem.modifiers
        newItem.cooldown=10f;
        //newItem.prefab = <Random Prefab selection???>

        return newItem;
    }
    /* Initialise a numm/empty Item class
        Item newItem = ScriptableObject.CreateInstance<Item>();
        newItem.InitializeNewItem(newItem, null, null, null, false, 0f, 0f, 0f, 0f, 0f); */

}
