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
        SpecialAbility
    }
    public Type ArtefactType;
    public int size,speed,atk,def,hp;
    public float cooldown;
    private float currentCooldown;
    public string[] modifiers;
    public GameObject prefab;
    public bool isOnCooldown=false;

}
