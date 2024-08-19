using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enimyShoot : MonoBehaviour
{
    Animator animator;
    [SerializeField] Transform fairpos;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player") {
            Shoot();
        }
        else
        {
            animator.Play("Egg Turret Idle Animation");
        }
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, fairpos.position, fairpos.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        float direction = transform.rotation.eulerAngles.y == 180 ? -1 : 1;
        bulletRb.velocity = new Vector2(bulletSpeed * direction, 0);
        animator.Play("Egg Turret Shoot Animation");
    }
}
