using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showText : MonoBehaviour
{
    private HardcoreMode hs; 
    // Start is called before the first frame update
    void Start()
    {
        hs = GetComponent<HardcoreMode>(); 
        if (hs.IsHardcore()==true){
            gameObject.SetActive(true); 
        }
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
