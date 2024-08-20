using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    Animator animator;
    [SerializeField] Transform firePos;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float distance;
    public GameObject player;
    public float shootDelay = 0.001f; 
    private bool canShoot = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        distance = Vector2.Distance(gameObject.transform.position, player.transform.position);
        if (distance < 7 && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        animator.Play("Egg Turret Shoot Animation");
        canShoot = false;

        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        float direction = transform.rotation.eulerAngles.y == 180 ? -1 : 1;
        bulletRb.velocity = new Vector2(bulletSpeed * direction, 0);

        

        yield return new WaitForSeconds(shootDelay);

        canShoot = true;
    }
}
