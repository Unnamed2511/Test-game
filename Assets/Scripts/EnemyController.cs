using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private PlayerHealth playerHealth;
    private SpriteRenderer spriteRenderer;
    private NavMeshAgent agent;
    public Transform target { get; private set; }

    private bool enemyIsStopped;
    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();  
        spriteRenderer = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void FixedUpdate()
    {
        SetAgentPosition();
        Flip();
    }
    private void SetAgentPosition() 
    {
        enemyIsStopped = enemyHealth.enemyIsDead || playerHealth.playerIsDead ? true : false;

        if (!enemyIsStopped)
        {
            agent.SetDestination(target.position);
        }

        agent.isStopped = enemyIsStopped;
    }
    private void Flip() 
    {
        spriteRenderer.flipX = !enemyIsStopped ? (target.position.x > transform.position.x ? false : true) : spriteRenderer.flipX;
    }
}
