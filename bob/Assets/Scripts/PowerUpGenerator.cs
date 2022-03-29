using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
// Start is called before the first frame update
    public GameObject healthPack;
    public GameObject WindPowerup;
    public GameObject MultiThreadPowerup;
    private GameObject player;
    public float distanceRespawn;
    private float screenWidthInPoints;
    private bool once = true;

    

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
            once = true;
        }

        //generate healthpack
        float currentPosition = this.player.transform.position.x;
        if((int)currentPosition % distanceRespawn == 0)
        {
            GameObject obj = (GameObject)Instantiate(healthPack);
            float randomY = Random.Range(player.GetComponent<ObjectGenerator>().objectsMinY, player.GetComponent<ObjectGenerator>().objectsMaxY);
            obj.transform.position = new Vector3(currentPosition + 10, randomY,0);
            powerups.Add(obj);
        }
        
        if (once && (int)currentPosition % 100 == 0 && player.gameObject.GetComponent<WindPush>().pickedUp == false)
        {
            GameObject obj = (GameObject)Instantiate(WindPowerup);
            float randomY = Random.Range(player.GetComponent<ObjectGenerator>().objectsMinY, player.GetComponent<ObjectGenerator>().objectsMaxY);
            obj.transform.position = new Vector3(currentPosition + 100, randomY,0);
            powerups.Add(obj);
            once = false;
        }

        if ((int)currentPosition % 50 == 0)
        {
            GameObject obj = (GameObject)Instantiate(MultiThreadPowerup);
            float randomY = Random.Range(player.GetComponent<ObjectGenerator>().objectsMinY, player.GetComponent<ObjectGenerator>().objectsMaxY);
            obj.transform.position = new Vector3(currentPosition + 20, randomY,0);
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
            yield return new WaitForSeconds(0.5f);
            
        }
    }
}
