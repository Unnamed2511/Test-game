using UnityEngine;

public class Bullet : MonoBehaviour
{
    private ItemInteractions interactions;
    private void Start()
    {
        interactions = GameObject.FindWithTag("Player").GetComponent<ItemInteractions>();
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * interactions.item.bulletSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null) 
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                GlobalEventManager.TakeDamageEnemy(interactions.item.damage, collision.collider.gameObject.GetComponent<EnemyHealth>());
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
