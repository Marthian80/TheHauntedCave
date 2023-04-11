using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    Vector2 moveInput;
    Rigidbody2D rb2d;
    CapsuleCollider2D playerCollider;
    BoxCollider2D feetCollider;
    Animator animator;
    float startingGravity;
    bool isAlive = true;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();        
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        startingGravity = rb2d.gravityScale;
    }
        
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void ClimbLadder()
    {        
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb2d.gravityScale = startingGravity;
            return;
        }

        rb2d.gravityScale = 0f;

        Vector2 climbVelocity = new Vector2(rb2d.velocity.x, moveInput.y * climbSpeed);
        rb2d.velocity = climbVelocity;
        bool playerIsClimbing = Mathf.Abs(rb2d.velocity.y) > Mathf.Epsilon;

        animator.SetBool("IsClimbing", playerIsClimbing);        
    }

    void FlipSprite()
    {
        bool playerIsMoving = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;

        if (playerIsMoving )
        {
            transform.localScale = new Vector2(Mathf.Sign(rb2d.velocity.x), 1f);
        }        
    }

    void Run()
    {        
        Vector2 playerVelocity = new Vector2(moveInput.x * speed, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;
        bool playerIsMoving = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;

        animator.SetBool("IsRunning", playerIsMoving);        
    }

    void OnMove(InputValue value)
    {
        if (isAlive)
        {
            moveInput = value.Get<Vector2>();
        }            
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && isAlive)
        {
            rb2d.velocity += new Vector2(0f, jumpHeight);
        }
    }

    void OnFire(InputValue value)
    {
        if (value.isPressed && isAlive)
        {
            Instantiate(bullet, gun.position, transform.rotation);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Enemy")
        {
            Die();
        }        
    }

    void Die()
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            animator.SetTrigger("Dying");
            rb2d.velocity += new Vector2(0f, jumpHeight);
            playerCollider.enabled = false;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }        
    }

    
}
