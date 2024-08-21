using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    Animator animator;
    [SerializeField] Transform firePos;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float distance;
    public float shootDelay = 1.5f;
    private bool canShoot = true;

    Transform player; // Retain this Transform for player reference
    public Transform player1;
    public Transform player2;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Check which player is active at the start and assign it
        if (player1.gameObject.activeSelf)
        {
            player = player1;
        }
        else if (player2.gameObject.activeSelf)
        {
            player = player2;
        }
        else
        {
            Debug.LogError("No active player found!");
            player = null; // Assign null to avoid further errors
        }
    }

    private void Update()
    {
        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.position);
            if (distance < 7f && canShoot)
            {
                canShoot = false;
                StartCoroutine(Shoot());
            }
            else if (distance >= 7f)
            {
                canShoot = true;
            }
        }
    }

    private IEnumerator Shoot()
    {
        // Check if the player is still within range
        while (distance < 7f)
        {
            // Play shoot animation (if any)
            if (animator != null)
            {
                animator.SetTrigger("Shoot");
            }

            GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            float direction = transform.rotation.eulerAngles.y == 180 ? -1 : 1;
            bulletRb.velocity = new Vector2(bulletSpeed * direction, 0);

            yield return new WaitForSeconds(shootDelay);
        }

        canShoot = true; // Allow shooting again once out of range
    }
}
