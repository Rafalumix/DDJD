using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
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
        ScoreLabel.text = "Score: " + ((uint)score).ToString();
        coinsCollectedLabel.text = coins.ToString() + " ECTS";
        UpdateGroundedStatus();

    }

    void FixedUpdate()
    {
        UpdateGroundedStatus();
        if (isDead == false)
        {
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
        else if (collider.gameObject.CompareTag("Trap") || collider.gameObject.CompareTag("Projectile"))
        {
            HitByObstacle(collider);
        }
    }
    private void OnCollisionEnter2D(Collision2D collider) 
    {
        if (collider.gameObject.CompareTag("Obstacle"))
        {
            if (AnimatorSon.GetBool("wasDamaged") == false)
            {
                AudioSource hitEffect = collider.gameObject.GetComponent<AudioSource>();
                hitEffect.Play();
                TakeDamage(5);
                
            }
            
            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(10*Vector2.up);
        }
    }

    void UpdateGroundedStatus()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.15f, groundCheckLayerMask);
        AnimatorSon.SetBool("isGrounded", isGrounded);
    }

    public void TakeDamage(float damage)
    {

        health -= damage;
        AnimatorSon.SetBool("wasDamaged", true);
        healthBar.UpdateHealthBar();
        CameraShake.Shake(0.3f, 0.05f);
        StartCoroutine(RemoveHitStatus());
    }

    public void Heal()
    {

        health = Mathf.Min(100, health + 25);
        healthBar.UpdateHealthBar();
    }

    IEnumerator RemoveHitStatus()
    {
        yield return new WaitForSeconds(1);
        AnimatorSon.SetBool("wasDamaged", false);
    }

    void HitByObstacle(Collider2D laserCollider)
    {
        if (AnimatorSon.GetBool("wasDamaged") == false)
        {
            //We will use this one when Bob die reaching 0hp, now just for testing
            AudioSource hitEffect = laserCollider.gameObject.GetComponent<AudioSource>();
            hitEffect.Play();
            TakeDamage(25);
            if (laserCollider.gameObject.CompareTag("Projectile"))
            {
                Destroy(laserCollider.gameObject);
            }
            
            // isDead = true;
            // AnimatorSon.SetBool("isDead", true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("mainScene");
    }
}

