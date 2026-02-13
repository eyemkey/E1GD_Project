using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{

    private float movementX; 
    private float movementY;

    private bool isGrounded;
    private bool hasClickedDoubleJump;
    private bool hasDoubleJumped;  


    private Rigidbody2D rb; 
    private Animator animator;
    private SpriteRenderer spriteRenderer; 

    private int score; 
    private int dashFrameCount;
    private bool isDashing; 

    [SerializeField] private float speed = 5f; 
    [SerializeField] private float jumpStrength = 300f; 
    [SerializeField] private float dashStrength = 200f; 
    [SerializeField] private AudioSource source; 
    [SerializeField] private AudioClip jumpClip;

    [SerializeField] private GameManager gameManager;

    public UnityEvent playerDeath; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        score = 0;
        hasClickedDoubleJump = false; 
        hasDoubleJumped = false; 
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>(); 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movementX * speed, rb.linearVelocity.y);  
        if(!isDashing) speed = 5f;

        if(rb.linearVelocity.y < 0)
        {
            animator.SetBool("isJumping", false); 
            animator.SetBool("isFalling", true); 
        } else if(rb.linearVelocity.y > 0)
        {
            animator.SetBool("isJumping", true); 
            animator.SetBool("isFalling", false); 
        } else
        {
            animator.SetBool("isJumping", false); 
            animator.SetBool("isFalling", false); 
        }

        if (isDashing)
        {
            dashFrameCount++; 
            if(dashFrameCount == 5)
            {
                isDashing = false; 
                dashFrameCount = 0; 
            }
        }

        if(hasClickedDoubleJump && !hasDoubleJumped && rb.linearVelocity.y < 0)
        {
            Jump(); 
            hasDoubleJumped = true;
        }

        if(!Mathf.Approximately(movementX, 0f))
        {
            spriteRenderer.flipX = movementX < 0; 
            animator.SetBool("isRunning", true); 
        }else
        {   
            animator.SetBool("isRunning", false); 
        }


    }

    private void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>(); 
        movementX = v.x; 
        movementY = v.y; 
    }

    private void OnJump()
    {
        if(isGrounded)
        {
            Jump(); 
        } else if(!hasClickedDoubleJump)
        {
            hasClickedDoubleJump = true; 
        }
    }
    private void OnDash()
    {
        if (!isDashing)
        {
            speed = 20f; 
            isDashing = true; 
            dashFrameCount = 0; 
        }
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 
            hasClickedDoubleJump = false; 
            hasDoubleJumped = false; 

            // animator.SetBool("isJumping", false); 
            // animator.SetBool("isFalling", false); 
        }
    }

    private void OnCollisionExit2D (Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; 
        }
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false); 
            score++; 
            gameManager.UpdateScore(score); 
        }
        else if(collision.gameObject.CompareTag("Spike"))
        {
            playerDeath.Invoke(); 
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpStrength)); 
        source.PlayOneShot(jumpClip);
        // animator.SetBool("isJumping", true); 
    }
}


