using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    public float speed;
    public float dashForce;
    public float jumpForce;
    private float moveInput;

    public float dashTime;
    private float dashTimeCounter;
    private bool isDashing = false;


    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    private float playerGravity;

    void Start()
    {
        jumpTimeCounter = jumpTime;
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        playerGravity = rb.gravityScale;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Walk();
        Jump();
        Dash();
    }

    void Walk()
    {
        if (!isDashing)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            animator.speed = moveInput;
        }

        if (!facingRight && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight && moveInput < 0)
        {
            Flip();
        }
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0 && !isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            extraJumps--;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.fixedDeltaTime;
            }
            else
            {
                isJumping = false;
                rb.velocity = new Vector2(rb.velocity.x, 15f);
                jumpTimeCounter = jumpTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.5f);
                isJumping = false;
            }
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (moveInput != 0)
            {
                Vector2 newVel = Vector2.right * dashForce * moveInput;
                isDashing = true;
                isJumping = false;
                dashTimeCounter = dashTime;
                rb.velocity = newVel;
                rb.gravityScale = 0;
            }
        }

        if (isDashing)
        {
            if (dashTimeCounter <= 0)
            {
                rb.gravityScale = playerGravity;
                isDashing = false;
            }
            else
            {
                dashTimeCounter -= Time.fixedDeltaTime;
            }
        }
    }

    void Flip()
    {
        if (facingRight)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}