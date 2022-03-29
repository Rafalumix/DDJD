using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPointScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition += new Vector3(-2f,0,-7f);
        Destroy(gameObject, 1f); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
