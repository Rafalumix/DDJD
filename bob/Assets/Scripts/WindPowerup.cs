using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WindPowerup : MonoBehaviour
{

    public bool pickedUp = false;
    public Text popUp;

    
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
            GameObject.Find("WindPush").GetComponent<Image>().enabled = true; 
            collider.gameObject.GetComponent<WindPush>().pickedUp = true;
            Destroy(gameObject);
            GameObject canvas = GameObject.Find("Canvas");
            Text obj = Instantiate(popUp, canvas.transform);
            Destroy(obj, 2.5f);
        }
    }
}
