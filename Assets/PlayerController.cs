using UnityEngine;

using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    private float movementX; 
    private float movementY; 

    [SerializeField]
    private float speed = 5f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        movementX = 0f; 
        movementY = 0f; 
    }

    // Update is called once per frame
    void Update()
    {
        float dx = movementX * speed * Time.deltaTime; 
        float dy = movementY * speed * Time.deltaTime; 

        Vector2 dv = new Vector2(dx, dy); 

        transform.Translate(dv); 
    }

    private void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>(); 

        movementX = v.x; 
        movementY = v.y; 
    }
}
