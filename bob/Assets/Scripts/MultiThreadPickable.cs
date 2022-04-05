using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MultiThreadPickable : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider) {

        if (collider.gameObject.CompareTag("Player"))
        {   
            collider.gameObject.GetComponent<Weapon>().powerup = true;
            collider.gameObject.GetComponent<MultiThreadPowerup>().Activate();
            GameObject.Find("MultiThread").GetComponent<Image>().enabled = true; 
            Destroy(gameObject);
        }
    }
}
