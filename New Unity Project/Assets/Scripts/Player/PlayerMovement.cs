using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    public float speed;
    public float dashForce;
    public float jumpForce;
    private float moveInput;
    public int extraJumpsValue;
    private int extraJumps;

    public float maxFallSpeed;

    private bool facingRight = true;
    private float playerGravity;
    private bool AirBorn = false;

    [Header("Timers")]
    public float dashTime;
    private float dashTimeCounter;
    private bool isDashing = false;
    public float dashCD;
    private float dashCDCounter;
    private bool canDash = true;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius;
    private bool isGrounded;
    public LayerMask whatIsGround;

    [Header("On Enter")]
    public GameObject startLandParticles;
    public float shakeIntensity;
    public float shakeTime;
    private bool hasLanded = false;

    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
        dashTimeCounter = dashTime;
        dashCDCounter = dashCD;
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        playerGravity = rb.gravityScale;
    }

    void FixedUpdate()
    {
        if (hasLanded)
        {
            Walk();
        }
        
        rb.velocity = new Vector2(rb.velocity.x, Vector2.ClampMagnitude(rb.velocity, maxFallSpeed).y);
    }

    void Update()
    {
        if (!hasLanded)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
            if (isGrounded)
            {
                hasLanded = true;
                Instantiate(startLandParticles, new Vector3(transform.position.x, transform.position.y - 0.7f, transform.position.z - 2), Quaternion.Euler(-90, 0, 0));
                CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTime);
            }
        }
        else
        {
            Jump();
            Dash();
        }
    }

    void Walk()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (!isDashing)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            if (moveInput == 0)
            {
                animator.SetFloat("Speed", 0);
            }
            else
            {
                animator.SetFloat("Speed", 1);
            }
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
        if (!isGrounded && !isJumping)
        {
            animator.SetBool("Down", true);
            AirBorn = true;
        }
        else if (isJumping)
        {
            animator.SetBool("Up", true);
        }
        else if (isGrounded && AirBorn)
        {
            animator.SetBool("Land", true);
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            extraJumps = extraJumpsValue;
            AirBorn = false;
        }
        else if (isGrounded)
        {
            animator.SetBool("AttackDown", false);
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetBool("Jump", true);
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            AirBorn = true;
            jumpTimeCounter = jumpTime;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0 && !isGrounded)
        {
            animator.SetBool("Down", false);
            animator.SetBool("Up", false);
            animator.SetBool("AirJump", true);
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            AirBorn = true;
            jumpTimeCounter = jumpTime;
            extraJumps--;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                animator.SetBool("Down", true);
                rb.velocity = new Vector2(rb.velocity.x, 15f);
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isJumping)
            {
                animator.SetBool("Down", true);
                rb.velocity = new Vector2(rb.velocity.x, 0.5f);
                isJumping = false;
            }
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (canDash)
            {
                if (moveInput != 0)
                {
                    animator.SetBool("Dash", true);
                    Vector2 newVel = Vector2.right * dashForce * moveInput;
                    isDashing = true;
                    canDash = false;
                    isJumping = false;
                    rb.velocity = newVel;
                    rb.gravityScale = 0;
                }
            }
        }

        if (isDashing)
        {
            if (dashTimeCounter <= 0)
            {
                animator.SetBool("Dash", false);
                rb.gravityScale = playerGravity;
                dashTimeCounter = dashTime;
                isDashing = false;
            }
            else
            {
                dashTimeCounter -= Time.deltaTime;
            }
        }
        else if (!canDash)
        {
            if(dashCDCounter <= 0)
            {
                dashCDCounter = dashCD;
                canDash = true;
            }
            else
            {
                dashCDCounter -= Time.deltaTime;
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}