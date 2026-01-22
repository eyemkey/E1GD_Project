using UnityEngine;

using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    private float movementX; 
    private float movementY; 

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

        if(movementY > 0)
        {
            rb.AddForce(new Vector2(0, jumpStrength)); 
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>(); 

        movementX = v.x; 
        movementY = v.y; 
    }
}
