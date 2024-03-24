using System;
using UnityEngine;

public class GlobalEventManager : MonoBehaviour
{
    public static event Action<int> OnPlayerTakeDamage;

    public static event Action<int, GameObject> OnPlayerAddHealth;

    public static event Action<int, EnemyHealth> OnEnemyTakeDamage;

    public static event Action<int, GameObject> OnPlayerAddGold;

    public static event Action<string, GameObject> OnPickUpItem;

    public static event Action OnAddKillCount;

    public static event Action OnDestroyWeaponInHand;

    public static void TakeDamagePlayer(int damageCount) 
    {
        OnPlayerTakeDamage?.Invoke(damageCount);
    }
    public static void HealPlayer(int healCount, GameObject gameObject)
    {
        OnPlayerAddHealth?.Invoke(healCount, gameObject);
    }
    public static void TakeDamageEnemy(int value, EnemyHealth enemyHealth)
    {
        OnEnemyTakeDamage?.Invoke(value, enemyHealth);
    }
    public static void AddGold(int value, GameObject gameObject)
    {
        OnPlayerAddGold?.Invoke(value, gameObject);
    }
    public static void PickUp(string name, GameObject item)
    {
        OnPickUpItem?.Invoke(name, item);
    }
    public static void AddKill()
    {
        OnAddKillCount?.Invoke();
    }
    public static void DestroyWeapon()
    {
        OnDestroyWeaponInHand?.Invoke();
    }
}
