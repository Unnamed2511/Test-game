using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPos;
    private GameObject[] enemy;
    private void Awake()
    {
        enemy = Resources.LoadAll<GameObject>("Enemy");
    }
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    private void Repeat() 
    {
        StartCoroutine(SpawnEnemy());
    }
    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSecondsRealtime(10);
        Instantiate(enemy[Random.Range(0, enemy.Length)], spawnPos[Random.Range(0, spawnPos.Length)]);
        Repeat();
    }
}
