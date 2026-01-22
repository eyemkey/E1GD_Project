using UnityEngine;

using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    private float movementX; 
    private float movementY;

    private bool isGrounded;
    private bool hasClickedDoubleJump;
    private bool hasDoubleJumped;  


    private Rigidbody2D rb; 

    private int score; 

    [SerializeField] private float speed = 5f; 
    [SerializeField] private float jumpStrength = 300f; 
    [SerializeField] private float dashStrength = 200f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        score = 0;
        hasClickedDoubleJump = false; 
        hasDoubleJumped = false; 
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movementX * speed, rb.linearVelocity.y);  
        speed = 5f;

        if(hasClickedDoubleJump && !hasDoubleJumped && rb.linearVelocity.y < 0)
        {
            Jump(); 
            hasDoubleJumped = true;
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
        speed = 250f; 
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 
            hasClickedDoubleJump = false; 
            hasDoubleJumped = false; 
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
            Debug.Log($"Score: {score}");
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpStrength)); 
    }
}
