using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private EnemyController enemyController;

    private readonly int damageEnemy = 1;

    private bool allowAttack = true;
    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyController = GetComponent<EnemyController>();
    }
    private void FixedUpdate()
    {
        Attack();
    }
    private void Attack()
    {
        float distance = Vector3.Distance(transform.position, enemyController.target.position);
        if (distance <= 1.5f && !enemyHealth.enemyIsDead)
        {
            if (allowAttack == true)
            {
                StartCoroutine(AttackSpeed());
            }
        }
    }
    private IEnumerator AttackSpeed()
    {
        allowAttack = false;
        GlobalEventManager.TakeDamagePlayer(damageEnemy);
        yield return new WaitForSeconds(1);
        allowAttack = true;
    }
}
