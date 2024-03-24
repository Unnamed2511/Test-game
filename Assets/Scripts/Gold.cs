using UnityEngine;

public class Gold : MonoBehaviour
{
    public int gold { get; private set; }
    private void OnEnable()
    {
        GlobalEventManager.OnPlayerAddGold += AddGold;
    }
    private void OnDisable()
    {
        GlobalEventManager.OnPlayerAddGold -= AddGold;
    }
    private void AddGold(int value, GameObject gameObject) 
    {
        gold += value;
        Destroy(gameObject);
    }
}
