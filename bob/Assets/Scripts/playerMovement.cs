using UnityEngine;


public class playerMovement : MonoBehaviour
{

    public float flight = 50.0f;
    public float forwardMovementSpeed = 3.0f;
    private uint coins = 0;
    private float score = 0;
    private float lastPosition;
    private float totalDistance;
    public UnityEngine.UI.Text coinsCollectedLabel;
    public UnityEngine.UI.Text ScoreLabel;



    private Rigidbody2D playerRigidbody;



    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        lastPosition = 0;

    }

    // Update is called once per frame
    void Update()
    {
        float distance = transform.position.x - lastPosition;
        totalDistance += distance;
        lastPosition = transform.position.x;
        score += distance;
        ScoreLabel.text = "Score: " + ((uint) score).ToString();
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

    void CollectCoin(Collider2D coinCollider)
    {
        coins++;
        coinsCollectedLabel.text = coins.ToString() + " ECTS"; 
        score += 10;
        Destroy(coinCollider.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Collectible"))
        {
            CollectCoin(collider);
        }
        //Collision with obstacles!
        // else {
        //     coins = 0;
        // }
    }
}
