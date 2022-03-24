using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target; 

    public GameObject projectile;
    public float playerDefaultSpeed = 3.0f;
    public float timeBetweenProjectiles = 5.0f;

    private bool targetOnScreen = false;

    private List<GameObject> projectiles = new List<GameObject>();

    private float screenWidthInPoints;

    void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
        InvokeRepeating("PrepareAttack",2.0f,timeBetweenProjectiles);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
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

        GameObject newProjectile = Instantiate(projectile,target.transform.position,target.transform.rotation);

        projectiles.Add(newProjectile);

        target.GetComponent<Animator>().Play("enemy1_idle");
        
    }
}
