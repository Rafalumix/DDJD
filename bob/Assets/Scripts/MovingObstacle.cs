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

        Vector2 newVelocity = obstacle.velocity;
        newVelocity.x = forwardMovementSpeed;
        obstacle.velocity = newVelocity;
        transform.Rotate(0,0,50*Time.deltaTime);
    }
}
