using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public LayerMask ladderLayer;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    private float moveInput;
    private bool isRunning;
    private bool isGrounded;
    private bool isClimbing;
    private bool isJumping;
    private bool isHurt;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isGrounded = IsGrounded();
        isJumping = Input.GetKeyDown(KeyCode.Space) && isGrounded;

        if (moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;
        }

        // Handle movement animations
        if (moveInput == 0 && !isClimbing && !isHurt)
        {
            animator.SetBool("Walk", false);
        }
        else if (!isHurt)
        {
            if (isRunning)
            {
                animator.Play("Run");
            }
            else
            {
                animator.SetBool("Walk", true);
            }
        }

        // Handle jump animation
        if (isJumping && !isHurt)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }

        // Handle ladder climbing
        if (Input.GetAxisRaw("Vertical") != 0 && IsTouchingLadder() && !isHurt)
        {
            isClimbing = true;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * walkSpeed);
            animator.SetBool("Climb", true);
        }
        else if (!IsTouchingLadder() || moveInput != 0)
        {
            isClimbing = false;
            rb.gravityScale = 1f;
            animator.SetBool("Climb", false);
        }

        // Handle shooting
        if (Input.GetKeyDown(KeyCode.Z) && !isHurt)
        {
            Shoot();
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

    private bool IsTouchingLadder()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.up, 0.1f, ladderLayer);
        return raycastHit.collider != null;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = new Vector2(bulletSpeed * (spriteRenderer.flipX ? -1 : 1), 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Hurt());
        }
    }

    private IEnumerator Hurt()
    {
        isHurt = true;
        animator.SetTrigger("Hurt");
        rb.velocity = new Vector2(-moveInput * runSpeed, rb.velocity.y); 

        yield return new WaitForSeconds(1f); 
        isHurt = false;
    }
}
