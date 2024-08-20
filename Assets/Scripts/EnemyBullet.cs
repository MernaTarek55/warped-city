using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;          
    public float lifetime = 3f;       
    public int damage = 1;            
    public float destroyDelay = 0.1f;   

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;  
        Destroy(gameObject, lifetime);         
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            StartCoroutine(DestroyBulletAfterDelay());
        }
    }

    private IEnumerator DestroyBulletAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);

        Destroy(gameObject);
    }
}
