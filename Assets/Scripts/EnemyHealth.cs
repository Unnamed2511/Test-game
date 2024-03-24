using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Health")]
    [SerializeField] private int currentEnemyHealth = 10;

    private BoxCollider2D enemyCollider;
    private Animator enemyAnimator;
    private AudioSource enemyAudioSource;
    public bool enemyIsDead {  get; private set; }
    private void OnEnable()
    {
        GlobalEventManager.OnEnemyTakeDamage += TakeDamage;
    }
    private void OnDisable()
    {
        GlobalEventManager.OnEnemyTakeDamage -= TakeDamage;
    }
    private void Start()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        enemyCollider = GetComponent<BoxCollider2D>();
        enemyAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        Death();
    }
    private void TakeDamage(int damageCount, EnemyHealth enemyHealth)
    {
        if (enemyHealth.currentEnemyHealth > 0) 
        {
            if (enemyHealth.currentEnemyHealth > damageCount) 
            {
                enemyHealth.currentEnemyHealth -= damageCount;
                enemyHealth.enemyAnimator.SetTrigger("Hit");
            }
            else 
            {
                enemyHealth.currentEnemyHealth = 0;
            }
        }
    }
    private void Death() 
    {
        if (currentEnemyHealth <= 0 && !enemyIsDead) 
        {
            enemyAudioSource.PlayOneShot(AudioManager.killEnemySound);
            GlobalEventManager.AddKill();
            enemyIsDead = true;
            enemyCollider.enabled = false;
            enemyAnimator.SetBool("Dead", true);
        }
    }
}
