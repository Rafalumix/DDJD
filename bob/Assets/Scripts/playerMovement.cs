using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public float flight = 50.0f;
    public float forwardMovementSpeed = 3.0f;

    private Rigidbody2D playerRigidbody;



    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Jump"))
        {
            playerRigidbody.AddForce(new Vector2(0, flight));
        }

        Vector2 newVelocity = playerRigidbody.velocity;
        newVelocity.x = forwardMovementSpeed;
        playerRigidbody.velocity = newVelocity;
    }
    
}
