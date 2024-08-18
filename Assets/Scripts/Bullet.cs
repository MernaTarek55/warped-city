using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;         // Speed of the bullet
    public float lifetime = 2f;       // Lifetime before the bullet is destroyed
    public int damage = 1;            // Damage the bullet deals to enemies

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;  
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits an enemy
        if (collision.CompareTag("Enemy"))
        {
            //Destroy(collision.GameObject);
            

            // Destroy the bullet after it hits an enemy
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground") || collision.CompareTag("Wall"))
        {
            // Destroy the bullet if it hits a solid object like the ground or a wall
            Destroy(gameObject);
        }
    }
}
