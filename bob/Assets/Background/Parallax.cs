using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam; 
    public Transform subject; 
    Vector2 startPosition; 
    float startZ; 
 
    Vector2 travel => (Vector2)cam.transform.position - startPosition; //distance camera has been moved from the start 
    
    float distanceFromSubject => transform.position.z - subject.position.z; 
    float clippingPlane => (cam.transform.position.z +(distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;
    Vector2 parallaxFactor; 
    
    // Start is called before the first frame update
    public void Start()
    {
        startPosition = transform.position; 
        startZ = transform.position.z; 
    }


    // Update is called once per frame
    public void Update()
    {
       transform.position = startPosition + travel * parallaxFactor;
       transform.position = new Vector3(newPos.x, newPos.y, startZ); 
    }
}
