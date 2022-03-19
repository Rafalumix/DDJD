using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{


    public float rotationSpeed = 0.0f;
    public float obstacleSpeed = -0.001f;
    private Collider2D laserCollider;
    private SpriteRenderer laserRenderer;

    // Start is called before the first frame update
    void Start()
    {
        laserCollider = gameObject.GetComponent<Collider2D>();
        laserRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        transform.Translate(new Vector2(-obstacleSpeed * Time.deltaTime, 0), Space.World);
    }
}
