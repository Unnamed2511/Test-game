using DG.Tweening;
using UnityEngine;

public class ItemInteractions : MonoBehaviour
{
    public Item item { get; private set; }
    public bool isEnable { get; private set; }

    private GameObject currentItemObject;
    private SpriteRenderer spriteRenderer;
    private Collider2D weaponCollision;

    private string weaponName;

    private void OnEnable()
    {
        GlobalEventManager.OnDestroyWeaponInHand += OnDestroyWeaponInHand;
    }
    private void OnDisable()
    {
        GlobalEventManager.OnDestroyWeaponInHand -= OnDestroyWeaponInHand;
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void SpawnWeaponInHand() => currentItemObject = Instantiate(item.itemGameObject, this.transform);
    private void OnDestroyWeaponInHand() => Destroy(currentItemObject);
    private void LoadItem(string name) => item = (Item)Resources.Load(name);
    private void SpawnWeaponOnGround()
    {
        if (item is not null)
        {
            GameObject newItem = Instantiate(item.itemWorldGameObject, transform.position, Quaternion.identity);
            Vector3 lookDirectional = spriteRenderer.flipX == true ? Vector3.left : Vector3.right;
            newItem.transform.DOMove(transform.position + lookDirectional * 1.5f, 0.5f);
        }
    }
    public void TakeWeapon()
    {
        if (weaponCollision is not null)
        {
            if (item == null)
            {
                LoadItem(weaponName);
                SpawnWeaponInHand();
            }
            else
            {
                OnDestroyWeaponInHand();
                SpawnWeaponOnGround();
                LoadItem(weaponName);
                SpawnWeaponInHand();
            }
            Destroy(weaponCollision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ItemData _itemData)) 
        {
            switch (_itemData.itemID)
            {
                case 1:
                    GlobalEventManager.AddGold(1, collision.gameObject);
                    break;
                case 2:
                    GlobalEventManager.AddGold(5, collision.gameObject);
                    break;
                case 3:
                    GlobalEventManager.AddGold(10, collision.gameObject);
                    break;
                case 4:
                    GlobalEventManager.HealPlayer(2, collision.gameObject);
                    break;
            }
        }
        else if (collision.gameObject.TryGetComponent(out WeaponData _weaponData)) 
        {
            isEnable = true;
            weaponName = _weaponData.weaponName;
            weaponCollision = collision;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out WeaponData _weaponData))
        {
            isEnable = false;
            weaponName = null;
            weaponCollision = null;
        }
    }
}
