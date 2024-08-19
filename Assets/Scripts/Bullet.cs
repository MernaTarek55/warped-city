using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public int damage = 1;
    public float destroyDelay = 0.5f;
    private Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();

            if (enemyAnimator != null)
            {
                enemyAnimator.Play("die");
                StartCoroutine(DestroyEnemyAfterDelay(collision.gameObject));
            }
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private IEnumerator DestroyEnemyAfterDelay(GameObject enemy)
    {
        yield return  new WaitForSeconds(destroyDelay);
        if (enemy != null)
        {
            Destroy(enemy);
            Destroy(gameObject);
        }
    }
}
