using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthpack : MonoBehaviour
{
    private float screenWidthInPoints;
    public GameObject HealthPackObject;

    void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {   
            GameObject player = collider.gameObject;
            playerMovement playerScript = player.GetComponent<playerMovement>();
            if(playerScript) 
            {
                playerScript.Heal();
                playerScript.healthBar.UpdateHealthBar();
                Destroy(gameObject);
            }  
        }
    }
}
