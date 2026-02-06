using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    private Rigidbody2D rb; 

    [SerializeField]
    private float bounceStrength = 100f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        rb.AddForce(new Vector2(0, bounceStrength));         
    }
}
