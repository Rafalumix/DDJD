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
    public AudioSource hitSounds;
    public AudioSource healthSound;
    public AudioSource deathSound;

    private static float damage; 
    private HardcoreMode hm; 
    public UnityEngine.UI.Text scoreText1; 
    public UnityEngine.UI.Text scoreText2; 
    private static int enemiesKilled = 0; 



    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        hm = GetComponent<HardcoreMode>(); 

        lastPosition = 0;
        damage = 25; 
        if (hm.IsHardcore() == true){
            damage = 9999; 
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        float distance = transform.position.x - lastPosition;
        totalDistance += distance;
        lastPosition = transform.position.x;
        score += distance;
        ScoreLabel.text = "Score: " + ((uint)score).ToString();
        coinsCollectedLabel.text = "ECTS: " + coins.ToString();
        UpdateGroundedStatus();

    }

    void FixedUpdate()
    {
        UpdateGroundedStatus();
        if (isDead == false)
        {
            if (!PauseButton.isGamePaused && Input.GetButton("Jump"))
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
            scoreText1.text = "METERS: " + ((int)totalDistance).ToString() + "\n\n\n\n\n" + "COINS: " + coins.ToString(); 
            scoreText2.text = "KILLED ENEMIES: " + ((int)enemiesKilled).ToString() + "\n\n\n\n\n" + "SCORE: " + ((int)score).ToString(); 

        }

    }

    void CollectCoin(Collider2D coinCollider)
    {
        GameObject coin = coinCollider.gameObject; 
        coin_script script = (coin_script) coin.GetComponent(typeof(coin_script)); 
        if(script.IsTaken()==false){
            coins++;
            score += 10;
            script.CoinTaken();
            //Destroy(coinCollider.gameObject,0.5f);
    }
        
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
                hitSounds.Play();
                if(hm.IsHardcore() == true){
                    TakeDamage(9999); 
                }
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
        if(health<=0){
            deathSound.Play();
        }
        AnimatorSon.SetBool("wasDamaged", true);
        healthBar.UpdateHealthBar();
        CameraShake.Shake(0.3f, 0.05f);
        StartCoroutine(RemoveHitStatus());
    }

    public void Heal()
    {
        healthSound.Play();
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
            if (laserCollider.gameObject.CompareTag("Trap"))
            {
                AudioSource hitEffect = laserCollider.gameObject.GetComponent<AudioSource>();
                hitEffect.Play();
            }
            else if (laserCollider.gameObject.CompareTag("Projectile"))
            {
                Destroy(laserCollider.gameObject);
                hitSounds.Play();
            }
            if(hm.IsHardcore() == true){
                    TakeDamage(9999); 
                }
                TakeDamage(30);
            
            // isDead = true;
            // AnimatorSon.SetBool("isDead", true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void IncreaseScoreAndDamage(){

        score += 100; 
        damage += 5; 
        enemiesKilled ++; 
        //Debug.Log(damage);
    }

    public string ActualDamage(){
        if (damage<250){
            return damage.ToString(); 
        } else if (damage<500){
            return "A LOT"; 
        } 
        return "A VERY LOT"; 
        
    }
}

