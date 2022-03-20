using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] availableEnemies;
    public List<GameObject> enemies;

    public float enemyMinDistance = 5.0f;
    public float enemyMaxDistance = 10.0f;

    public float enemyMinY = -1.4f;
    public float enemyMaxY = 1.4f;
    private float screenWidthInPoints;

    void AddEnemy(float lastEnemyX)
    {
        Debug.Log("yo");
        //1
        int randomIndex = Random.Range(0, availableEnemies.Length);
        //2
        GameObject obj = (GameObject)Instantiate(availableEnemies[randomIndex]);
        //3
        float enemyPositionX = lastEnemyX + Random.Range(enemyMinDistance, enemyMaxDistance);
        float randomY = Random.Range(enemyMinY, enemyMaxY);
        obj.transform.position = new Vector3(enemyPositionX,randomY,0); 

        //5
        enemies.Add(obj);            
    }

    void GenerateEnemiesIfRequired()
    {
        //1
        float playerX = transform.position.x;
        float removeEnemyX = playerX - screenWidthInPoints;
        float addEnemyX = playerX + screenWidthInPoints;
        float farthestEnemyX = 0;
        //2
        List<GameObject> enemiesToRemove = new List<GameObject>();
        foreach (var enemy in enemies)
        {
            //3
            float enemyX = enemy.transform.position.x;
            //4
            farthestEnemyX = Mathf.Max(farthestEnemyX, enemyX);
            //5
            if (enemyX < removeEnemyX) 
            {           
                enemiesToRemove.Add(enemy);
            }
        }
        //6
        foreach (var enemy in enemiesToRemove)
        {
            enemies.Remove(enemy);
            Destroy(enemy);
        }
        //7
        if (farthestEnemyX < addEnemyX && enemies.Count < 3)
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
        Debug.Log("yo");
    }

    void FixedUpdate()
    {
        Debug.Log("yo");
    }
}
