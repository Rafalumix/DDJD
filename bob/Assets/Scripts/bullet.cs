using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public float speed = 20f; 
    private Rigidbody2D bulletRigidBody; 

    // Start is called before the first frame update
    void Start()
    {   
        bulletRigidBody = GetComponent<Rigidbody2D>();

        bulletRigidBody.velocity = transform.right * speed; 
    }

    void FixedUpdate() 
    {
        float playerPos = GameObject.Find("Player").transform.position.x;
        if (bulletRigidBody.gameObject.transform.position.x - playerPos >= 15){
            Destroy(gameObject);
        }


    }

    void OnTriggerEnter2D (Collider2D hitInfo){
        if (hitInfo.gameObject.CompareTag("Enemy") || hitInfo.gameObject.CompareTag("Obstacle") )
        {
            // Debug.Log(hitInfo.name); 
            Destroy(gameObject);
        }


    }
}
