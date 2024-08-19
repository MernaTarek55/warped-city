using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;            // Speed of the bullet
    public float lifetime = 3f;         // Lifetime before the bullet is destroyed
    public int damage = 1;              // Damage the bullet deals to the player
    public float destroyDelay = 0.1f;   // Delay before destroying the bullet on collision

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;  // Move the bullet to the right (you can adjust this direction as needed)
        Destroy(gameObject, lifetime);          // Destroy the bullet after its lifetime ends
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            // Deal damage to the player
            //PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            //if (playerHealth != null)
            //{
             //   playerHealth.TakeDamage(damage);
           // }

            // Destroy the bullet after hitting the player
            StartCoroutine(DestroyBulletAfterDelay());
        }
    }

    private IEnumerator DestroyBulletAfterDelay()
    {
        // Wait for the specified destroy delay (if you want an immediate destroy, you can set the delay to 0)
        yield return new WaitForSeconds(destroyDelay);

        // Destroy the bullet
        Destroy(gameObject);
    }
}
