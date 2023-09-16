using UnityEngine;

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


    string[] names = { "Sword", "Axe", "Mace", "Bow", "Staff","Onzil", "Kulbeda", "Mambele", "Pinga","Trombash"};
    string[] descriptions = { "A powerful weapon", "A deadly tool", "A magical artifact", "A legendary item", "Ancient artifact" };
    public Sprite[] icons = {};
    public Item InitializeNewRandomItem(Item newItem){
        //Item newItem = new Item();
        int nameIndex = Random.Range(0, names.Length);
        int descriptionIndex = Random.Range(0, descriptions.Length);
        int iconIndex = Random.Range(0,icons.Length);

        newItem.name = names[nameIndex];
        newItem.description = descriptions[descriptionIndex];
        if(icons.Length>0){
            newItem.icon=icons[iconIndex];
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
