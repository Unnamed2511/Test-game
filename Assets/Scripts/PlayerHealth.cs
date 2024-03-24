using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagble, IHealing
{
    [Header("Maximum Health Value")]
    public readonly int maxPlayerHealth = 10;
    public int currentPlayerHealth { get; private set; }
    public bool playerIsDead { get; private set; }

    private AudioSource playerAudioSource;
    private Animator animator;
    private void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        currentPlayerHealth = maxPlayerHealth;
    }
    private void OnEnable()
    {
        GlobalEventManager.OnPlayerTakeDamage += TakeDamage;
        GlobalEventManager.OnPlayerAddHealth += Heal;
    }
    private void OnDisable()
    {
        GlobalEventManager.OnPlayerTakeDamage -= TakeDamage;
        GlobalEventManager.OnPlayerAddHealth -= Heal;
    }
    private void Update()
    {
        Death();
    }
    public void Heal(int healCount, GameObject gameObject)
    {
        if (currentPlayerHealth > 0 && currentPlayerHealth != maxPlayerHealth)
        {
            if (maxPlayerHealth - currentPlayerHealth > healCount)
            {
                currentPlayerHealth += healCount;
                Destroy(gameObject);
            }
            else
            {
                currentPlayerHealth = maxPlayerHealth;
                Destroy(gameObject);
            }
        }
    }
    public void TakeDamage(int damageCount)
    {
        if (currentPlayerHealth > 0)
        {
            if (damageCount < currentPlayerHealth)
            {
                currentPlayerHealth -= damageCount;
            }
            else
            {
                currentPlayerHealth = 0;
            }
        }
    }
    private void Death()
    {
        if (currentPlayerHealth <= 0 && !playerIsDead)
        {
            playerAudioSource.PlayOneShot(AudioManager.deadSound);
            playerIsDead = true;
            animator.SetBool("Death", true);
            GlobalEventManager.DestroyWeapon();
        }
    }
}
