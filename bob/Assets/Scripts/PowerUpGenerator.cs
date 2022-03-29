using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
// Start is called before the first frame update
    public GameObject healthPack;
    private GameObject player;
    public float distanceRespawn;
    private float screenWidthInPoints;


    private List<GameObject> powerups = new List<GameObject>();


    void GeneratePowerupsIfRequired()
    {
        //1
        float playerX = transform.position.x;
        float leftWall = playerX - screenWidthInPoints;
        float rightWall = playerX + screenWidthInPoints;
        //2
        List<GameObject> PowerupsToRemove = new List<GameObject>();
        foreach (var powerup in powerups)
        {
            //3
            if(powerup) 
            {
                float powerupX = powerup.transform.position.x;
                //5
                if (powerupX < leftWall) 
                {           
                    PowerupsToRemove.Add(powerup);
                    print("delete");
                }
            }
            else {
                PowerupsToRemove.Add(powerup);
            }
        }
        //6
        foreach (var powerup in PowerupsToRemove)
        {
            powerups.Remove(powerup);
            Destroy(powerup);
        }

        //generate healthpack
        float currentPosition = this.player.transform.position.x;
        if((int)currentPosition % distanceRespawn == 0)
        {
            print(currentPosition);
            GameObject obj = (GameObject)Instantiate(healthPack);
            float randomY = Random.Range(player.GetComponent<ObjectGenerator>().objectsMinY, player.GetComponent<ObjectGenerator>().objectsMaxY);
            obj.transform.position = new Vector3(currentPosition + 10, randomY,0);
            powerups.Add(obj);
        }
    }

    void Start()
    {
        this.player = GameObject.FindWithTag("Player");
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
        StartCoroutine(GeneratorCheck());
    }

    private IEnumerator GeneratorCheck()
    {
        while (true)
        {
            GeneratePowerupsIfRequired();
            yield return new WaitForSeconds(0.25f);
            
        }
    }
}
