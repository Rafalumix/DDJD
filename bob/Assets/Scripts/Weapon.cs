using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firePoint; 
    public GameObject bulletPrefab; 
    public bool powerup = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            Shoot(); 
        }
    }

    void Shoot(){
        // shooting logic
        if(!powerup) 
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); 
        else {   
            Vector2 up = new Vector2(firePoint.position.x, firePoint.position.y + 0.8f);
            Vector2 down = new Vector2(firePoint.position.x, firePoint.position.y - 0.8f);
            Vector2 middle = new Vector2(firePoint.position.x + 0.5f, firePoint.position.y);
            Instantiate(bulletPrefab, up, firePoint.rotation); 
            Instantiate(bulletPrefab, middle, firePoint.rotation); 
            Instantiate(bulletPrefab, down, firePoint.rotation); 
        }
    }
}
