using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target; 
    private Animator anim;  
    public GameObject floatingPoints;
    public GameObject bob; 
    private bool isAlive;

    public GameObject projectile;
    public float playerDefaultSpeed = 3.0f;
    public float timeBetweenProjectiles = 5.0f;

    private bool targetOnScreen = false;

    private List<GameObject> projectiles = new List<GameObject>();

    private float screenWidthInPoints;

    public int health = 100; 

    public playerMovement bobScript;  

    void Start()
    {
        isAlive = true; 
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
        anim = GetComponent<Animator>(); 
        InvokeRepeating("PrepareAttack",2.0f,timeBetweenProjectiles);
        bobScript = (playerMovement) bob.GetComponent(typeof(playerMovement)); 
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        if (health <= 0){
            Die(); 
        }
        if(isAlive==true){
        float playerPos = GameObject.Find("Player").transform.position.x;
        float leftWall = playerPos - screenWidthInPoints;

        //when enemy on screen
        if(target.transform.position.x - playerPos <= 9){ 
            targetOnScreen = true;
            target.transform.Translate(Vector3.right * playerDefaultSpeed * Time.deltaTime); //move enemy
        }
        else{
            targetOnScreen = false;
        }

        //!!!check for projectiles outside the screen to remove them

        List<GameObject> projectilesToRemove = new List<GameObject>();
        foreach (var projec in projectiles)
        {
            
            if (projec.gameObject == null){
                projectilesToRemove.Add(projec);
            }
            else{
                //3
                float projecX = projec.transform.position.x;

                //5
                if (projecX < leftWall) 
                {           
                    projectilesToRemove.Add(projec);
            }
            }
            
        }
        //6
        foreach (var projec in projectilesToRemove)
        {
            projectiles.Remove(projec);
            Destroy(projec);
        }
        }
    }

    void PrepareAttack()
    {
        if(targetOnScreen){
            target.GetComponent<Animator>().Play("enemy1_attack");
            StartCoroutine(LaunchAttack(2));
        }
    }

    IEnumerator LaunchAttack(float time)
    {
        yield return new WaitForSeconds(time);
        if(anim.GetBool("isDead")==false){
            
            GameObject newProjectile = Instantiate(projectile,target.transform.position,target.transform.rotation);
            projectiles.Add(newProjectile);
            target.GetComponent<Animator>().Play("enemy1_idle");  
        }
       
        
    }
    public void TakeDamage (int damage)
    {
        if(isAlive==true) {
            if (damage >= 100){
                GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity) as GameObject; 
                points.transform.GetChild(0).GetComponent<TextMesh>().text = "A VERY LOT";
                health -= damage; 
            } else {
               GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity) as GameObject; 
            points.transform.GetChild(0).GetComponent<TextMesh>().text = bobScript.ActualDamage();
            health -= damage; 
            }
                
        }
    }

    void Die(){
        if (gameObject!=null && isAlive==true){ 

            bobScript.IncreaseScoreAndDamage();
            anim.SetBool("isDead", true); 
            isAlive=false; 
        }
    }

    void DestroyObject(){
        Destroy(gameObject);
    }   
}
