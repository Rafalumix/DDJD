using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public Rigidbody2D obstacle;
    public float forwardMovementSpeed = -5.0f;

    // Start is called before the first frame update
    void Start()
    {
        obstacle = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(transform.CompareTag("Obstacle"))
        {
            Vector2 newVelocity = obstacle.velocity;
            newVelocity.x = forwardMovementSpeed;
            obstacle.velocity = newVelocity;
            transform.Rotate(0,0,50*Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(transform.CompareTag("Projectile")) 
        {
            if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Trap") || collider.gameObject.CompareTag("Projectile") )
            {   
                GameObject enemy = collider.gameObject;
                Destroy(enemy);
            }
        }
    }
}
