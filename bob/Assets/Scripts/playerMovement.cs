using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{

    public float health, maxHealth;
    public HealthBar healthBar;

    public Button restartButton;

    public float flight = 50.0f;
    public float forwardMovementSpeed = 3.0f;
    private uint coins = 0;
    private float score = 0;
    private float lastPosition;
    private float totalDistance;
    public UnityEngine.UI.Text coinsCollectedLabel;
    public UnityEngine.UI.Text ScoreLabel;
    public Animator AnimatorSon; //Put there the son for getting the animations


    public Transform groundCheckTransform;
    private bool isGrounded;
    public LayerMask groundCheckLayerMask;
    public bool isDead = false; 

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
        coinsCollectedLabel.text = coins.ToString() + " ECTS"; 
        UpdateGroundedStatus();

    }

    void FixedUpdate()
    {
        UpdateGroundedStatus();
        if (isDead == false){
        if (Input.GetButton("Jump"))
        {
            playerRigidbody.AddForce(new Vector2(0, flight));
        }
        
            Vector2 newVelocity = playerRigidbody.velocity;
            newVelocity.x = forwardMovementSpeed;
            playerRigidbody.velocity = newVelocity;
        }
        if (isDead && isGrounded)
        {
            restartButton.gameObject.SetActive(true);
        }
        
    }

    void CollectCoin(Collider2D coinCollider)
    {
        coins++;
        score += 10;
        Destroy(coinCollider.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Collectible"))
        {
            CollectCoin(collider);
        }
        // Collision with obstacles!
        else if (collider.gameObject.CompareTag("Obstacle"))
        {
            HitByObstacle(collider); 
        }
        else {
            // coins = 0;
        }
        //Collision with obstacles!
        // else {
        //     coins = 0;
        // }
    }

    void UpdateGroundedStatus()
{
    isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.15f, groundCheckLayerMask);
    AnimatorSon.SetBool("isGrounded", isGrounded);
}

    public void TakeDamage(){
    
    health -= 25 ;            
    healthBar.UpdateHealthBar();
}

    void HitByObstacle(Collider2D laserCollider)
    {
        //We will use this one when Bob die reaching 0hp, now just for testing
        TakeDamage(); 
        //isDead = true;
        //AnimatorSon.SetBool("isDead", true);
    }

    public void RestartGame()
{
    SceneManager.LoadScene("mainScene");
}}

