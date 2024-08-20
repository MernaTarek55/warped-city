using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boyPlayerMovment : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public LayerMask ladderLayer;
    public Transform idleFirePoint;
    public Transform crouchFirePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Vector2 crouchColliderSize = new Vector2(1f, 0.5f);
    public Vector2 standingColliderSize = new Vector2(1f, 2f);

    private float moveInput;
    private bool isRunning;
    private bool isGrounded;
    private bool isClimbing;
    private bool isJumping;
    private bool isHurt;
    private bool isCrouching;

    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isGrounded = IsGrounded();
        isJumping = Input.GetKeyDown(KeyCode.Space) && isGrounded;
        isCrouching = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

        // Handle rotation and movement input
        if (!isCrouching && !isHurt)
        {
            if (moveInput > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (moveInput < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            animator.SetInteger("Speed", isRunning ? 2 : 1);
        }
        else if (isCrouching)
        {
            moveInput = 0; // Prevent movement while crouching
            animator.SetTrigger("isCrouch");

            // Handle crouch shooting
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Shoot(crouchFirePoint);
            }
        }

        if (isJumping && !isHurt && !isClimbing && !isCrouching)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            animator.SetTrigger("isJumping");
        }

        // Trigger the fall animation when falling back to the ground
        if (!isGrounded && rb.velocity.y < 0)
        {
            animator.SetTrigger("isFall");
        }

        if (Input.GetKeyDown(KeyCode.Z) && !isHurt && !isRunning && !isClimbing && !isCrouching)
        {
            Shoot(idleFirePoint);
        }
        else if (!isCrouching)
        {
            animator.SetBool("isShooting", false);
        }
    }

    void FixedUpdate()
    {
        if (!isClimbing && !isHurt)
        {
            float speed = isRunning ? runSpeed : walkSpeed;
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void OnDrawGizmos()
    {
        if (boxCollider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
        }
    }

    private void Shoot(Transform firePoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        float direction = transform.rotation.eulerAngles.y == 180 ? -1 : 1;
        bulletRb.velocity = new Vector2(bulletSpeed * direction, 0);
        animator.SetBool("isShooting", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Hurt());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Hurt());
        }
    }

    private IEnumerator Hurt()
    {
        isHurt = true;
        rb.velocity = new Vector2(-moveInput * runSpeed, rb.velocity.y);
        PlayerHealth.health--;
        if (PlayerHealth.health <= 0)
        {
            PlayerHealth.health = 0;
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }
        isHurt = false;
    }
}
