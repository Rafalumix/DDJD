using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    private float distanceToPlayer;
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        distanceToPlayer = transform.position.x - targetObject.transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        float targetObjectX = targetObject.transform.position.x;
        Vector3 newCameraPosition = transform.position;

        newCameraPosition.x = targetObjectX + distanceToPlayer;
        transform.position = newCameraPosition;
    }
}
