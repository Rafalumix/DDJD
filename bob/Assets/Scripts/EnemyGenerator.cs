using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] availableEnemies;
    private List<GameObject> enemies = new List<GameObject>();

    public int maxEnemies = 3;

    public float enemyMinDistance = 5.0f;
    public float enemyMaxDistance = 10.0f;

    public float enemyMinY = -4f;
    public float enemyMaxY = 4f;

    public float minYBetweenEnemies = 1.0f;
    private float screenWidthInPoints;

    void AddEnemy(float lastEnemyX)
    {
        //1
        int randomIndex = Random.Range(0, availableEnemies.Length);
        //2
        GameObject obj = (GameObject)Instantiate(availableEnemies[randomIndex]);
        //3
        float enemyPositionX = lastEnemyX + Random.Range(enemyMinDistance, enemyMaxDistance);

        bool tryAgain = true;
        float randomY = 0;

        while(tryAgain){
            tryAgain = false;
            randomY = Random.Range(enemyMinY, enemyMaxY);
            foreach(var enemy in enemies)
            {
                if( Mathf.Abs(enemy.transform.position.y - randomY)<minYBetweenEnemies)
                {
                    tryAgain = true;
                    break;
                }
            }
        }
        
        obj.transform.position = new Vector3(enemyPositionX,randomY,0); 

        //5
        enemies.Add(obj);            
    }

    void GenerateEnemiesIfRequired()
    {
        //1
        float playerX = transform.position.x;
        float leftWall = playerX - screenWidthInPoints;
        float rightWall = playerX + screenWidthInPoints;
        float farthestEnemyX = 0;
        //2
        List<GameObject> enemiesToRemove = new List<GameObject>();
        foreach (var enemy in enemies)
        {
            //3
            if(enemy) {
                float enemyX = enemy.transform.position.x;
                //4
                farthestEnemyX = Mathf.Max(farthestEnemyX, enemyX);
                //5
                if (enemyX < leftWall) 
                {           
                    enemiesToRemove.Add(enemy);
                }
            }
            
            else {
                enemiesToRemove.Add(enemy); 
            }
            
        }
        //6
        foreach (var enemy in enemiesToRemove)
        {
            enemies.Remove(enemy); 
            if (enemy!=null){
                Destroy(enemy);
            }
            
        }
        //7
        if (farthestEnemyX < rightWall && enemies.Count < maxEnemies)
        {
            AddEnemy(farthestEnemyX);
        }
    }
    void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
        StartCoroutine(GeneratorCheck());
    }
    private IEnumerator GeneratorCheck()
    {
        while (true)
        {
            GenerateEnemiesIfRequired();
            yield return new WaitForSeconds(0.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

    }
}
