using UnityEngine;

using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    private float movementX; 
    private float movementY;

    private bool isGrounded;  

    private Rigidbody2D rb; 

    [SerializeField] private float speed = 5f; 
    [SerializeField] private float jumpStrength = 100f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        movementX = 0f; 
        movementY = 0f; 

        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movementX * speed, rb.linearVelocity.y);  

        if(movementY > 0 && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpStrength)); 
            isGrounded = false; 
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>(); 

        movementX = v.x; 
        movementY = v.y; 
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 
        }
    }
}
