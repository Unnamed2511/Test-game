using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items")]
public class Item : ScriptableObject
{
    [Header("Main")]
    public string itemName;
    public GameObject itemGameObject;
    public GameObject itemWorldGameObject;
    [Space]
    public GameObject bullet;
    public enum ItemType { Weapon, Melle }
    public ItemType type;
    public float fireRate;
    public float bulletSpeed;
    public int damage;
}
