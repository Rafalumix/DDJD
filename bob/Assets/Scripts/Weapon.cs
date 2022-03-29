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
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); 
        if(powerup) 
        {   
            Vector2 up = new Vector2(firePoint.position.x, firePoint.position.y + 0.8f);
            Vector2 down = new Vector2(firePoint.position.x, firePoint.position.y - 0.8f);
            Instantiate(bulletPrefab, up, firePoint.rotation); 
            Instantiate(bulletPrefab, down, firePoint.rotation); 
        }
    }
}
